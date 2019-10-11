using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroceryFrontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeletePage : Page
    {
        private Product productToDelete = null;
        public DeletePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            productToDelete = (Product)e.Parameter;
            txtName.Text = productToDelete.Name;
            txtCategory.Text = productToDelete.Category;
            txtPrice.Text = productToDelete.Price.ToString();
        }

        private async void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            string name, category, rawPrice;
            double price;

            name = txtName.Text;
            category = txtCategory.Text;
            rawPrice = txtPrice.Text;

            if (name.Length > 0 && category.Length > 0 && rawPrice.Length > 0)
            {
                productToDelete.Price = double.Parse(txtPrice.Text);
                productToDelete.Name = name;
                productToDelete.Category = category;
                await Product.EditProduct(productToDelete);
                Frame.Navigate(typeof(MainPage));

            }
            else return;

        }

        private void TextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            // Function to check that the input can be casted to a valid double
            args.Cancel = (
                args.NewText.Any(c => (!char.IsDigit(c) && !char.IsPunctuation(c))) || // All chars is either number or punctuation
                args.NewText.Where(c => char.IsPunctuation(c)).Count() > 1 // And we only allow one punctuation.
            );
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void btnRemoveProduct_Click(object sender, RoutedEventArgs e)
        {            
            await Product.DeleteProduct(productToDelete.Id);
            Frame.Navigate(typeof(MainPage));
        }
    }
}
