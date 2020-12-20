using System;
using System.Collections.Generic;
using Social_API.Repositories;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Social_API.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostController : ApiController
    {
        PostRepository postRepository = new PostRepository();
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(postRepository.GetAll());
        }
        [Route("{id}", Name = "GetCategoryById")]
        public IHttpActionResult Get(int id)
        {
            var category = postRepository.Get(id);
            if (category == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(postRepository.Get(id));
        }
    }
}
