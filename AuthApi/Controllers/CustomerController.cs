using AuthApi.Dtos;
using AuthApi.Helper;
using AuthApi.Models;
using AuthApi.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public CustomerController(IUnitOfWork uof , IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var custs = await _uof._customer.getAll();
            var custsDto = _mapper.Map<IEnumerable<CustomerListDto>>(custs);
            return Ok(custsDto);
        }
        [HttpGet("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var cust = await _uof._customer.getById(id);
            ApiResponse api = new ApiResponse();
            if(cust == null)
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "Customer is Not Found";
                return NotFound(api);
            }

            var custsDto = _mapper.Map<CustomerListDto>(cust);
            return Ok(custsDto);
        }
        
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CustomerAddDto model)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Model is not Valid";
                return BadRequest(api);
            }

            var cust = _mapper.Map<Customer>(model);
            await _uof._customer.CreateRecord(cust);
            await _uof.saveChangesAsync();

            return StatusCode(201);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(CustomerUpdateDto model , int id)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Model is not Valid";
                return BadRequest(api);
            }
            if(model.Id != id)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Invalid Customer";
                return BadRequest(api);
            }
            
            var cust = await _uof._customer.getById(id);
            if(cust == null)
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "Customer does not exist";
                return NotFound(api);
            }
            cust.Name = model.Name;
            cust.Phone = model.Phone;
            cust.Email = model.Email;
            
             _uof._customer.Update(cust);
            await _uof.saveChangesAsync();

            return Ok();
        }

        [HttpPost("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Model is not Valid";
                return BadRequest(api);
            }
            
            var cust = await _uof._customer.getById(id);
            if (cust == null)
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "Customer does not exist";
                return NotFound(api);
            }
          
            _uof._customer.Remove(cust);
            await _uof.saveChangesAsync();

            return Ok();
        }
    }
}
