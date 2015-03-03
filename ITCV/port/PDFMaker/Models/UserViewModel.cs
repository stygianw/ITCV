using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPN.NewModel;

namespace PDFMaker.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public UserStatistics Stats { get; set; }
    }
}