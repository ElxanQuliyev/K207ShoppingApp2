using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Web.Models
{
    public class Product
    {
        public int ID { get; set; }
     
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public bool isNew { get; set; }
        public bool  IsFeatured { get; set; }
        public DateTime PublishDate { get; set; }
        public int Count { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<ProductPicture> ProductPictures { get; set; }
    }
}
