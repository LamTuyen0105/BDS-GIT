using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Service.SampleDatum;
using RealEstate.ViewModels.Service.SampleData;

namespace RealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleDatumController : ControllerBase
    {
        private readonly IGetSampleDataService _getSampleDataService;
        public SampleDatumController (IGetSampleDataService getSampleDataService)
        {
            _getSampleDataService = getSampleDataService;
        }

        [HttpGet("Province")]
        public async Task<IActionResult> GetProvince()
        {
            var province = await _getSampleDataService.GetProvince();
            return Ok(province);
        }

        [HttpGet("District")]
        public async Task<IActionResult> GetDistrict(GetDistrictRequest request)
        {
            var district = await _getSampleDataService.GetDistrict(request);
            return Ok(district);
        }

        [HttpGet("Ward")]
        public async Task<IActionResult> GetWard(GetWardRequest request)
        {
            var ward = await _getSampleDataService.GetWard(request);
            return Ok(ward);
        }

        [HttpGet("Direction")]
        public async Task<IActionResult> GetDirection()
        {
            var direction = await _getSampleDataService.GetDirection();
            return Ok(direction);
        }

        [HttpGet("TypeOfProperty")]
        public async Task<IActionResult> GetTypeOfProperty()
        {
            var typeOfProperty = await _getSampleDataService.GetTypeOfProperty();
            return Ok(typeOfProperty);
        }

        [HttpGet("TypeOfTransaction")]
        public async Task<IActionResult> GetTypeOfTransaction()
        {
            var typeOfTransaction = await _getSampleDataService.GetTypeOfTransaction();
            return Ok(typeOfTransaction);
        }

        [HttpGet("EvaluationStatus")]
        public async Task<IActionResult> GetEvaluationStatus()
        {
            var evaluationStatus = await _getSampleDataService.GetEvaluationStatus();
            return Ok(evaluationStatus);
        }

        [HttpGet("LegalPaper")]
        public async Task<IActionResult> GetLegalPaper()
        {
            var legalPaper = await _getSampleDataService.GetLegalPaper();
            return Ok(legalPaper);
        }
    }
}
