using RealEstate.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class Province : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public List<District> Districts { get; set; }
    }
}
