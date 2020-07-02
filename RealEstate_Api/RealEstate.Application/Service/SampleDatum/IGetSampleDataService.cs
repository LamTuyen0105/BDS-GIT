using RealEstate.ViewModels.Service.SampleData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.SampleDatum
{
    public interface IGetSampleDataService
    {
        Task<List<CommonViewModel>> GetProvince();
        Task<List<DistrictWardViewModel>> GetDistrict(GetDistrictRequest request);
        Task<List<DistrictWardViewModel>> GetWard(GetWardRequest request);
        Task<List<CommonViewModel>> GetDirection();
        Task<List<CommonViewModel>> GetEvaluationStatus();
        Task<List<CommonViewModel>> GetTypeOfProperty();
        Task<List<CommonViewModel>> GetTypeOfTransaction();
        Task<List<CommonViewModel>> GetLegalPaper();        
    }
}
