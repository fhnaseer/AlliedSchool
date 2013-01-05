using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class ViewStudentsViewModel : ViewModelBase
    {
        #region Constructors
        public ViewStudentsViewModel(MainWindow MainWindow, AlliedSchoolModelContext SchoolContext)
        {
            this.MainWindow = MainWindow;
            this.SchoolContext = SchoolContext;
        }
        #endregion

        #region BaseClass Interface
        public override void Clear()
        {
            OnPropertyChanged("StudentsList");
            SelectedStudent = null;
            SelectedClass = null;
        }

        public override void Back()
        {
            MainWindow.DataContext = new WelcomePageViewModel(MainWindow, SchoolContext);
        }
        #endregion

        private List<Student> _studentsList;
        public List<Student> StudentsList
        {
            get
            {
                if (_studentsList == null)
                {
                    _studentsList = new List<Student>();
                    foreach (var student in SchoolContext.Students)
                        _studentsList.Add(student);
                }
                return _studentsList;
            }
        }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value;
                    FirstName = (_selectedStudent == null) ? string.Empty : _selectedStudent.FirstName;
                    LastName = (_selectedStudent == null) ? string.Empty : _selectedStudent.LastName;
                    SelectedClass = _selectedStudent.Standard;
                    OnPropertyChanged("SelectedStudent");
                }
            }
        }

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

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
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
            return (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ? false : true);
        }

        private void Delete()
        {
            SchoolContext.Students.Remove(SelectedStudent);
            SchoolContext.SaveChanges();
            StudentsList.Remove(SelectedStudent);
            SchoolContext.SaveChanges();
            Clear();
        }

        private void Update()
        {
            SelectedStudent.FirstName = FirstName;
            SelectedStudent.LastName = LastName;
            SelectedStudent.FullName = FirstName + " " + LastName;
            SelectedStudent.StandardId = SelectedClass.Id;
            SchoolContext.SaveChanges();
            OnPropertyChanged("SelectedStudent");
        }
        #endregion

    }
}
