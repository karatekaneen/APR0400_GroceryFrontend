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

namespace GroceryFrontend.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPage : Page
    {
        private Product productToEdit = null;
        public EditPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            productToEdit = (Product)e.Parameter;
            txtName.Text = productToEdit.Name;
            txtCategory.Text = productToEdit.Category;
            txtPrice.Text = productToEdit.Price.ToString();
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
                productToEdit.Price = double.Parse(txtPrice.Text);
                productToEdit.Name = name;
                productToEdit.Category = category;
                await Product.EditProduct(productToEdit);
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
    }
}
