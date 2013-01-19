using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class ViewClassViewModel : ViewModelBase
    {
        #region Constructors
        public ViewClassViewModel(MainWindow MainWindow, AlliedSchoolModelContext SchoolContext)
        {
            this.MainWindow = MainWindow;
            this.SchoolContext = SchoolContext;
        }
        #endregion

        #region BaseClass Interface
        public override void Clear()
        {
            OnPropertyChanged("ClassesList");
            SelectedClass = null;
        }

        public override void Back()
        {
            MainWindow.DataContext = new WelcomePageViewModel(MainWindow, SchoolContext);
        }
        #endregion

        private List<Standard> _classesList;
        public List<Standard> ClassesList
        {
            get
            {
                if (_classesList == null)
                {
                    _classesList = new List<Standard>();
                    foreach (var standard in SchoolContext.Standards)
                        _classesList.Add(standard);
                }
                return _classesList;
            }
            set
            {
                _classesList = value;
                OnPropertyChanged("ClassesList");
            }
        }

        private Standard _selectedClass;
        public Standard SelectedClass
        {
            get { return _selectedClass; }
            set
            {
                if (_selectedClass != value)
                {
                    _selectedClass = value;
                    ClassName = (_selectedClass == null) ? string.Empty : _selectedClass.ClassName;
                    SectionName = (_selectedClass == null) ? string.Empty : _selectedClass.SectionName;
                    OnPropertyChanged("SelectedClass");
                }
            }
        }

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
        private ICommand _updateCommand;
        public ICommand UpdateCommand
        {
            get { return _updateCommand ?? (_updateCommand = new RelayCommand(Update, CanUpdate)); }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete, CanUpdate)); }
        }

        private bool CanUpdate()
        {
            return (string.IsNullOrEmpty(ClassName) || string.IsNullOrEmpty(SectionName) ? false : true);
        }

        private void Delete()
        {
            SchoolContext.Standards.Remove(SelectedClass);
            SchoolContext.SaveChanges();
            Clear();
            ClassesList = null;
        }

        private void Update()
        {
            SelectedClass.ClassName = ClassName;
            SelectedClass.SectionName = SectionName;
            SelectedClass.FullName = ClassName + " " + SectionName;
            SchoolContext.SaveChanges();
            ClassesList = null;
            OnPropertyChanged("SelectedClass");
        }
        #endregion
    }
}
