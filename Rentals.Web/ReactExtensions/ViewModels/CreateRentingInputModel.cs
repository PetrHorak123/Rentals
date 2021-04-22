using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.ReactExtensions.ViewModels
{
    public class CreateRentingInputModel
    {
        public int userId { get; set; }
        public string startsAt { get; set; }
        public string endsAt { get; set; }
        public int[] items { get; set; }
        public int state { get; set; }
        public string note { get; set; }
    }
}
