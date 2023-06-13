using System;

namespace Sign_Up_Form.DTO
{
    public class StockDTO
    {
        public int Id { get; set; }

        public double StockDebut { get; set; }

        public double StockFin { get; set; }

        public string Mois { get; set; }

        
        public int MatiereId { get; set; }

        public DateTime date { get; set; }

    }

    public class UpdateStockDTO
    {
        public int Id { get; set; }

        public double StockDebut { get; set; }

        public double StockFin { get; set; }

        public string Mois { get; set; }


        public int MatiereId { get; set; }

        

    }
}
