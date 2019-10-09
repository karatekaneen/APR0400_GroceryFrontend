using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GroceryFrontend.Models
{
    public class ProductViewModel
    {
        internal ObservableCollection<Product> observableProducts = new ObservableCollection<Product>();
        private string BASE_URL = @"http://localhost:50261/api/";
        private List<Product> allProducts = new List<Product>();

        #region props

        public ObservableCollection<Product> Products { get
            {
                return this.observableProducts;
            } 
        }

        public string AppTitle { get { return "Grosshandlarens Produkter"; } }
        #endregion

        #region Methods

        public async void LoadData()
        {
            // Den här fungerar fint
            Product test = new Product { Category = "Dairy", Name = "Butter", Price = 25.00 };
            observableProducts.Add(test);

            // Produkterna kommer rätt
            allProducts = await GetProducts();

            Product firstProduct = allProducts[0]; // För debug
            System.Diagnostics.Debug.WriteLine(firstProduct); // Den här är korrekt

            // Men när jag försöker lägga till dem i min ObservableCollection så får jag detta fel:
            //allProducts.ForEach(product => this.observableProducts.Add(product));
            // 		Message	"Programmet anropade ett gränssnitt som ordnats för en annan tråd. (Exception from HRESULT: 0x8001010E (RPC_E_WRONG_THREAD))"	string

            // Det fungerar inte heller när man sparar i en lokal variabel och sedan försöker i en annan funktion som inte är async som nedan:
            //AddProductsToCollection();

            // Det fungerar inte heller att skapa nya produkter som jag gjorde ovan i stil med:
            //allProducts.ForEach(product => this.observableProducts.Add(new Product { Name = product.Name, Category = product.Category, Price = product.Price, Id = product.Id }));

        }

        public void AddProductsToCollection()
        {
            this.allProducts.ForEach(product => this.observableProducts.Add(product));
        }
           
        private async Task<List<Product>> GetProducts()
        {
            try
            {
                string URL = BASE_URL + "products";
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(URL));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                   return JsonConvert.DeserializeObject<List<Product>>(content);

                } else
                {
                    Exception e = new Exception("request failed");
                    throw e;
                }
            }
            catch (Exception ex)
            {
                //ToDo Give errormessage to user and possibly log error
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }
        }
        #endregion
    }
}
