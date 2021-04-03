using SpecificationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpecificationAPI.Repository
{
    public interface ISpecificationRepo
    {
        IEnumerable<SpecificationTable> GetAllSpecification();
        Task<SpecificationTable> PostSpecification(SpecificationTable item);

        Task<SpecificationTable> RemoveSpecification(string id);
    }
}
