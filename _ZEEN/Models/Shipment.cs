using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _ZEEN.Models
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string SellerID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string BuyerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public int ZipCode { get; set; }

        public bool Shipped { get; set; }
    }
}

