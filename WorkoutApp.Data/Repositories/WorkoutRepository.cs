﻿using SQLite;
using WorkoutApp.Data.Models;
using WorkoutApp.Data.Repositories.Base;

namespace WorkoutApp.Data.Repositories
{
    public class WorkoutRepository : BaseRepository<Workout>
    {
        SQLiteAsyncConnection Database;

        public WorkoutRepository() : base()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            _ = await Database.CreateTableAsync<Workout>();
        }

        public async Task<Workout> GetByIdAsync(int id)
        {
            await Init();

            return await Database.Table<Workout>().Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Workout>> GetAllAsync()
        {
            await Init();

            var workouts = await Database.Table<Workout>().ToListAsync();

            foreach (var workout in workouts)
            {
                workout.Batches = await Database.Table<ExerciseBatch>().Where(b => b.WorkoutId == workout.Id).ToListAsync();

                foreach (var batch in workout.Batches)
                {
                    batch.Sets = await Database.Table<ExerciseSet>().Where(s => s.ExerciseBatchId == batch.Id).ToListAsync();

                    foreach (var set in batch.Sets)
                    {
                        set.Exercise = await Database.Table<Exercise>().Where(e => e.Id == set.ExerciseId).FirstOrDefaultAsync();
                    }
                }
            }

            return workouts;
        }

        public async Task<int> InsertAsync(Workout workout)
        {
            await Init();

            return await Database.InsertAsync(workout);
        }

        public async Task DeleteAllAsync()
        {
            await Init();

            await Database.DeleteAllAsync<Workout>();
        }

        public async Task DeleteAllSetsAsync()
        {
            await Init();

            await Database.DeleteAllAsync<ExerciseSet>();
        }

        public async Task DeleteAllBatchesAsync()
        {
            await Init();

            await Database.DeleteAllAsync<ExerciseBatch>();
        }

        public async Task<int> GetNumOfWorkouts()
        {
            await Init();

            return await Database.Table<Workout>().CountAsync();
        }

        public async Task<int> GetAmountOfPersonalRecordsAsync(Workout workout)
        {
            await Init();

            var prs = 0;

            var allSets = workout.Batches.SelectMany(b => b.Sets).ToList();

            var exercises = allSets.Select(s => s.Exercise).Distinct();

            foreach (var exercise in exercises)
            {
                var batches = await Database.Table<ExerciseBatch>().Where(b => b.ExerciseId == exercise.Id).ToListAsync();

                foreach (var batch in batches)
                {
                    batch.Sets = await Database.Table<ExerciseSet>().Where(s => s.ExerciseBatchId == batch.Id).ToListAsync();
                }

                var maxWeight = batches.SelectMany(b => b.Sets).Max(s => s.Weight);

                var allSetsThisWorkout = workout.Batches.Where(b => b.ExerciseId == exercise.Id).SelectMany(b => b.Sets).ToList();

                var currentWeight = allSetsThisWorkout.Where(s => s.ExerciseId == exercise.Id).Max(s => s.Weight);

                if (currentWeight > maxWeight)
                    prs++;
            }

            return prs;
        }
    }
}
