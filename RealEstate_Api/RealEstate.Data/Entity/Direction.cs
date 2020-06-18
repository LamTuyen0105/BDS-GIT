using RealEstate.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class Direction : BaseEntity
    {
        public string DirectionName { get; set; }

        public List<Property> Properties { get; set; }
    }
}
