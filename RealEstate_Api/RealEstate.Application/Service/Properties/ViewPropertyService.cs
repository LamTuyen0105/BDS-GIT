using RealEstate.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.Property;

namespace RealEstate.Application.Service.Properties
{
    public class ViewPropertyService : IViewPropertyService
    {
        private readonly RealEstateDbContext _context;
        public ViewPropertyService(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<List<PropertyViewModel>> GetAll()
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        select new { p, w, tt, d};
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
                        select new { p, w, tt, d };
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
