﻿
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ModelAbstractApi modelApi;
        public ObservableCollection<IBall> balls { set; get; }
        public ICommand ButtonClicked { get; set; }
        private string textToInteger;
        private bool active;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel() : this(ModelAbstractApi.CreateApi())
        {

        }

        public ViewModel(ModelAbstractApi model)
        {
            active = true;
            this.modelApi = model;
            balls = new ObservableCollection<IBall>();
            IDisposable observer = modelApi.Subscribe(x => balls.Add(x));
            ButtonClicked = new UserCommand(() => StartButtonClicked());
        }


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StartButtonClicked()
        {
            modelApi.GenerateBalls(numberOfBallsToInt());
        }

    

        public int numberOfBallsToInt()
        {
            int number;
            if (Int32.TryParse(textToInteger, out number) && textToInteger != "0")
            {
                number = Int32.Parse(textToInteger);
                Active = false;
                if (number > 15)
                {
                    return 15;
                }
                return number;
            }
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
    }
}
