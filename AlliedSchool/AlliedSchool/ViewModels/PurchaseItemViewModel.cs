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
            throw new NotImplementedException();
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
                    OnPropertyChanged("ItemName");
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

        public string ItemName
        {
            get { return (SelectedItem == null) ? string.Empty : SelectedItem.Name; }
            set
            {
                SelectedItem.Name = value;
                OnPropertyChanged("ItemName");
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

        #region CommandBindings
        private ICommand _purchaseCommand;
        public ICommand PurchaseCommand
        {
            get { return _purchaseCommand ?? (_purchaseCommand = new RelayCommand(Purchase, CanPurchase)); }
        }

        private bool CanPurchase()
        {
            return true;
        }

        private void Purchase()
        {
            var shoppingItem = new ShoppingItem();
            shoppingItem.ItemId = SelectedItem.ItemId;
            shoppingItem.StudentId = SelectedStudent.StudentId;
            shoppingItem.Student = SelectedStudent;
            SchoolContext.ShoppingItems.Add(shoppingItem);
            SchoolContext.SaveChanges();
            SelectedItem = null;
        }
        #endregion
    }
}
