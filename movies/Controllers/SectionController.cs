using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Attributes;
using movies.Interfaces.Repositories;
using movies.Models.Section;

namespace movies.Controllers
{
    public class SectionController : BaseController
    {
        [Dependency]
        public ISectionRepository SectionRepository { get; set; }

        public SectionController(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<SectionModel> Get()
        {
            return null;
            var collection = SectionRepository.Collection();

            return collection.Select(item => new SectionModel
            {
                Name = item.Name
            });
        }
    }
}
