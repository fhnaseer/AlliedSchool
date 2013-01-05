using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class AddClassViewModel : ViewModelBase
    {
        #region Constructors
        public AddClassViewModel(MainWindow MainWindow, AlliedSchoolModelContext SchoolContext)
        {
            this.MainWindow = MainWindow;
            this.SchoolContext = SchoolContext;
        }
        #endregion

        #region BaseClass Interface
        public override void Clear()
        {
            ClassName = SectionName = string.Empty;
        }

        public override void Back()
        {
            MainWindow.DataContext = new WelcomePageViewModel(MainWindow, SchoolContext);
        }
        #endregion

        private string _className;
        public string ClassName
        {
            get { return _className; }
            set
            {
                _className = value;
                OnPropertyChanged("ClassName");
            }
        }

        private string _sectionName;
        public string SectionName
        {
            get { return _sectionName; }
            set
            {
                _sectionName = value;
                OnPropertyChanged("SectionName");
            }
        }

        #region CommandBindings
        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(Add, CanAdd)); }
        }

        private bool CanAdd()
        {
            return (string.IsNullOrEmpty(ClassName) || string.IsNullOrEmpty(SectionName) ? false : true);
        }

        private void Add()
        {
            var standard = new Standard();
            standard.ClassName = ClassName;
            standard.SectionName = SectionName;
            standard.FullName = ClassName + " " + SectionName;
            SchoolContext.Standards.Add(standard);
            SchoolContext.SaveChanges();
        }
        #endregion
    }
}
