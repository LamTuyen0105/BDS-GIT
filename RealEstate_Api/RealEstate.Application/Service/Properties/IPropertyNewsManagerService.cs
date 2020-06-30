using RealEstate.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.NewsManagers
{
    public interface IPropertyNewsManagerService
    {
        Task<bool> UpdateStatus(int propertyId, Status newStatus);
        
    }
}