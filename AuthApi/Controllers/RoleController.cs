using AuthApi.Dtos;
using AuthApi.Helper;
using AuthApi.Models;
using AuthApi.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    
    public class RoleController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public RoleController(IUnitOfWork uow , IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost("assignRoles")]
        public async Task<IActionResult> assignRoles(List<RolePermissionAdd> model)
        {
            var roles = _mapper.Map<List<RolePermission>>(model);
            var api = await _uow._rolePermission.assignRoles(roles);
            if(api.ResponseCode == 404)
            {
                return NotFound(api);
            }
            var res = await _uow.saveChangesAsync();
            if (res == true)
            {
                api.ResponseCode = 201;
                api.Result = "Passed";
                return Ok(api);
            }
            else
            {
                api.ResponseCode = 400;
                api.Result = "Failed";
                return Ok(api);
            }
        }
        
        [HttpGet("getroles")]
        public async Task<IActionResult> getRoles(string role)
        {
            var roles = await _uow._rolePermission.allRoles(role);
            return Ok(roles);
        }

    }
}
