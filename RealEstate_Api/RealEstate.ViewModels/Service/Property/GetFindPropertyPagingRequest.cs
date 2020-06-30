using RealEstate.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.Service.Property
{
    public class GetFindPropertyPagingRequest : PagingRequestBase
    {
        public int typeOfTransactionId { get; set; }
        public int? typeOfPropertyId { get; set; }
        public decimal? price { get; set; }
        public double? area { get; set; }
        public int? houseDirectionId { get; set; }
        public int? numberOfBedrooms { get; set; }
        public int? evaluationStatusId { get; set; }
        public int? provinceId { get; set; }
        public int? districtId { get; set; }
        public int? wardId { get; set; }
    }
}
