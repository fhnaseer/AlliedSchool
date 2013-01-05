using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class AddItemViewModel : ViewModelBase
    {
        #region Constructors
        public AddItemViewModel(MainWindow MainWindow, AlliedSchoolModelContext SchoolContext)
        {
            this.MainWindow = MainWindow;
            this.SchoolContext = SchoolContext;
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

        public string ItemName { get; set; }
        public Int64 Price { get; set; }
        public Int64 Quantity { get; set; }

        #region CommandBindings
        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(Add, CanAdd)); }
        }

        private bool CanAdd()
        {
            return (string.IsNullOrEmpty(ItemName) || Price == default(Int64) || Quantity == default(Int64)) ? false : true;
        }

        private void Add()
        {
            var item = new Item();
            item.Name = ItemName;
            item.Price = Price;
            item.Quantity = Quantity;
            SchoolContext.Items.Add(item);
            SchoolContext.SaveChanges();
        }
        #endregion
    }
}
