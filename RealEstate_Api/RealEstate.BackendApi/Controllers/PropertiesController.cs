using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Service.NewsManagers;
using RealEstate.ViewModels.Service.Property;
using RealEstate.ViewModels.Service.PropertyImage;

namespace RealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyOwnerService _ownerPropertyService;        
        public PropertiesController(IPropertyOwnerService ownerPropertyService)
        {
            _ownerPropertyService = ownerPropertyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var properties = await _ownerPropertyService.GetAll();
            return Ok(properties);
        }

        [HttpGet("ViewPagingByTransaction")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetViewPropertyPagingRequest request)
        {
            var properties = await _ownerPropertyService.GetAllByTypeOfTransaction(request);
            return Ok(properties);
        }

        [HttpGet("ViewPaging")]
        public async Task<IActionResult> GetPaging([FromQuery] GetPropertyPagingRequest request)
        {
            var properties = await _ownerPropertyService.GetPaging(request);
            return Ok(properties);
        }

        [HttpGet("Find")]
        public async Task<IActionResult> Find ([FromQuery] GetFindPropertyPagingRequest request)
        {
            var properties = await _ownerPropertyService.Find(request);
            return Ok(properties);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetById(int propertyId)
        {
            var property = await _ownerPropertyService.GetById(propertyId);
            if (property == null)
                return BadRequest("Không tìm thấy được tin đăng");
            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PropertyCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var propertyId = await _ownerPropertyService.Create(request);
            if (propertyId == 0)
                return BadRequest();
            var property = await _ownerPropertyService.GetById(propertyId);
            return CreatedAtAction(nameof(GetById), new { id = propertyId }, property);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] PropertyUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _ownerPropertyService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> Delete(int propertyId)
        {
            var affectedResult = await _ownerPropertyService.Delete(propertyId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPost("{propertyId}/images")]
        public async Task<IActionResult> CreateImage(int propertyId, [FromForm] PropertyImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _ownerPropertyService.AddImage(propertyId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _ownerPropertyService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpGet("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int propertyId, int imageId)
        {
            var image = await _ownerPropertyService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Không tìm thấy hình ảnh");
            return Ok(image);
        }

        [HttpGet("{propertyId}/images")]
        public async Task<IActionResult> GetListImage(int propertyId)
        {
            var image = await _ownerPropertyService.GetListImages(propertyId);
            if (image == null)
                return BadRequest("Không tìm thấy hình ảnh");
            return Ok(image);
        }

        [HttpPut("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] PropertyImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ownerPropertyService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ownerPropertyService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
    }
}