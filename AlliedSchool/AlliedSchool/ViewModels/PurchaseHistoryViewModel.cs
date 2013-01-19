using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class PurchaseHistoryViewModel : ViewModelBase
    {
        #region Constructors
        public PurchaseHistoryViewModel(MainWindow MainWindow, AlliedSchoolModelContext SchoolContext)
        {
            this.MainWindow = MainWindow;
            this.SchoolContext = SchoolContext;
        }
        #endregion

        #region BaseClass Interface
        public override void Clear()
        {
            SelectedStudent = null;
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
                    _shoppingList = null;
                    RemainingAmount = PaidAmount = TotalAmount = default(long);
                    OnPropertyChanged("ShoppingList");
                    OnPropertyChanged("SelectedStudent");
                    OnPropertyChanged("FeeAmount");
                }
            }
        }

        private List<ShoppingItem> _shoppingList;
        public List<ShoppingItem> ShoppingList
        {
            get
            {
                if (_shoppingList == null)
                {
                    _shoppingList = new List<ShoppingItem>();
                    foreach (var shoppingItem in SchoolContext.ShoppingItems)
                    {
                        if (shoppingItem.Student == SelectedStudent)
                        {
                            _shoppingList.Add(shoppingItem);
                            TotalAmount += shoppingItem.Price;
                            if (shoppingItem.IsPaid)
                                PaidAmount += shoppingItem.Price;
                            else
                                RemainingAmount += shoppingItem.Price;
                        }
                    }
                    TotalAmount += (long)FeeAmount;
                    RemainingAmount += (long)FeeAmount;
                }
                return _shoppingList;
            }
        }

        public double FeeAmount
        {
            get
            {
                if (SelectedStudent == null)
                    return default(double);

                int siblingsCount = 0;
                bool isOldest = false;
                foreach (var student in StudentsList)
                {
                    if (student.FamilyID == SelectedStudent.FamilyID)
                    {
                        if (student == SelectedStudent && siblingsCount == 0)
                            isOldest = true;
                        else
                        {
                            isOldest = false;
                            siblingsCount++;
                        }
                        break;
                    }
                }
                double discount = 0;
                if (siblingsCount > 0 && !isOldest)
                    discount = Double.Parse(Properties.Resources.Discount);
                double fee = Double.Parse(Properties.Resources.Fees);
                return fee - fee * discount;
            }
        }

        private long _totalAmount;
        public long TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

        private long _paidAmount;
        public long PaidAmount
        {
            get { return _paidAmount; }
            set
            {
                _paidAmount = value;
                OnPropertyChanged("PaidAmount");
            }
        }

        private long _remainingAmount;
        public long RemainingAmount
        {
            get { return _remainingAmount; }
            set
            {
                _remainingAmount = value;
                OnPropertyChanged("RemainingAmount");
            }
        }

        #region CommandBindings
        private ICommand _paidCommand;
        public ICommand PaidCommand
        {
            get { return _paidCommand ?? (_paidCommand = new RelayCommand(PayBills, CanPaid)); }
        }

        private bool CanPaid()
        {
            return (SelectedStudent == null) ? false : true;
        }

        private void PayBills()
        {
            foreach (var shoppingItem in ShoppingList)
                shoppingItem.IsPaid = true;
            SchoolContext.SaveChanges();
            Clear();
        }
        #endregion
    }
}
