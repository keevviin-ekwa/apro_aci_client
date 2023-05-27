using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sign_Up_Form.Models
{
    public class Produit
    {
        public int id { get; set; }

   
        public string Reference { get; set; }

      
        public string Designation { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime DateModification { get; set; }


        public virtual ICollection<Stock> Stocks { get; set; }



        public virtual ICollection<Matiere> Matieres { get; set; }

       public virtual ICollection<MatiereProduit> MatiereProduits { get; }
    }
}
