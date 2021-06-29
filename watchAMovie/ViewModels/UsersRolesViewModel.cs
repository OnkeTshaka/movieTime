using watchAMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace watchAMovie.ViewModels
{
    public class UsersRolesViewModel
    {
        public ICollection<ApplicationUser> Adminstrators { get; set; }
        public ICollection<ApplicationUser> Managers { get; set; }
        public ICollection<ApplicationUser> Members { get; set; }
    }
}