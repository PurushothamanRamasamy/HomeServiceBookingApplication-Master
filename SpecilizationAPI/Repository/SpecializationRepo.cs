using SpecilizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecilizationAPI.Repository
{
    public class SpecializationRepo: ISpecializationRepo
    {
        private readonly SpecializationContext _context;
        public SpecializationRepo()
        {

        }
        public SpecializationRepo(SpecializationContext context)
        {
            _context = context;
        }
        public IEnumerable<SpecializationTable> GetAllSpecialization()
        {
            return _context.Specializations.Where(name=>name.Name!="admin").ToList();
        }
        public async Task<SpecializationTable> PostSpecialization(SpecializationTable item)
        {
            SpecializationTable Sp = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                Sp = new SpecializationTable() { Name = item.Name};
                await _context.Specializations.AddAsync(Sp);
                await _context.SaveChangesAsync();
            }
            return Sp;
        }
        public async Task<SpecializationTable> RemoveSpecialization(string id)
        {
            SpecializationTable sp = await _context.Specializations.FindAsync(id);
            if (sp == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _context.Specializations.Remove(sp);
                await _context.SaveChangesAsync();
            }
            return sp;
        }
    }
}
