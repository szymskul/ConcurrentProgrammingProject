﻿using Logic;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace ViewModel
{
        public class ViewModel : INotifyPropertyChanged
        {
            private readonly ModelAbstractApi modelApi;
            public ObservableCollection<BallModel> balls { set; get; }
            public event PropertyChangedEventHandler? PropertyChanged;
            public ICommand ButtonClicked { get; set; }
            string textToInteger;
            private Task task;
            private bool active;
            private bool nonactive = false;
            private string errorMessage;

            public ViewModel() : this(ModelAbstractApi.CreateApi())
            { 
            
            }

            public ViewModel(ModelAbstractApi model)
            {
            active = true;
            this.modelApi = model;
            balls = new ObservableCollection<BallModel>();
            ButtonClicked = new UserCommand(() => StartButtonClicked());
            }


            protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private void StartButtonClicked()
            {
                modelApi.GenerateBalls(numberOfBallsToInt());
                task = new Task(changingPosition);
                task.Start();
            }

            public void changingPosition()
            {
                while (true)
                {
                ObservableCollection<BallModel> ListOfBalls = new ObservableCollection<BallModel>();

                foreach (BallModel ball in modelApi.balls)
                {
                    ListOfBalls.Add(ball);
                }
                balls = ListOfBalls;
                RaisePropertyChanged(nameof(balls));
                Thread.Sleep(10);
            }
        }

            public int numberOfBallsToInt()
            {
                int result;
                if (Int32.TryParse(numberOfBalls, out result) && numberOfBalls != "0")
                {
                    result = Int32.Parse(numberOfBalls);
                    errorMessage = " ";
                    active = !active;
                    nonactive = !nonactive;
                    return result;
                }
                errorMessage = "Zla";
                return 0;
            }
            public string numberOfBalls
            {
                get { return textToInteger; }
                set
                {
                    textToInteger = value;
                    RaisePropertyChanged(nameof(numberOfBalls));
                }

            }

        public bool NotActive
        {
            get
            {
                return nonactive;
            }
            set
            {
                nonactive = value;
                RaisePropertyChanged(nameof(NotActive));
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                RaisePropertyChanged(nameof(Active));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }
    }
}