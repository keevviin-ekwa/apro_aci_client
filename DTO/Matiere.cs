using System;

namespace Sign_Up_Form.DTO
{
    public class MatiereDTO
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
    }

    public class CreateMatiereDTO
    {


        public string Reference { get; set; }
        public string Designation { get; set; }
        public string Origine { get; set; }


        public double PrixUnitaire { get; set; }


        public double Colissage { get; set; }


        public int DelaisAppro { get; set; }
        public DateTime DateCreation { get; set; }
       public DateTime DateModification { get; set; }



    }

    public class UpdateMatiereDTO
    {
        public int Id { get; set; }


        public string Reference { get; set; }

        public string Designation { get; set; }
        public string Origine { get; set; }


        public double PrixUnitaire { get; set; }


        public double Colissage { get; set; }


        public int DelaisAppro { get; set; }


    }
}
