using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialVintageApp.Models
{
    public class Shipping
    {
        public Shipping() { }
        public int OptionId { get; set; }

        public string OptionName { get; set; } = null!;

        

    }

    public class Status
    {
        public Status() { }
        public int StatusId { get; set; }

        public string StatusName { get; set; } = null!;

        

    }

    public class Catagory
    {
        public Catagory() { }
        public int CatagoryId { get; set; }

        public string CatagoryName { get; set; } = null!;

        

    }


    public class BasicData
    {
        public List<Catagory> Catagories { get; set; }
        public BasicData() { }
        public List<Status> Statuss { get; set; }
        public List<Shipping> Shippings { get; set; }
    }
}
