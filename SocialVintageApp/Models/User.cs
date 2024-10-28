using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialVintageApp.Models
{
    public class User
    {
        
            public int UserId { get; set; }

            public string UserName { get; set; } = null!;

            public string UserMail { get; set; } = null!;

            public string? UserAdress { get; set; }

            public bool HasStore { get; set; }

            public string Pswrd { get; set; } = null!;

        
    }
}
