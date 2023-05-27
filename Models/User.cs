using System;

namespace Sign_Up_Form.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string DroitAcces { get; set; }
        public string MotDePasse { get; set; }
        public bool IsFirstConnection { get; set; }
        public string Email { get; set; }
        public string Fonction { get; set; }
        public string Tel { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateDerniereMaj { get; set; }
        public int Status { get; set; }
    }
}
