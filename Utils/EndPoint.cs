using System;
using System.Collections.Generic;
using System.Text;

namespace Sign_Up_Form.Utils
{
    public static class EndPoint
    {
        public  const  string getAllMarques= Configuration.API_DOMAIN + "/Marques";
        public  const string getAllProducts = Configuration.API_DOMAIN + "/Produit";
        public const string saveProduit = Configuration.API_DOMAIN + "/Produit";
        public const string updateProduit = Configuration.API_DOMAIN + "/Produit/updateProduct/";



        public const string getAllMatiere = Configuration.API_DOMAIN + "/Matiere";
        public const string getMatiereById = Configuration.API_DOMAIN + "/Matiere/GetMatiereById/";
        public const string saveMatiere = Configuration.API_DOMAIN + "/Matiere";
        public const string getAllMatiereByProductId = Configuration.API_DOMAIN + "/MatiereProduit/GetMatiereByProductId/";
        public const string getAllMatiereProduit = Configuration.API_DOMAIN + "/MatiereProduit";

        public const string saveMatiereProduit = Configuration.API_DOMAIN + "/MatiereProduit";

        public const string getAllStockByMonth = Configuration.API_DOMAIN + "/Stock/Month?Month=";
        public const string saveStock = Configuration.API_DOMAIN + "/Stock";
        public const string updateStock = Configuration.API_DOMAIN + "/Stock";


        //Endpoint objectif

        public const string getAllObjectif = Configuration.API_DOMAIN + "/Objectif";
        public const string saveObjectif = Configuration.API_DOMAIN + "/Objectif";
        public const string getObjectifByYear = Configuration.API_DOMAIN + "/Objectif/getObjectifByyear/";
        public const string updateObjectif = Configuration.API_DOMAIN + "/Objectif";
        public const string getObjectifByMonth = Configuration.API_DOMAIN + "/Objectif/Month?Month=";
     

        //user
        public const string getAllUser = Configuration.API_DOMAIN + "/User/GetAllUser";
        public const string getUserById = Configuration.API_DOMAIN + "/User/GetUserById/";
        public const string saveUser = Configuration.API_DOMAIN + "/User/saveUser";
        public const string updateUser = Configuration.API_DOMAIN + "/User/updateUser";
        public const string changePassword = Configuration.API_DOMAIN + "/User/changeUserPassword";
        public const string getAllDroits = Configuration.API_DOMAIN + "/User/GetAllDroitsAcces";

        public const string loginUser = Configuration.API_DOMAIN + "/Login/connectAndGetCredentials";

        //Commande

        public const string getAllCommandes = Configuration.API_DOMAIN + "/Commande";
        public const string deleteCommande = Configuration.API_DOMAIN + "/Commande/deleteCommande/";
    }
}
