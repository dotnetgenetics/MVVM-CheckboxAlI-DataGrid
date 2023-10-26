using MVVMCheckboxAlIDataGrid.Model;
using MVVMCheckboxAlIDataGrid.ViewModel.Base;
using MVVMCheckboxAlIDataGrid.ViewModel.Command;
using MVVMCheckboxAlIDataGrid.ViewModel.Converters;
using MVVMCheckboxAlIDataGrid.ViewModel.DAL;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVMCheckboxAlIDataGrid.ViewModel
{
   public class ProductsViewModel : ViewModelBase
   {
      #region Commands

      private ICommand _updateItemCommand;
      private ICommand _updateAllCommand;
      public ICommand UpdateItemCommand
      {
         get
         {
            if (_updateItemCommand == null)
            {
               _updateItemCommand = new RelayCommand(param => UpdateItem(param), null);
            }

            return _updateItemCommand;
         }
      }

      public ICommand UpdateAllCommand
      {
         get
         {
            if (_updateAllCommand == null)
            {
               _updateAllCommand = new RelayCommand(param => UpdateAllProducts(param), null);
            }

            return _updateAllCommand;
         }
      }

      #endregion

      #region Members

      private ObservableCollection<Products> _productList;
      public ObservableCollection<Products> ProductList
      {
         get
         {
            return _productList;
         }
         set
         {
            _productList = value;
            OnPropertyChanged("ProductList");
         }
      }

      private bool _checkAll;
      public bool CheckAll
      {
         get
         {
            return _checkAll;
         }
         set
         {
            _checkAll = value;
            OnPropertyChanged("CheckAll");
         }
      }

      public MultiParamConverter MultiParamConverter
      {
         get; set;
      }

      #endregion

      #region ViewModelMethods

      public ProductsViewModel()
      {
         MultiParamConverter = new MultiParamConverter();
         GetAllProducts();
      }

      private void UpdateItem(object param)
      {
         var product = (Products)param;
         DBUtil.UpdateProductDiscontinue(product.Discontinue, product.Id);
         GetAllProducts();
      }

      private void UpdateAllProducts(object param)
      {
         var values = (object[])param;
         var check = (bool)values[0];
         var productsItemCollection = (ItemCollection)values[1];

         if (productsItemCollection.Count > 0)
         {
            foreach (var item in productsItemCollection)
            {
               if (item != null)
               {
                  DBUtil.UpdateProductDiscontinue(check, ((Products)item).Id);
               }
            }
         }

         GetAllProducts();
      }

      private void GetAllProducts()
      {
         bool flag = true;

         ProductList = new ObservableCollection<Products>(Products(DBUtil.GetProduct()));

         foreach (var product in ProductList)
         {
            if (!product.Discontinue)
            {
               flag = false;
               CheckAll = false;
            }
         }

         if (flag)
            CheckAll = true;
      }

      private ObservableCollection<Products> Products(DataTable dt)
      {
         var convertedList = (from rw in dt.AsEnumerable()
                              select new Products()
                              {
                                 Id = Convert.ToInt32(rw["productID"]),
                                 ProductName = rw["ProductName"].ToString(),
                                 UnitPrice = Convert.ToDecimal(rw["UnitPrice"]),
                                 QuantityPerUnit = (rw["QuantityPerUnit"]).ToString(),
                                 Discontinue = (bool)rw["Discontinue"]
                              }).ToList();

         return new ObservableCollection<Products>(convertedList);
      }

      #endregion
   }
}
