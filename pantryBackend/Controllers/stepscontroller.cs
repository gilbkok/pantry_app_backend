using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pantryBackend.Data;
using pantryBackend.Models;

namespace pantryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class stepsController:ControllerBase
    {
        private readonly pantryDbContext _stepsContext;
        public stepsController(pantryDbContext stepsContext)
        {
            _stepsContext = stepsContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<steps>> GetAllsteps()
        {
            return _stepsContext.steps;
        }

        [HttpPost]

        public void PostSteps(steps step)
        {
            _stepsContext.steps.Add(step);
            _stepsContext.SaveChanges();

        }
    }
}
