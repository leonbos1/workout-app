﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using WorkoutApp.Data.Models;

namespace WorkoutApp.ViewModels
{
    public class WorkoutPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ExerciseBatchViewModel> ExerciseBatches { get; set; } = new ObservableCollection<ExerciseBatchViewModel>();

        public WorkoutPageViewModel()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
