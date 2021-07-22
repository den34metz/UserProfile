using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserProfile.Models;

namespace UserProfile.ViewModels
{
    public class TeamPocsViewModel
    {
        public IEnumerable<PocsModel> Pocs { get; set; }
        public IEnumerable<TeamsModel> Teams { get; set; }

        public string Division { get; set; }
    }
}