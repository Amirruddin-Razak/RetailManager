using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWPFUserInterface.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<string> _products;
        private BindingList<string> _cart;
        private double _itemQuantity;

        public BindingList<string> Products
        {
            get => _products;
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public BindingList<string> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public double ItemQuantity
        {
            get => _itemQuantity;
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public string Subtotal
        {
            get
            {
                return "$0.00";
            }
        }

        public string Tax
        {
            get
            {
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                return "$0.00";
            }
        }

        public bool CanAddToCart()
        {
            //Todo Check if item is selected
            return true;
        }

        public void AddToCart()
        {
            //Todo add item to cart
        }

        public bool CanRemoveFromCart()
        {
            //Todo Check if item is selected
            return true;
        }

        public void RemoveFromCart()
        {
            //Todo remove item from cart
        }

        public bool CanCheckOut()
        {
            //Todo Check if cart is not empty
            return true;
        }

        public void CheckOut()
        {
            //Todo handle check out
        }
    }
}
