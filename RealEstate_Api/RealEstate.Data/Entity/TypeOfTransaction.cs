using RealEstate.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class TypeOfTransaction : BaseEntity
    {
        public string TypeOfTransactionName { get; set; }

        public List<Property> Properties { get; set; }
    }
}
