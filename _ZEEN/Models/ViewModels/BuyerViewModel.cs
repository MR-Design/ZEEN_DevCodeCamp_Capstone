using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _ZEEN.Models.ViewModels
{
    public class BuyerViewModel
    {
        public RegularUser user { get; set; }
        public List<RegularUser> users { get; set; }
        public Sale sale { get; set; }
        public List<Sale> sales { get; set; }
        public Shipment shipment { get; set; }
        public List<Shipment> shipments { get; set; }
    }
}
