using Caliburn.Micro;
using RMWPFUserInterface.Library.Api;
using RMWPFUserInterface.Library.Models;
using RMWPFUserInterface.Models;
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
        private BindingList<ProductModel> _products;
        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
        private ProductModel _selectedProduct;
        private CartItemModel _selectedCartItem;
        private int _itemQuantity;
        IProductEndpoint _productEndpoint;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProduct();
        }

        private async Task LoadProduct()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        public BindingList<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public BindingList<CartItemModel> Cart
        {
            get => _cart;
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public ProductModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public int ItemQuantity
        {
            get => _itemQuantity;
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public CartItemModel SelectedCartItem
        {
            get => _selectedCartItem;
            set
            {
                _selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }

        public string Subtotal => CalculateSubtotal().ToString("c");

        private decimal CalculateSubtotal() 
        {
            decimal subtotal = 0;

            foreach (CartItemModel item in Cart)
            {
                subtotal += item.Product.RetailPrice * item.QuantityInCart;
            }

            return subtotal;
        }

        public string Tax => CalculateTax().ToString("c");

        private decimal CalculateTax()
        {
            decimal tax = 0;

            foreach (CartItemModel item in Cart)
            {
                tax += item.Product.RetailPrice * item.QuantityInCart * (item.Product.TaxPercentage / 100m);
            }

            return tax;
        }

        public string Total
        {
            get
            {
                decimal total = CalculateSubtotal() + CalculateTax();
                return total.ToString("c");
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                if (ItemQuantity > 0 && (SelectedProduct?.AvailableQuantity >= ItemQuantity))
                {
                    output = true;
                }

                return output;
            }
        }

        public void AddToCart()
        {
            CartItemModel existingProduct = Cart.FirstOrDefault(x => x.Product.ProductName == SelectedProduct.ProductName);

            if (existingProduct == null)
            {
                Cart.Add(new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                });
            }
            else
            {
                existingProduct.QuantityInCart += ItemQuantity;
            }

            SelectedProduct.ReservedQuantity += ItemQuantity;
            if (SelectedProduct.AvailableQuantity == 0)
            {
                Products.Remove(SelectedProduct);
            }

            Cart.ResetBindings();
            Products.ResetBindings();
            NotifyOfPropertyChange(() => Subtotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanRemoveFromCart);
            NotifyOfPropertyChange(() => CanAddToCart);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = true;

                if (SelectedCartItem == null)
                {
                    output = false;
                }

                return output;
            }
        }

        public void RemoveFromCart()
        {
            ProductModel existingProduct = Products.FirstOrDefault(x => x.ProductName == SelectedCartItem.Product.ProductName);

            if (existingProduct == null)
            {
                Products.Add(SelectedCartItem.Product);
            }

            SelectedCartItem.Product.ReservedQuantity -= SelectedCartItem.QuantityInCart;
            Cart.Remove(SelectedCartItem);

            Cart.ResetBindings();
            Products.ResetBindings();
            NotifyOfPropertyChange(() => Subtotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanRemoveFromCart);
            NotifyOfPropertyChange(() => CanAddToCart);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = true;

                if (Cart.Count == 0)
                {
                    output = false;
                }

                return output;
            }
        }

        public void CheckOut()
        {
            //Todo handle check out
        }
    }
}
