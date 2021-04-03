using SpecificationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecificationAPI.Repository
{
    public class SpecificationRepo: ISpecificationRepo
    {
        private readonly SpecificationContext _context;
        public SpecificationRepo()
        {

        }
        public SpecificationRepo(SpecificationContext context)
        {
            _context = context;
        }
        public IEnumerable<SpecificationTable> GetAllSpecification()
        {
            return _context.Specifications.ToList();
        }
        public async Task<SpecificationTable> PostSpecification(SpecificationTable item)
        {
            SpecificationTable Sp = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                Sp = new SpecificationTable() { Name = item.Name,SpecializationName=item.SpecializationName };
                await _context.Specifications.AddAsync(Sp);
                await _context.SaveChangesAsync();
            }
            return Sp;
        }
        public async Task<SpecificationTable> RemoveSpecification(string id)
        {
            SpecificationTable sp = await _context.Specifications.FindAsync(id);
            if (sp == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _context.Specifications.Remove(sp);
                await _context.SaveChangesAsync();
            }
            return sp;
        }
    }
}
