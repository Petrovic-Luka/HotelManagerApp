using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public interface IGuest
    {
        public int Guest_Id { get; init; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone_Number { get; set; }

        public string Display
        {
            get { return $"{Name} Email: {Email} Phone:{Phone_Number}"; }
        }
    }
}
