﻿namespace WorkoutApp.Data.Models
{
    public class Exercise
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}