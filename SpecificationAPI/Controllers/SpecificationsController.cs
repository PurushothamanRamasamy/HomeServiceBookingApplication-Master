using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecificationAPI.Models;
using SpecificationAPI.Repository;

namespace SpecificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationsController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SpecificationsController));
        private readonly ISpecificationRepo _context;

        public SpecificationsController(ISpecificationRepo context)
        {
            _context = context;
        }

        // GET: api/Specifications
        [HttpGet]
        public IEnumerable<SpecificationTable> GetSpecifications()
        {
            return  _context.GetAllSpecification();
        }

        // GET: api/Specifications/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<SpecificationTable>> GetSpecificationTable(string id)
        {
            var specificationTable = await _context.Specifications.FindAsync(id);

            if (specificationTable == null)
            {
                return NotFound();
            }

            return specificationTable;
        }*/

        // PUT: api/Specifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutSpecificationTable(string id, SpecificationTable specificationTable)
        {
            if (id != specificationTable.Name)
            {
                return BadRequest();
            }

            _context.Entry(specificationTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecificationTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Specifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpecificationTable>> PostSpecificationTable(SpecificationTable specificationTable)
        {
            _log4net.Info("  Specification Initiated");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedSpecification = await _context.PostSpecification(specificationTable);

            return Ok(addedSpecification);
        }

        // DELETE: api/Specifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecificationTable(string id)
        {
            _log4net.Info("This id: "+id+"got deleted");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletedSpecification = await _context.RemoveSpecification(id);

            return Ok(deletedSpecification);
        }

        /*private bool SpecificationTableExists(string id)
        {
            return _context.Specifications.Any(e => e.Name == id);
        }*/
    }
}
