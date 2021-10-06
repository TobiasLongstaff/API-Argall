using API_Argall.Context;
using API_Argall.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Argall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalletsController : ControllerBase
    {
        private readonly AppDbContext _repository;

        public PalletsController(AppDbContext repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Argall_Bd>>> Get()
        {
            return await _repository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{Idbulto}")]
        public async Task<ActionResult<IEnumerable<Argall_Bd>>> Get(int id)
        {
            return await _repository.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Argall_Bd>> Post([FromBody] Argall_Bd value)
        {
            if(value.idcamara != null)
            {
                var response = await _repository.AsignarCamara(value);
                if (response == null) { return NotFound(); }
                return response;
            }
            if (value.idbulto != null)
            {
                var response = await _repository.InsertAgregar(value);
                if (response == null) { return NotFound(); }
                return response;
            }
            else if(value.idpallet != null)
            {
                var response = await _repository.CerrarPallets(value);
                if (response == null) { return NotFound(); }
                return response;
            }
            else
            {
                var response  = await _repository.Login(value);
                if (response == null) { return NotFound(); }
                return response;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PalletsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
