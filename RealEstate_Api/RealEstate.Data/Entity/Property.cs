using RealEstate.Data.Abstract;
using RealEstate.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class Property : BaseEntity
    {
        public string Title { get; set; }
        public string ApartmentNumber { get; set; }
        public string StreetNames { get; set; }
        public int WardId { get; set; }
        public string Image { get; set; }
        public double Area { get; set; }
        public double? AreaFrom { get; set; }
        public double? AreaTo { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double? Facade { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string Description { get; set; }
        public int? EvaluationStatusId { get; set; }
        public int? LegalPapersId { get; set; }
        public int TypeOfPropertyId { get; set; }
        public int TypeOfTransactionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? NumberOfStoreys { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public int? NumberOfWCs { get; set; }
        public Status Status { get; set; }
        public int? HouseDirectionId { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string ContactName { get; set; }
        public string EmailContact { get; set; }
        public string ContactPhone { get; set; }
        public Guid? UserID { get; set; }

        public Ward Ward { get; set; }
        public TypeOfProperty TypeOfProperty { get; set; }
        public TypeOfTransaction TypeOfTransaction { get; set; }
        public Direction Direction { get; set; }
        public LegalPaper LegalPaper { get; set; }
        public EvaluationStatus EvaluationStatus { get; set; }
        public AppUser AppUser { get; set; }

        public List<ImageProperty> ImageProperties { get; set; }
        public List<Favorite> Favorites { get; set; }

    }
}
