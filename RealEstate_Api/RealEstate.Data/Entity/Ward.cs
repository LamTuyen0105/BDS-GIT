using RealEstate.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class Ward : BaseEntity
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int DistrictId { get; set; }

        public District District { get; set; }
    }
}
