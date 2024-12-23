using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialVintageApp.Models
{
    public class Store
    {
        public int StoreId { get; set; }

        public string StoreName { get; set; } = null!;

        public string Adress { get; set; } = null!;

        public int OptionId { get; set; }

        public string LogoExt { get; set; } = null!;

        public int CatagoryId { get; set; }
    }
}
