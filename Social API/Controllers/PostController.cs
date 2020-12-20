using System;
using System.Collections.Generic;
using Social_API.Repositories;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Social_API.Models;

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
        [Route("{id}", Name = "GetPostById")]
        public IHttpActionResult Get(int id)
        {
            var category = postRepository.Get(id);
            if (category == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(postRepository.Get(id));
        }
        [Route("")]
        public IHttpActionResult Post(Post post)
        {
            postRepository.Insert(post);
            string uri = Url.Link("GetPostById", new { id = post.PostId });
            return Created(uri, post);
        }
        [Route("{id}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] Post post)
        {
            post.PostId = id;
            postRepository.Update(post);
            return Ok(post);
        }
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            postRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("{id}/comments")]
        public IHttpActionResult GetAllComments(int id)
        {
            var comment = postRepository.GetCommentByPost(id);
            if (comment == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(postRepository.GetCommentByPost(id));
        }
    }
}
