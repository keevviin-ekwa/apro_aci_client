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
    public class MatiereService
    {
        public static async Task<ResponseObject<List<Matiere>>> GetAllMatieres()
        {
            ResponseObject<List<Matiere>> respFromServer = new ResponseObject<List<Matiere>>();
            var url = EndPoint.getAllMatiere;
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Matiere>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Matiere>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

        public static async Task<ResponseObject<Matiere>> GetMatiereById(int Id)
        {
            ResponseObject<Matiere> respFromServer = new ResponseObject<Matiere>();
            var url = EndPoint.getMatiereById+Id;
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Matiere>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Matiere>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }


        public static async Task<ResponseObject<Matiere>> SaveMatiere(CreateMatiereDTO createMatiereDTO)
        {
            ResponseObject<Matiere> respFromServer = new ResponseObject<Matiere>();
            var url = EndPoint.saveMatiere;
            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(url, createMatiereDTO);

            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Matiere>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Matiere>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }



    }
}
