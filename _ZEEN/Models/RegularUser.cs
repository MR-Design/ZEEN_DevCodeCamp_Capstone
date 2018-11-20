using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _ZEEN.Models
{
    public class RegularUser
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public byte[] AvatarImage { get; set; }

        [Display(Name = "Select Gender")]
        public string Gender { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Link to your website")]
        public string WebSite { get; set; }


        [Display(Name = "Bio")]
        [StringLength(10000), DataType(DataType.MultilineText)]
        public string Bio { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        [Display(Name = "Zip code")]
        public int ZipCode { get; set; }
    }
}
