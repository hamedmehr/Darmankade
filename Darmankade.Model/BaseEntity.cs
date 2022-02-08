using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darmankade.Model
{
    [Serializable]
    public class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
