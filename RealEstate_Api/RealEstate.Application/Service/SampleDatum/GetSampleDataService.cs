using RealEstate.Data.EF;
using RealEstate.ViewModels.Service.SampleData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Application.Service.SampleDatum
{
    public class GetSampleDataService : IGetSampleDataService
    {
        private readonly RealEstateDbContext _context;

        public GetSampleDataService(RealEstateDbContext context)
        {
            _context = context;
        }
        public async Task<List<CommonViewModel>> GetDirection()
        {
            var query = from d in _context.Directions
                        select new { d };
            var data = await query.Select(x => new CommonViewModel()
            {
                Id = x.d.Id,
                Name = x.d.DirectionName,
            }).ToListAsync();
            return data;
        }

        public async Task<List<DistrictWardViewModel>> GetDistrict(GetDistrictRequest request)
        {
            var query = from d in _context.Districts
                        join p in _context.Provinces on d.ProvinceId equals p.Id
                        where d.ProvinceId == request.ProvinceId
                        select new { d };
            var data = await query.Select(x => new DistrictWardViewModel()
            {
                Id = x.d.Id,
                Name = x.d.Name,
                Prefix = x.d.Prefix
            }).ToListAsync();
            return data;
        }

        public async Task<List<CommonViewModel>> GetProvince()
        {
            var query = from p in _context.Provinces
                        select new { p };
            var data = await query.Select(x => new CommonViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
            }).ToListAsync();
            return data;
        }

        public async Task<List<CommonViewModel>> GetTypeOfProperty()
        {
            var query = from tp in _context.TypeOfProperties
                        select new { tp };
            var data = await query.Select(x => new CommonViewModel()
            {
                Id = x.tp.Id,
                Name = x.tp.TypeOfPropertyName,
            }).ToListAsync();
            return data;
        }

        public async Task<List<CommonViewModel>> GetEvaluationStatus()
        {
            var query = from e in _context.EvaluationStatuses
                        select new { e };
            var data = await query.Select(x => new CommonViewModel()
            {
                Id = x.e.Id,
                Name = x.e.EvaluationStatusName
            }).ToListAsync();
            return data;
        }

        public async Task<List<DistrictWardViewModel>> GetWard(GetWardRequest request)
        {
            var query = from w in _context.Wards
                        join d in _context.Districts on w.DistrictId equals d.Id
                        join p in _context.Provinces on d.ProvinceId equals p.Id
                        where w.DistrictId == request.DistrictId
                        select new { w };
            var data = await query.Select(x => new DistrictWardViewModel()
            {
                Id = x.w.Id,
                Name = x.w.Name,
                Prefix = x.w.Prefix
            }).ToListAsync();
            return data;
        }

        public async Task<List<CommonViewModel>> GetLegalPaper()
        {
            var query = from l in _context.LegalPapers
                        select new { l };
            var data = await query.Select(x => new CommonViewModel()
            {
                Id = x.l.Id,
                Name = x.l.TypeOfLegalPapers,
            }).ToListAsync();
            return data;
        }

        public async Task<List<CommonViewModel>> GetTypeOfTransaction()
        {
            var query = from tt in _context.TypeOfTransactions
                        select new { tt };
            var data = await query.Select(x => new CommonViewModel()
            {
                Id = x.tt.Id,
                Name = x.tt.TypeOfTransactionName,
            }).ToListAsync();
            return data;
        }
    }
}
