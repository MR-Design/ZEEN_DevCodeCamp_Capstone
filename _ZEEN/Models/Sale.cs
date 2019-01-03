using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _ZEEN.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("RegularUser")]
        //public int RegularUserID { get; set; }
        //public RegularUser regularUser { get; set; }


        [ForeignKey("ApplicationUser")]
        public string SaleID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string BuyerID { get; set; }
        [Display(Name = "Title")]
        public string Detail { get; set; }

        [StringLength(10000), Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string Discription { get; set; }

        [Display(Name = "Product Category")]
        public string Category { get; set; }

        [Display(Name = "Select Gender")]
        public string Gender { get; set; }
        public int Quantity { get; set; }
        public double? Size { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }

        [Display(Name = "Price")]
        public double UnitPrice { get; set; }

        public string Statu { get; set; }
      

        public string Image { get; set; }

    }
}
