using RealEstate.Data.EF;
using RealEstate.Data.Enum;
using RealEstate.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.NewsManagers
{
    public class PropertyNewsManagerService : IPropertyNewsManagerService
    {
        private readonly RealEstateDbContext _context;

        public PropertyNewsManagerService (RealEstateDbContext context)
        {
            _context = context;
        }        

        public async Task<bool> UpdateStatus(int propertyId, Status newStatus)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null) throw new RealEstateException($"Không thể tìm thấy mã: {propertyId}");
            property.Status = newStatus;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}