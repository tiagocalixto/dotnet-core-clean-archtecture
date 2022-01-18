using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLittlePetShop.Api.mapper;
using MyLittlePetShop.Api.Models.constants;
using MyLittlePetShop.Api.Models.dto;
using MyLittlePetShop.Api.validator.filter;
using MyLittlePetShop.UseCase.handler.interfaces;

namespace MyLittlePetShop.Api.Controllers
{
    [ValidateModelStateAttribute]
    public class OwnerController : Controller
    {
        private readonly IUseCaseHandler _handler;

        public OwnerController(IUseCaseHandler handler)
        {
            _handler = handler;
        }
        
        [HttpGet]
        [Authorize]
        [Route("v1/owner/{id}")]
        public ActionResult<OwnerDto> FindById([Range(1, int.MaxValue,
                                                ErrorMessage = Constants.OWNER_ID_INVALID_RANGE)]
                                               [FromRoute] int id)
        {
            var response = _handler.FindOwnerById(id);
            return Ok(OwnerDtoMapper.ConvertEntityToDto(response));
        }

        [HttpGet]
        [Authorize]
        [Route("v1/owner/findByContact")]
        public ActionResult<OwnerDto> FindByContact([Required(ErrorMessage = Constants.CONTACT_TYPE_REQUIRED)]
                                               [FromQuery(Name = "contact_type")] string contactType,
                                               [Required(ErrorMessage = Constants.CONTACT_VALUE_REQUIRED)]
                                               [FromQuery(Name = "contact_value")] string contactValue)
        {
            var response = _handler.FindOwnerByContact(contactType, contactValue);
            return Ok(OwnerDtoMapper.ConvertEntityToDto(response));
        }

        [HttpGet]
        [Authorize]
        [Route("/v1/owner/findByName/{name}")]
        public ActionResult<PageableDto<List<OwnerDto>>> FindByName([FromRoute] string name,
                                          [Range(1, int.MaxValue, ErrorMessage = Constants.PAGE_INVALID_RANGE)]
                                          [Required(ErrorMessage = Constants.PAGE_REQUIRED )]
                                          [FromQuery(Name = "page")] int page,
                                          [Range(1, int.MaxValue, ErrorMessage = Constants.PAGE_SIZE_INVALID_RANGE)]
                                          [Required(ErrorMessage = Constants.PAGE_SIZE_REQUIRED )]
                                          [FromQuery(Name = "page_size")] int pageSize)
        {
            var response = _handler.FindOwnerByName(name, pageSize, page);
            return Ok(OwnerDtoMapper.ConvertEntityToPageableDto(response, page, pageSize));
        }

        [HttpPost]
        [Authorize]
        [Route("/v1/owner")]
        public ActionResult<OwnerDto> Save([FromBody] OwnerDto owner)
        {
            var response = _handler.SaveOwner(OwnerDtoMapper.ConvertDtoToEntity(owner));
            return Created("", OwnerDtoMapper.ConvertEntityToDto(response));
        }

        [HttpPut]
        [Authorize]
        [Route("/v1/owner")]
        public ActionResult<OwnerDto> Update([FromBody] OwnerDto owner)
        {
            var response = _handler.UpdateOwner(OwnerDtoMapper.ConvertDtoToEntity(owner));
            return Ok(OwnerDtoMapper.ConvertEntityToDto(response));
        }

        [HttpDelete]
        [Authorize]
        [Route("/v1/owner/{id}")]
        public ActionResult Delete([Range(1, int.MaxValue,
                                          ErrorMessage = Constants.OWNER_ID_INVALID_RANGE)]
                                   [FromRoute] int id)
        {
            _handler.DeleteOwnerById(id);
            return Ok();
        }
    }
}
