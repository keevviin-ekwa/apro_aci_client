using System;

namespace Sign_Up_Form.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public double StockDebut { get; set; }

        public double StockFin { get; set; }

        public string Mois { get; set; }

        public double Livraison { get; set; }

        public int Commande { get; set; }
        public double Consommation { get; set; }    
        public int MatiereId { get; set; }

        public Matiere Matiere { get; set; }
        public DateTime DateCreation { get; set; }

        public DateTime DateModification { get; set; }

    }
}
