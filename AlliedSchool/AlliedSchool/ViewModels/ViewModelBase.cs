using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    abstract public class ViewModelBase : INotifyPropertyChanged
    {
        public MainWindow MainWindow { get; set; }

        public AlliedSchoolModelContext SchoolContext { get; set; }

        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get { return _clearCommand ?? (_clearCommand = new RelayCommand(Clear, CanClickClear)); }
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand ?? (_backCommand = new RelayCommand(Back, CanClickBack)); }
        }

        public bool CanClickClear() { return true; }
        abstract public void Clear();

        public bool CanClickBack() { return true; }
        abstract public void Back();

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
