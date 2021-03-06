﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.Service.Property
{
    public class PropertyUpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ApartmentNumber { get; set; }
        public string StreetNames { get; set; }
        public int WardId { get; set; }
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
        public int LegalPapersId { get; set; }
        public string NumberOfStoreys { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public int? NumberOfWCs { get; set; }
        public int? HouseDirectionId { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
