using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObject.DTOs;
using DataTransferObject.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareBusiness.Factories;

namespace Backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OnlyUserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var apiResult = APIResultFactory.Build(true, StatusCodes.Status200OK,
                        ErrorMessageEnum.None, payload: new OnlyTestDto() { Message = "Hello User~~" });

            return Ok(apiResult);
        }
    }
}
