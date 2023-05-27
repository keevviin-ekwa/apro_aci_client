using Sign_Up_Form.DTO;

namespace Sign_Up_Form.DTO
{
    public class MatiereProduitDTO
    {
        public int ProduitId { get; set; }

        public int MatiereId { get; set; }

        public double ContributionMatiereGF { get; set; }

        public double contributionMatierePF { get; set; }
        
    }

    public class ResultMatiereProduitDTO
    {
        public ProductDTO Produit { get; set; }

        public MatiereDTO Matiere { get; set; }

        public double ContributionMatiere { get; set; }
    }
}
