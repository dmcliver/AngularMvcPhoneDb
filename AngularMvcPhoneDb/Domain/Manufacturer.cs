using System;
using System.Collections.Generic;

namespace AngularMvcPhoneDb.Core.Domain
{
    public class Manufacturer
    {
        public String Name { get; set; }
        public virtual ICollection<SmartPhone> SmartPhones { get; set; } 
    }
}
