namespace GroceryFrontend
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Net.Http;
    using System.Threading.Tasks;

    public partial class Product
    {
        private static string BASE_URL = @"http://localhost:50261/api/";
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public static async Task<List<Product>> GetProducts()
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

                }
                else
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
    }


}
