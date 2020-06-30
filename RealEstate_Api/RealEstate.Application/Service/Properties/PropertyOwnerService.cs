using RealEstate.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.Property;
using Microsoft.AspNetCore.Http;
using RealEstate.ViewModels.Service.PropertyImage;
using RealEstate.Application.Common;
using RealEstate.Data.Entity;
using RealEstate.Utilities.Exceptions;
using System.Net.Http.Headers;
using System.IO;
using RealEstate.Data.Enum;
using System.Net;

namespace RealEstate.Application.Service.NewsManagers
{
    public class PropertyOwnerService : IPropertyOwnerService
    {
        private readonly RealEstateDbContext _context;
        private readonly IStorageService _storageService;

        public PropertyOwnerService (RealEstateDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int propertyId, PropertyImageCreateRequest request)
        {
            var propertyImage = new PropertyImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                PropertyId = propertyId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                propertyImage.LinkName = await this.SaveFile(request.ImageFile);
                propertyImage.FileSize = request.ImageFile.Length;
            }
            _context.PropertyImages.Add(propertyImage);
            await _context.SaveChangesAsync();
            return propertyImage.Id;
        }

        public async Task<int> Create(PropertyCreateRequest request)
        {
            var property = new Property()
            {
                Title = request.Title,
                ApartmentNumber = request.ApartmentNumber,
                StreetNames = request.StreetNames,
                WardId = request.WardId,
                Area = request.Area,
                AreaFrom = request.AreaFrom,
                AreaTo = request.AreaTo,
                Length = request.Length,
                Width = request.Width,
                Facade = request.Facade,
                Price = request.Price,
                PriceFrom = request.PriceFrom,
                PriceTo = request.PriceTo,
                Description = request.Description,
                EvaluationStatusId = request.EvaluationStatusId,
                LegalPapersId = request.LegalPapersId,
                TypeOfPropertyId = request.TypeOfPropertyId,
                TypeOfTransactionId = request.TypeOfTransactionId,
                StartDate = DateTime.Now,
                EndDate = request.EndDate,
                NumberOfStoreys = request.NumberOfStoreys,
                NumberOfBedrooms = request.NumberOfBedrooms,
                NumberOfWCs = request.NumberOfWCs,
                HouseDirectionId = request.HouseDirectionId,
                Lat = request.Lat,
                Lng = request.Lng,
                ContactName = request.ContactName,
                EmailContact = request.EmailContact,
                ContactPhone = request.ContactPhone,
                UserID = request.UserID,
            };
            if (request.ThumbnailImage != null)
            {
                property.PropertyImages = new List<PropertyImage>()
                {
                    new PropertyImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        LinkName = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property.Id;
        }

        public async Task<int> Delete(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null) throw new RealEstateException($"Không thể tìm thấy tin đăng: {propertyId}");
            var images = _context.PropertyImages.Where(i => i.PropertyId == propertyId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.LinkName);
            }

            _context.Properties.Remove(property);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<PropertyViewModel>> GetAllPaging(GetPostPropertyPagingRequest request)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Status == Status.Approved
                        select new { p, w, tt, d, j };
            if (request.TypeOfTransactionIds.Count > 0)
                query = query.Where(x => request.TypeOfTransactionIds.Contains(x.tt.Id));
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    Name = x.w.Name,
                    LinkName = x.j.LinkName,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            var pagedResult = new PagedResult<PropertyViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PropertyViewModel> GetById(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            var propertyViewModel = new PropertyViewModel()
            {
                Id = property.Id,
                Title = property.Title,
                Area = property.Area,
                AreaFrom = property.AreaFrom,
                AreaTo = property.AreaTo,
                Length = property.Length,
                Width = property.Width,
                Facade = property.Facade,
                Price = property.Price,
                PriceFrom = property.PriceFrom,
                PriceTo = property.PriceTo,
                Description = property.Description,
                NumberOfStoreys = property.NumberOfStoreys,
                NumberOfBedrooms = property.NumberOfBedrooms,
                NumberOfWCs = property.NumberOfWCs,
                Lat = property.Lat,
                Lng = property.Lng,
                ContactName = property.ContactName,
                EmailContact = property.EmailContact,
                ContactPhone = property.ContactPhone
            };
            return propertyViewModel;
        }

        public async Task<PropertyImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.PropertyImages.FindAsync(imageId);
            if (image == null)
                throw new RealEstateException($"Không thể tìm thấy hình: {imageId}");

            var viewModel = new PropertyImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                LinkName = image.LinkName,
                IsDefault = image.IsDefault,
                PropertyId = image.PropertyId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<PropertyImageViewModel>> GetListImages(int propertyId)
        {
            return await _context.PropertyImages.Where(x => x.PropertyId == propertyId)
                .Select(i => new PropertyImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    LinkName = i.LinkName,
                    IsDefault = i.IsDefault,
                    PropertyId = i.PropertyId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var propertyImage = await _context.PropertyImages.FindAsync(imageId);
            if (propertyImage == null)
                throw new RealEstateException($"Không thể tìm thấy hình: {imageId}");
            _context.PropertyImages.Remove(propertyImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(PropertyUpdateRequest request)
        {
            var property = await _context.Properties.FindAsync(request.Id);
            if (property == null) throw new RealEstateException($"Không thể tìm thấy mã tin: {request.Id}");
            property.Title = request.Title;
            property.ApartmentNumber = request.ApartmentNumber;
            property.StreetNames = request.StreetNames;
            property.WardId = request.WardId;
            property.Area = request.Area;
            property.AreaFrom = request.AreaFrom;
            property.AreaTo = request.AreaTo;
            property.Length = request.Length;
            property.Width = request.Width;
            property.Facade = request.Facade;
            property.Price = request.Price;
            property.PriceFrom = request.PriceFrom;
            property.PriceTo = request.PriceTo;
            property.Description = request.Description;
            property.EvaluationStatusId = request.EvaluationStatusId;
            property.LegalPapersId = request.LegalPapersId;
            property.NumberOfStoreys = request.NumberOfStoreys;
            property.NumberOfBedrooms = request.NumberOfBedrooms;
            property.NumberOfWCs = request.NumberOfWCs;
            property.HouseDirectionId = request.HouseDirectionId;
            property.Lat = request.Lat;
            property.Lng = request.Lng;
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.PropertyImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.PropertyId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.LinkName = await this.SaveFile(request.ThumbnailImage);
                    _context.PropertyImages.Update(thumbnailImage);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, PropertyImageUpdateRequest request)
        {
            var propertyImage = await _context.PropertyImages.FindAsync(imageId);
            if (propertyImage == null)
                throw new RealEstateException($"Không thể tìm thấy hình: {imageId}");

            if (request.ImageFile != null)
            {
                propertyImage.LinkName = await this.SaveFile(request.ImageFile);
                propertyImage.FileSize = request.ImageFile.Length;
            }
            _context.PropertyImages.Update(propertyImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<PropertyViewModel>> GetAll()
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Status == Status.Approved
                        select new { p, w, tt, d, j };
            var data = await query.Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    Name = x.w.Name,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    LinkName = x.j.LinkName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            return data;
        }

        public async Task<PagedResult<PropertyViewModel>> GetAllByTypeOfTransaction(GetViewPropertyPagingRequest request)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        select new { p, w, tt, d, j };
            if (request.typeOfTransactionId.HasValue && request.typeOfTransactionId >0)
                query = query.Where(x => x.p.TypeOfTransactionId == request.typeOfTransactionId);

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    Name = x.w.Name,
                    LinkName = x.j.LinkName,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            var pagedResult = new PagedResult<PropertyViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<PagedResult<PropertyViewModel>> Find(GetFindPropertyPagingRequest request)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join ds in _context.Districts on w.DistrictId equals ds.Id
                        join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Status == Status.Approved
                        select new { p, w, tt, d, tp, ds, pc, j };
            if (request.typeOfTransactionId > 0)
                query = query.Where(x => x.p.TypeOfTransactionId == request.typeOfTransactionId);
            if (request.typeOfPropertyId.HasValue && request.typeOfPropertyId > 0)
                query = query.Where(x => x.p.TypeOfPropertyId == request.typeOfPropertyId);
            if (request.price.HasValue && request.price > 0)
                query = query.Where(x => x.p.Price <= request.price);
            if (request.area.HasValue && request.area > 0)
                query = query.Where(x => x.p.Area <= request.area);
            if (request.houseDirectionId.HasValue && request.houseDirectionId > 0)
                query = query.Where(x => x.p.HouseDirectionId == request.houseDirectionId);
            if (request.numberOfBedrooms.HasValue && request.numberOfBedrooms > 0)
                query = query.Where(x => x.p.NumberOfBedrooms <= request.numberOfBedrooms);
            if (request.evaluationStatusId.HasValue && request.evaluationStatusId > 0)
                query = query.Where(x => x.p.EvaluationStatusId == request.evaluationStatusId);
            if (request.provinceId.HasValue && request.provinceId > 0)
                query = query.Where(x => x.pc.Id == request.provinceId);
            if (request.districtId.HasValue && request.districtId > 0)
                query = query.Where(x => x.ds.Id == request.districtId);
            if (request.wardId.HasValue && request.wardId > 0)
                query = query.Where(x => x.p.WardId == request.wardId);

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    Name = x.w.Name,
                    LinkName = x.j.LinkName,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            var pagedResult = new PagedResult<PropertyViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<PropertyViewModel>> GetPaging(GetPropertyPagingRequest request)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        select new { p, w, tt, d, j };

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    Name = x.w.Name,
                    LinkName = x.j.LinkName,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            var pagedResult = new PagedResult<PropertyViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
    }
}
