using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialVintageApp.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        public string Size { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Color { get; set; } = null!;

        public string Price { get; set; } = null!;

        public int StoreId { get; set; }

        public string ItemInfo { get; set; } = null!;

        public virtual ICollection<string> ItemsImages { get; set; } = new List<string>();

        public virtual Store Store { get; set; } = null!;

        public Item() { }
    }
}
