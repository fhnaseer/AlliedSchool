using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class AddStudentViewModel : ViewModelBase
    {
        #region Constructors
        public AddStudentViewModel(MainWindow MainWindow, AlliedSchoolModelContext SchoolContext)
        {
            this.MainWindow = MainWindow;
            this.SchoolContext = SchoolContext;
        }
        #endregion

        #region BaseClass Interface
        public override void Clear()
        {
            SelectedClass = null;
            FirstName = LastName = string.Empty;
        }

        public override void Back()
        {
            MainWindow.DataContext = new WelcomePageViewModel(MainWindow, SchoolContext);
        }
        #endregion

        public string FirstName { get; set; }
        public string LastName { get; set; }

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
                    OnPropertyChanged("SelectedClass");
                }
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
            return (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ? false : true);
        }

        private void Add()
        {
            var student = new Student();
            student.FirstName = FirstName;
            student.LastName = LastName;
            student.FullName = FirstName + " " + LastName;
            student.StandardId = SelectedClass.Id;
            SchoolContext.Students.Add(student);
            SchoolContext.SaveChanges();
        }
        #endregion
    }
}
