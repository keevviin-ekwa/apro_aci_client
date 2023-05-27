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
    class MarqueService
    {
        public static async Task<ResponseObject<List<Marque>>> GetAllMarques()
        {
            ResponseObject<List<Marque>> respFromServer = new ResponseObject<List<Marque>>();
            var url = EndPoint.getAllMarques;
            var client = new HttpClient();
            var test= await client.GetStringAsync(url);
            var response = await client.GetAsync(url);
            
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Marque>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Marque>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }
    }
}
