namespace Sign_Up_Form.Models
{
    public class Objectif
    {
        public int Id { get; set; }
        public double ObjectifGF { get; set; }

        public double ObjectifPF { get; set; }
        
        public Produit Produit { get; set; }
        public string Mois { get; set; }
    }
}
