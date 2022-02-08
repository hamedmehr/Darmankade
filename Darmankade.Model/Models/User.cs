using System;

namespace Darmankade.Model.Models
{
    [Serializable]
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
