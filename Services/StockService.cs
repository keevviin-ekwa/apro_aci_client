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
    class StockService
    {
        public static async Task<ResponseObject<List<Stock>>> GetAllStock(string Month)
        {
            ResponseObject<List<Stock>> respFromServer = new ResponseObject<List<Stock>>();
            var param= System.Web.HttpUtility.UrlEncode(Month);
            var url = EndPoint.getAllStockByMonth+param;
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            var test = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Stock>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<Stock>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

        public static async Task<ResponseObject<Stock>> SaveStock(StockDTO stockDTO)
        {
            ResponseObject<Stock> respFromServer = new ResponseObject<Stock>();
            var url = EndPoint.saveStock;
            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(url, stockDTO);

            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Stock>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
           else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<Stock>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

    }
}
