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
        private string FamilyID { get; set; }

        private string _fatherName;
        public string FatherName
        {
            get { return _fatherName; }
            set
            {
                _fatherName = value;
                OnPropertyChanged("FatherName");
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
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
            set
            {
                _studentsList = value;
                OnPropertyChanged("StudentsList");
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
                    FatherName = _selectedStudent.FatherName;
                    Address = _selectedStudent.Address;
                    PhoneNumber = _selectedStudent.PhoneNumber;
                    FamilyID = _selectedStudent.FamilyID;
                    OnPropertyChanged("SelectedStudent");
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
            return (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || SelectedClass == null ||
                string.IsNullOrEmpty(FatherName) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(PhoneNumber) ? false : true);
        }

        private void Add()
        {
            var student = new Student();
            student.FirstName = FirstName;
            student.LastName = LastName;
            student.FullName = FirstName + " " + LastName;
            student.FatherName = FatherName;
            student.Address = Address;
            student.PhoneNumber = PhoneNumber;
            if (string.IsNullOrEmpty(FamilyID))
                FamilyID = FatherName + Address;
            student.FamilyID = FamilyID;
            student.StandardId = SelectedClass.Id;
            SchoolContext.Students.Add(student);
            SchoolContext.SaveChanges();
            StudentsList = null;
        }
        #endregion
    }
}
