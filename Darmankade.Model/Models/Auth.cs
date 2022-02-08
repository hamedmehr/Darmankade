using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darmankade.Model.Models
{
    [Serializable]
    public class Auth : BaseEntity
    {
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
