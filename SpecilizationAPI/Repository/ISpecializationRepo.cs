using SpecilizationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecilizationAPI.Repository
{
     public interface ISpecializationRepo
    {
        IEnumerable<SpecializationTable> GetAllSpecialization();
        Task<SpecializationTable> PostSpecialization(SpecializationTable item);

        Task<SpecializationTable> RemoveSpecialization(string id);

    }
}
