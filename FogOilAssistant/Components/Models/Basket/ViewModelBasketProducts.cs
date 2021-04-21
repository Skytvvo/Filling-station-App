using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FogOilAssistant.Annotations;
using FogOilAssistant.Components.Data.GlobalStorage;
using FogOilAssistant.Components.Database;

namespace FogOilAssistant.Components.Models.Basket
{
    public class ViewModelBasketProducts : INotifyPropertyChanged
    {

        #region Props

        private List<Product> baskedProducts;
        public List<Product> BaskedProducts
        {
            get => baskedProducts;
            set
            {
                this.baskedProducts = value;
                OnPropertyChanged("BaskedProducts");
            }
        }

        #endregion


        #region Constructor

        public ViewModelBasketProducts()
        {
            this.BaskedProducts = DataBaseData.getInstance().BasketProducts;
        }

        #endregion


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        #endregion

    }
}
