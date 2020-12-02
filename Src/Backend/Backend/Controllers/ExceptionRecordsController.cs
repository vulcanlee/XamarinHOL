using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Services;
using DataAccessLayer.Models;
using DataTransferObject.DTOs;
using DataTransferObject.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShareBusiness.Factories;

namespace Backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionRecordsController : ControllerBase
    {
        private readonly IMapper mapper;

        #region 建構式
        public ExceptionRecordsController(
            IMapper mapper)
        {
            this.mapper = mapper;
        }
        #endregion

        #region C 新增
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ExceptionRecordDTO> exceptionRecordRequestDTO)
        {
            APIResult apiResult;
            if (exceptionRecordRequestDTO != null)
            {
                foreach (var item in exceptionRecordRequestDTO)
                {
                    Console.WriteLine($"{item.CallStack}");
                }
                apiResult = APIResultFactory.Build(true, StatusCodes.Status201Created,
                    ErrorMessageEnum.None, payload: exceptionRecordRequestDTO);
            }
            else
            {
                apiResult = APIResultFactory.Build(false, StatusCodes.Status200OK,
                    ErrorMessageEnum.傳送過來的資料有問題, payload: null);
            }
            return Ok(apiResult);
        }
        #endregion
    }
}
