﻿using System;
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

        [ForeignKey("RegularUser")]
        public string SaleID { get; set; }
        public RegularUser regularUser { get; set; }
        [Display(Name = "What are you selling")]
        public string Detail { get; set; }

        [StringLength(10000), Display(Name = "Describe it"), DataType(DataType.MultilineText)]
        public string Discription { get; set; }

        [Display(Name = "Product Category")]
        public string Category { get; set; }

        [Display(Name = "Select Gender")]
        public string Gender { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }

        [Display(Name = "Price")]
        public double? UnitPrice { get; set; }


    }
}
