

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
    public class UserService
    {

        public static async Task<ResponseObject<List<User>>> GetAllUser()
        {
            ResponseObject<List<User>> respFromServer = new ResponseObject<List<User>>();
           
            var url = EndPoint.getAllUser;
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            var test = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<User>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<User>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }

        public static async Task<ResponseObject<User>> GetUserById(int Id)
        {
            ResponseObject<User> respFromServer = new ResponseObject<User>();

            var url = EndPoint.getUserById+Id;
            var client = new HttpClient();

            var response = await client.GetAsync(url);
            var test = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }


        public static async Task<ResponseObject<User>> SaveUser(User user)
        {
            ResponseObject<User> respFromServer = new ResponseObject<User>();
            var url = EndPoint.saveUser;
            var client = new HttpClient();

            var response = await client.PostAsJsonAsync(url, user);

            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
           else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }



        public static async Task<ResponseObject<User>> connectUser(LoginUserDTO userToConnect)
        {
            try
            {
                ResponseObject<User> respFromServer = new ResponseObject<User>();
                string json = JsonConvert.SerializeObject(userToConnect);
                //Needed to setup the body of the request
               
                var url = EndPoint.loginUser;
                var client = new HttpClient();
                //Pass in the full URL and the json string content
                var response = await client.PostAsJsonAsync(url, userToConnect);

                if (response.IsSuccessStatusCode)
                {
                    respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                    ConnectedUserModel user = new ConnectedUserModel();
                    user.userConnected = respFromServer.Data;
                  
                    user.accessRights = Helpers.convertStringToList(user.userConnected.DroitAcces);
                    Helpers.connectedUserModel = user;
                    client.Dispose();
                    return respFromServer;
                }
                else
                {
                    //respFromServer = JsonConvert.DeserializeObject<ResponseObject<Employe>>(await response.Content.ReadAsStringAsync());
                    client.Dispose();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception = " + ex.Message);
                throw ex;
            }
        }



        public static async Task<ResponseObject<User>> UpdateUserPassword(ChangePasswordDTO pwdChangeModel)
        {
            ResponseObject<User> respFromServer = new ResponseObject<User>();
            string json = JsonConvert.SerializeObject(pwdChangeModel);
          //  StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = EndPoint.changePassword;
            var client = new HttpClient();

            //Pass in the full URL and the json string content
            var response = await client.PostAsJsonAsync(url, pwdChangeModel);

            if (response.IsSuccessStatusCode)
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }



        public static async Task<ResponseObject<List<DroitsAcces>>> GetAllDroitsAcces()
        {
            ResponseObject<List<DroitsAcces>> respFromServer = new ResponseObject<List<DroitsAcces>>();
            var url = EndPoint.getAllDroits;
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<DroitsAcces>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<List<DroitsAcces>>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }




        public static async Task<ResponseObject<User>> UpdateUser(User user)
        {
            ResponseObject<User> respFromServer = new ResponseObject<User>();
            var url = EndPoint.updateUser;
            var client = new HttpClient();

            var response = await client.PutAsJsonAsync(url, user);

            if (response.IsSuccessStatusCode)
            {

                respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
            else
            {
                respFromServer = JsonConvert.DeserializeObject<ResponseObject<User>>(await response.Content.ReadAsStringAsync());
                client.Dispose();
                return respFromServer;
            }
        }
    }
}
