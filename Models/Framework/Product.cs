namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        [StringLength(255)]
        public string ProductImg { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        public int Stock { get; set; }

        public decimal? Rating { get; set; }

        public DateTime? CreatedAt { get; set; }

       
        public string BrandName { get; set; }

        public string CategoryName { get; set; }

        // Các thuộc tính điều hướng để truy cập đối tượng Brand và Category
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        
    }
}
