using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sign_Up_Form.Models
{
    public class Matiere
    {
        public int Id { get; set; }

        public string Reference { get; set; }


      
        public string Designation { get; set; }

     
        public string Origine { get; set; }

        public double PrixUnitaire { get; set; }

        public double Colissage { get; set; }

 
        public int DelaisAppro { get; set; }


        public DateTime DateCreation { get; set; }

        public DateTime DateModification { get; set; }

        public List<Commande> Commandes { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<Produit> Produits { get; set; }
        public List<MatiereProduit> MatiereProduits { get; }
    }
}
