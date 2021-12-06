using Microsoft.AspNetCore.Mvc;
using ORM.Define;
using Repository.Define;
using Repository.Implement;

namespace NetCoreBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public NoteController(IDataFactory data)
        {
            _unit = new UnitOfWork(data, new[] { "note" });
        }

        [HttpGet]
        [Route("api/note")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_unit.NoteRepository.GetById(id));
        }
    }
}