using MVVMCheckboxAlIDataGrid.ViewModel.Base;

namespace MVVMCheckboxAlIDataGrid.Model
{
   public class Products : ViewModelBase
   {
      private int _id;
      public int Id
      {
         get
         {
            return _id;
         }
         set
         {
            _id = value;
            OnPropertyChanged("Id");
         }
      }

      private string _name;
      public string ProductName
      {
         get
         {
            return _name;
         }
         set
         {
            _name = value;
            OnPropertyChanged("ProductName");
         }
      }

      private decimal _price;
      public decimal UnitPrice
      {
         get
         {
            return _price;
         }
         set
         {
            _price = value;
            OnPropertyChanged("UnitPrice");
         }
      }

      private string _quantity;
      public string QuantityPerUnit
      {
         get
         {
            return _quantity;
         }
         set
         {
            _quantity = value;
            OnPropertyChanged("QuantityPerUnit");
         }
      }

      private bool _discontinue;
      public bool Discontinue
      {
         get
         {
            return _discontinue;
         }
         set
         {
            _discontinue = value;
            OnPropertyChanged("Discontinue");
         }
      }
   }
}
