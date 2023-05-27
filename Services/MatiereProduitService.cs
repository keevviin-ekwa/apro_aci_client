using Newtonsoft.Json;
using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Sign_Up_Form.Services
{
    internal class MatiereProduitService
    {
        public static async Task<ResponseObject<List<MatiereProduit>>> GetAllMatieresByProduct(int Id)
        {
            ResponseObject<List<MatiereProduit>> respFromServer = new ResponseObject<List<MatiereProduit>>();
            var url = EndPoint.getAllMatiereByProductId+Id;
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<MatiereProduit>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<MatiereProduit>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }


        public static async Task<ResponseObject<List<MatiereProduit>>> GetAllMatieresProduit()
        {
            ResponseObject<List<MatiereProduit>> respFromServer = new ResponseObject<List<MatiereProduit>>();
            var url = EndPoint.getAllMatiereProduit;
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<MatiereProduit>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<MatiereProduit>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

        public static async Task<ResponseObject<MatiereProduit>> SaveMatiereProduit(MatiereProduitDTO matierePoduitDto)
        {
            ResponseObject<MatiereProduit> respFromServer = new ResponseObject<MatiereProduit>();
            var url = EndPoint.saveMatiereProduit;
            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(url, matierePoduitDto);

            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<MatiereProduit>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else

            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<MatiereProduit>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

    }
}
