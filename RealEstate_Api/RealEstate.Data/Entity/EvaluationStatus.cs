using RealEstate.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class EvaluationStatus : BaseEntity
    {
        public string EvaluationStatusName { get; set; }

        public List<Property> Properties { get; set; }
    }
}
