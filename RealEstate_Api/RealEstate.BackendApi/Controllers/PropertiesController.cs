using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Service.Properties;
using RealEstate.ViewModels.Service.Property;
using RealEstate.ViewModels.Service.PropertyImage;

namespace RealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IViewPropertyService _viewPropertyService;
        private readonly IPostPropertyService _postPropertyService;
        public PropertiesController(IViewPropertyService viewPropertyService, IPostPropertyService postPropertyService)
        {
            _viewPropertyService = viewPropertyService;
            _postPropertyService = postPropertyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var properties = await _viewPropertyService.GetAll();
            return Ok(properties);
        }

        [HttpGet("ViewPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetViewPropertyPagingRequest request)
        {
            var properties = await _viewPropertyService.GetAllByTypeOfTransaction(request);
            return Ok(properties);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetById(int propertyId)
        {
            var property = await _postPropertyService.GetById(propertyId);
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
            var propertyId = await _postPropertyService.Create(request);
            if (propertyId == 0)
                return BadRequest();
            var property = await _postPropertyService.GetById(propertyId);
            return CreatedAtAction(nameof(GetById), new { id = propertyId }, property);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] PropertyUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _postPropertyService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> Delete(int propertyId)
        {
            var affectedResult = await _postPropertyService.Delete(propertyId);
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
            var imageId = await _postPropertyService.AddImage(propertyId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _postPropertyService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpGet("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int propertyId, int imageId)
        {
            var image = await _postPropertyService.GetImageById(imageId);
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
            var result = await _postPropertyService.UpdateImage(imageId, request);
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
            var result = await _postPropertyService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }
    }
}
