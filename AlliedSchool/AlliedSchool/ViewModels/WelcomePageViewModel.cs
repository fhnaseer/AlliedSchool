using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AlliedSchool.ViewModels
{
    public class WelcomePageViewModel : ViewModelBase
    {
        #region Constructors
        public WelcomePageViewModel(MainWindow mainWindow)
            : this(mainWindow, new AlliedSchoolModelContext())
        {
        }

        public WelcomePageViewModel(MainWindow mainWindow, AlliedSchoolModelContext schoolContext)
        {
            this.MainWindow = mainWindow;
            this.SchoolContext = schoolContext;
        }
        #endregion

        #region BaseClass Interface
        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override void Back()
        {
            MainWindow.Close();
        }
        #endregion

        #region CommandBindings
        private ICommand _addStudentCommand;
        public ICommand AddStudentCommand
        {
            get { return _addStudentCommand ?? (_addStudentCommand = new RelayCommand(AddStudent)); }
        }

        private ICommand _addItemCommand;
        public ICommand AddItemCommand
        {
            get { return _addItemCommand ?? (_addItemCommand = new RelayCommand(AddItem)); }
        }

        private ICommand _viewStudentsCommand;
        public ICommand ViewStudentsCommand
        {
            get { return _viewStudentsCommand ?? (_viewStudentsCommand = new RelayCommand(ViewStudents)); }
        }

        private ICommand _viewStockCommand;
        public ICommand ViewStockCommand
        {
            get { return _viewStockCommand ?? (_viewStockCommand = new RelayCommand(ViewStock)); }
        }

        private ICommand _purchaseItemCommand;
        public ICommand PurchaseItemCommand
        {
            get { return _purchaseItemCommand ?? (_purchaseItemCommand = new RelayCommand(PurchaseItem)); }
        }

        private ICommand _addClassCommand;
        public ICommand AddClassCommand
        {
            get { return _addClassCommand ?? (_addClassCommand = new RelayCommand(AddClass)); }
        }

        private ICommand _viewClassCommand;
        public ICommand ViewClassesCommand
        {
            get { return _viewClassCommand ?? (_viewClassCommand = new RelayCommand(ViewClasses)); }
        }
        #endregion

        public void AddStudent()
        {
            MainWindow.DataContext = new AddStudentViewModel(MainWindow, SchoolContext);
        }

        public void ViewStudents()
        {
            MainWindow.DataContext = new ViewStudentsViewModel(MainWindow, SchoolContext);
        }

        private void AddItem()
        {
            MainWindow.DataContext = new AddItemViewModel(MainWindow, SchoolContext);
        }

        public void ViewStock()
        {
            MainWindow.DataContext = new ViewStockViewModel(MainWindow, SchoolContext);
        }

        public void PurchaseItem()
        {
            MainWindow.DataContext = new PurchaseItemViewModel(MainWindow, SchoolContext);
        }

        public void AddClass()
        {
            MainWindow.DataContext = new AddClassViewModel(MainWindow, SchoolContext);
        }

        public void ViewClasses()
        {
            MainWindow.DataContext = new ViewClassViewModel(MainWindow, SchoolContext);
        }
    }
}
