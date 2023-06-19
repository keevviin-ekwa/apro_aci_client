using Newtonsoft.Json;
using Sign_Up_Form.Models;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sign_Up_Form.Services
{
    public class CommandeService
    {
        public static async Task<ResponseObject<List<Commande>>> GetAllCommandes()
        {
            ResponseObject<List<Commande>> respFromServer = new ResponseObject<List<Commande>>();
            var url = EndPoint.getAllCommandes;
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Commande>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Commande>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }


        public static async Task<ResponseObject<Commande>> deleteCommande(int Id)
        {
            ResponseObject<Commande> respFromServer = new ResponseObject<Commande>();
            var url = EndPoint.deleteCommande + Id;
            var client = new HttpClient();

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Commande>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Commande>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }


    }
}
