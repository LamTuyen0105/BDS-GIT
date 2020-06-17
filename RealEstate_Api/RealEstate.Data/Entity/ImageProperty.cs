using RealEstate.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class ImageProperty : BaseEntity
    {
        public string LinkName { get; set; }
        public int PropertyId { get; set; }

        public Property Property { get; set; }
    }
}
