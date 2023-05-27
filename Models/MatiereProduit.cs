namespace Sign_Up_Form.Models
{


    public class MatiereProduit
    {
        public int ProduitId { get; set; }

        public int MatiereId { get; set; }

        public double ContributionMatiereGF { get; set; }

        public double ContributionMatierePF { get; set; }

        public string DateLivration {get;set;}
        public Produit Produit { get; set; }

        public Matiere Matiere { get; set; }
    }
}
