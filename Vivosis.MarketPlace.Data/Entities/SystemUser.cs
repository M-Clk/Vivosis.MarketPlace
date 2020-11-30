using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vivosis.MarketPlace.Data.Entities
{
    public class SystemUser:IdentityUser<int>
    {
        public DateTime ExpireTime { get; set; }
        public bool Status { get; set; }
        [Required]
        public string DbName { get; set; }
        [Required]
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        [Required]
        public string Server { get; set; }
        public virtual IList<Store> Stores { get; set; }
    }
}
