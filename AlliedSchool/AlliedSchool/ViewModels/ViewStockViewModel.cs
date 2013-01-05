using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class ViewStockViewModel : ViewModelBase
    {
        #region Constructors
        public ViewStockViewModel(MainWindow MainWindow, AlliedSchoolModelContext SchoolContext)
        {
            this.MainWindow = MainWindow;
            this.SchoolContext = SchoolContext;
            ItemsList = new List<Item>();
            foreach (var item in SchoolContext.Items)
                ItemsList.Add(item);
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

        public string ItemName
        {
            get { return (SelectedItem == null) ? string.Empty : SelectedItem.Name; }
            set
            {
                SelectedItem.Name = value;
                OnPropertyChanged("ItemName");
            }
        }

        public Int64 Price
        {
            get { return (SelectedItem == null) ? default(Int64) : SelectedItem.Price; }
            set
            {
                SelectedItem.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public Int64 Quantity
        {
            get { return (SelectedItem == null) ? default(Int64) : SelectedItem.Quantity; }
            set
            {
                SelectedItem.Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        #region CommandBindings
        private ICommand _updateCommand;
        public ICommand UpdateCommand
        {
            get { return _updateCommand ?? (_updateCommand = new RelayCommand(Update, CanUpdate)); }
        }

        private bool CanUpdate()
        {
            return (string.IsNullOrEmpty(ItemName) || Price == default(Int64) ? false : true);
        }

        private void Update()
        {
            SchoolContext.SaveChanges();
            SelectedItem = null;
        }
        #endregion

    }
}
