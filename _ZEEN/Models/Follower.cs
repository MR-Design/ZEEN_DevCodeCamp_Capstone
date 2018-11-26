using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _ZEEN.Models
{
    public class Follower
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string FromId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string To { get; set; }

        public string Sender { get; set; }

    }
}
