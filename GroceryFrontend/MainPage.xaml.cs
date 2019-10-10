using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GroceryFrontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            lstProducts.ItemsSource = await Product.GetProducts();
        }

        private void btnCreateProduct_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Create");
            Product p = (Product)lstProducts.SelectedItem;
            System.Diagnostics.Debug.WriteLine(p.Name);
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Delete");
            if (lstProducts.SelectedIndex != -1)
            {
                Product p = (Product)lstProducts.SelectedItem;

                Frame.Navigate(typeof(DeletePage), p);
            }
            else return;

        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Edit");

        }
    }
}
