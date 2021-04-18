using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecilizationAPI.Models;
using SpecilizationAPI.Repository;

namespace SpecilizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly ISpecializationRepo _context;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SpecializationsController));

        public SpecializationsController(ISpecializationRepo context)
        {
            _context = context;
        }

        // GET: api/Specializations
        [HttpGet]
        public  IEnumerable<SpecializationTable> GetSpecializations()
        {
            _log4net.Info("Specialization get is initialized");
            return  _context.GetAllSpecialization();
        }

        

       

        // POST: api/Specializations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpecializationTable>> PostSpecialization(SpecializationTable specialization)
        {
            _log4net.Info("Specilization post method is initialized");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var addedSpecilization = await _context.PostSpecialization(specialization);

            return Ok(addedSpecilization);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserServiceInfo(string id)
        {
            _log4net.Info("this row with id:"+id+"Deleted");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletedSpecilization = await _context.RemoveSpecialization(id);

            return Ok(deletedSpecilization);
        }




    }
}
