using Newtonsoft.Json;
using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using Sign_Up_Form.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sign_Up_Form.Services
{
    public class ObjectifService
    {
        public static async Task<ResponseObject<List<Objectif>>> GetAllOBjectif()
        {
            ResponseObject<List<Objectif>> respFromServer = new ResponseObject<List<Objectif>>();
            var url = EndPoint.getAllObjectif;
            var client = new HttpClient();
        
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Objectif>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Objectif>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }



        public static async Task<ResponseObject<List<Objectif>>> GetOBjectifByYear(string year)
        {
            ResponseObject<List<Objectif>> respFromServer = new ResponseObject<List<Objectif>>();
            var url = EndPoint.getObjectifByYear+year;
            var client = new HttpClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Objectif>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Objectif>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

        public static async Task<ResponseObject<Objectif>> GetOBjectifByMonth(string Month)
        {
            ResponseObject<Objectif> respFromServer = new ResponseObject<Objectif>();
            var param = System.Web.HttpUtility.UrlEncode(Month);
            var url = EndPoint.getObjectifByMonth+param;
            var client = new HttpClient();
          //  var response = await client.GetStringAsync(url);
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Objectif>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Objectif>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }


        public static async Task<ResponseObject<Objectif>> SaveObjectif(ObjectifDTO objectifDTO)
        {
            ResponseObject<Objectif> respFromServer = new ResponseObject<Objectif>();
            var url = EndPoint.saveObjectif;
            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(url, objectifDTO);

            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Objectif>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Objectif>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

        public static async Task<ResponseObject<Objectif>> UpdateObjectif(ObjectifDTO objectifDTO)
        {
            ResponseObject<Objectif> respFromServer = new ResponseObject<Objectif>();
            var url = EndPoint.updateObjectif;
            var client = new HttpClient();

            var response = await client.PutAsJsonAsync(url, objectifDTO);

            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Objectif>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Objectif>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }
    }
}
