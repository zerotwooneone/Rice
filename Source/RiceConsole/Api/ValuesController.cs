using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Rice.Core.Abstractions.ModuleLoad;

namespace RiceConsole.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILoadableModuleFactory _loadableModuleFactory;
        private readonly IModuleLoader _moduleLoader;

        public ValuesController(ILoadableModuleFactory loadableModuleFactory, IModuleLoader moduleLoader)
        {
            _loadableModuleFactory = loadableModuleFactory;
            _moduleLoader = moduleLoader;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
