using System;

namespace Sign_Up_Form.DTO
{
    public class ProductDTO
    {

        public int Id { get; set; }
        public string Reference { get; set; }
        public string Designation { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime DateModification { get; set; }
    }

    public class CreateProductDTO
    {
        public string Reference { get; set; }
        public string Designation { get; set; }
        public int MarqueId { get; set; }
    }


    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Designation { get; set; }
    }


}
