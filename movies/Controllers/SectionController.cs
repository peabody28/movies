using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Repositories;
using movies.Models.Section;

namespace movies.Controllers
{
    public class SectionController : BaseController
    {
        public ISectionRepository SectionRepository { get; set; }

        public SectionController(IUserRepository userRepository, ISectionRepository sectionRepository) : base(userRepository)
        {
            SectionRepository = sectionRepository;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<SectionModel> Get()
        {
            var collection = SectionRepository.Collection();

            return collection.Select(item => new SectionModel
            {
                Name = item.Name
            });
        }
    }
}
