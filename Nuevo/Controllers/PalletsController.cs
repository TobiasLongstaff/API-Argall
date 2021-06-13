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
        /*[HttpGet("{Idbulto}")]
        public async Task<ActionResult<Argall_Bd>> Get(int Idbulto)
        {
            var response = await _repository.GetById(Idbulto);
            if (response == null) { return NotFound(); }
            return response;
        }*/

        // POST api/values
        [HttpPost("Insertart")]
        public async Task<ActionResult<Argall_Bd>> Insertart([FromBody] Argall_Bd value)
        {
            var response = await _repository.InsertAgregar(value);
            if (response == null) { return NotFound(); }
            return response;
        }

        [HttpPost("Cerrar")]
        public async Task<ActionResult<Argall_Bd>> Cerrar([FromBody] Argall_Bd value)
        {
            var response = await _repository.CerrarPallets(value);
            if (response == null) { return NotFound(); }
            return response;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Argall_Bd>> Login([FromBody] Argall_Bd value)
        {
            var response = await _repository.Login(value);
            if (response == null) { return NotFound(); }
            return response;
        }

        /*public async Task<ActionResult<Argall_Bd>> Post([FromBody] Argall_Bd value)
        {
            if(value.idpallet != null)
            {
                var response = await _repository.CerrarPallets(value);
                if (response == null) { return NotFound(); }
                return response;
            }
            else if(value.idbulto != null)
            {
                var response = await _repository.InsertAgregar(value);
                if (response == null) { return NotFound(); }
                return response;
            }
            else
            {
                var response = await _repository.Login(value);
                if (response == null) { return NotFound(); }
                return response;
            }
        }*/

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        /*[HttpDelete("{id}")]
        public async Task Delete(int id)
        {
           await _repository.DeleteById(id);
        }
        private readonly AppDbContext context;

        public PalletsController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<PalletsController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.tabla_prueba.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PalletsController>/5
        [HttpGet("{id}", Name = "GetGestor")]
        public ActionResult Get(int id)
        {
            try
            {
                var pallet = context.tabla_prueba.FirstOrDefault(g => g.id == id);
                return Ok(pallet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<PalletsController>
        [HttpPost]
        public ActionResult Post([FromBody] Argall_Bd pallet)
        {
            try
            {
                context.tabla_prueba.Add(pallet);
                context.SaveChanges();
                return CreatedAtRoute("GetGestor", new { id = pallet.id }, pallet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PalletsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Argall_Bd pallet)
        {
            try
            {
                if (pallet.id == id)
                {
                    context.Entry(pallet).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetGestor", new { id = pallet.id }, pallet);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        // DELETE api/<PalletsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
