using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class PurchaseItemViewModel : ViewModelBase
    {
        #region Constructors
        public PurchaseItemViewModel(MainWindow MainWindow, AlliedSchoolModelContext SchoolContext)
        {
            this.MainWindow = MainWindow;
            this.SchoolContext = SchoolContext;
            ItemsList = new List<Item>();
            foreach (var item in SchoolContext.Items)
                ItemsList.Add(item);
            StudentsList = new List<Student>();
            foreach (var student in SchoolContext.Students)
                StudentsList.Add(student);
        }
        #endregion

        #region BaseClass Interface
        public override void Clear()
        {
            SelectedItem = null;
            SelectedStudent = null;
            Price = Quantity = 0;
        }

        public override void Back()
        {
            MainWindow.DataContext = new WelcomePageViewModel(MainWindow, SchoolContext);
        }
        #endregion

        public List<Student> StudentsList { get; set; }
        public List<Item> ItemsList { get; set; }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("Price");
                    OnPropertyChanged("Quantity");
                    OnPropertyChanged("SelectedItem");
                }
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
                    OnPropertyChanged("SelectedStudent");
                }
            }
        }

        private Int64 _price;
        public Int64 Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        private Int64 _quantity;
        public Int64 Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                Price = SelectedItem.Price * Quantity;
                OnPropertyChanged("Quantity");
                OnPropertyChanged("Price");
            }
        }

        public bool IsPaid { get; set; }

        #region CommandBindings
        private ICommand _purchaseCommand;
        public ICommand PurchaseCommand
        {
            get { return _purchaseCommand ?? (_purchaseCommand = new RelayCommand(Purchase, CanPurchase)); }
        }

        private bool CanPurchase()
        {
            return (SelectedStudent == null || SelectedItem == null) ? false : true;
        }

        private void Purchase()
        {
            var shoppingItem = new ShoppingItem();
            shoppingItem.ItemId = SelectedItem.ItemId;
            shoppingItem.StudentId = SelectedStudent.StudentId;
            shoppingItem.Student = SelectedStudent;
            shoppingItem.Quantity = Quantity;
            shoppingItem.Price = Price;
            shoppingItem.IsPaid = IsPaid;
            SchoolContext.ShoppingItems.Add(shoppingItem);
            SchoolContext.SaveChanges();
            Clear();
        }
        #endregion
    }
}
