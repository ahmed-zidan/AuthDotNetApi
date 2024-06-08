using AuthApi.Dtos;
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
    
    public class MenuController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public MenuController(IUnitOfWork uow , IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost("addmenu")]
        public async Task<IActionResult> addmenu(MenuAddDto model)
        {
            var menu = _mapper.Map<Menu>(model);
            await _uow._menueService.addMenu(menu);
            await _uow.saveChangesAsync();
            return StatusCode(201);
        }
        [HttpGet("getmenus")]
        public async Task<IActionResult> getMenus()
        {
           var menus =  await _uow._menueService.allMenus();
            var menusDto = _mapper.Map<List<MenuListDto>>(menus);
            return Ok(menusDto);
        }

        [HttpGet("getmenusByRole/{role}")]
        public async Task<IActionResult> getMenus(string role)
        {
            var menus = await _uow._menueService.allMenusByRole(role);
            var menusDto = _mapper.Map<List<MenuListDto>>(menus);
            return Ok(menusDto);
        }
    }
}
