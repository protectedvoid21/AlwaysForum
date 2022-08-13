using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Controllers {
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ReactionController : ControllerBase {
        // GET: api/<ReactionController>
        [HttpGet]
        public async Task<Reaction> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReactionController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<ReactionController>
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/<ReactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<ReactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
