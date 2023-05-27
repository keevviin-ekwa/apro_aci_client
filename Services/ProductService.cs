using Newtonsoft.Json;
using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Utils;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Sign_Up_Form.Services
{
    public class ProductService
    {
        public static async Task<ResponseObject<List<Produit>>> GetAllProduits()
        {
            ResponseObject<List<Produit>> respFromServer = new ResponseObject<List<Produit>>();
            var url = EndPoint.getAllProducts;
            var client = new HttpClient();
          
            var response = await client.GetAsync(url);
            var test = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Produit>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Produit>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }


        public static async Task<ResponseObject<Produit>> SaveProduit(CreateProductDTO createProductDTO)
        {
            ResponseObject<Produit> respFromServer = new ResponseObject<Produit>();
            var url = EndPoint.saveProduit;
            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(url,createProductDTO);
            
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Produit>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {

            }
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Produit>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

        public static async Task<ResponseObject<Produit>> updateProduit(Produit produit)
        {
            ResponseObject<Produit> respFromServer = new ResponseObject<Produit>();
            var url = EndPoint.updateProduit+produit.id;
            var client = new HttpClient();

            var response = await client.PutAsJsonAsync(url, produit);

            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Produit>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
           else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Produit>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }



    }
}
