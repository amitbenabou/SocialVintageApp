using SocialVintageApp.Services;
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

        
        public int CatagoryId { get; set; }
        public string ProfileImagePath { get; set; } = "";
        public string FullProfileImagePath
        {
            get
            {
                return SocialVintageWebAPIProxy.ImageBaseAddress + ProfileImagePath;
            }
        }
    }
}
