using System;
using System.Collections.Generic;
using Social_API.Repositories;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Social_API.Models;
using Inventory_with_Repository_Pattern.Repositories;
using System.Web;

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
            var post = postRepository.Get(id);
            if (post == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            post.Links.Add(new Link() { Url = "http://localhost:63238/api/posts/", Method = "Get", Relation = "Self" });
            post.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString(), Method = "Get", Relation = "Get all categories" });
            post.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString(), Method = "Post", Relation = "Create new category resources" });
            post.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString(), Method = "Put", Relation = "Modify existing category resources" });
            post.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString(), Method = "Delete", Relation = "Remove existing category resources" });
            return Ok(postRepository.Get(id));
        }
        [Route("")]
        public IHttpActionResult Post(Post post)
        {
            postRepository.Insert(post);
            post.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString(), Method = "Delete", Relation = "Remove existing category resources" });
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
        //..........................................
        [Route("{id}/comments")]
        public IHttpActionResult GetAllComments(int id)
        {
            var comment = postRepository.GetCommentByPost(id);
            if (comment == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }else
            return Ok(postRepository.GetCommentByPost(id));
        }
        [Route("{id}/comments/{CommentId}")]
        public IHttpActionResult GetAllCommentsById(int id, int CommentId)
        {
            var comment = postRepository.GetAllCommentsByPostByCommentId(id, CommentId);
            if (comment == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }else
            return Ok(postRepository.GetAllCommentsByPostByCommentId(id, CommentId));
        }
        [Route("{id}/comments")]
        public IHttpActionResult Post(Comment comment)
        {
            CommentRepository commentRepository = new CommentRepository();
            commentRepository.Insert(comment);
            Post post = new Post();
            return Created("api/Products/" + post.PostId, comment);
        }
        [Route("{id}/comments/{CommentId}")]
        public IHttpActionResult Put([FromUri] int id, [FromUri] int CommentId, [FromBody] Comment comment)
        {
            Post post = new Post();
            post.PostId = id;
            comment.CommentId = CommentId;
            CommentRepository commentRepository = new CommentRepository();
            commentRepository.Update(comment);
            return Ok(comment);
        }
        [Route("{id}/comments/{CommentId}")]
        public IHttpActionResult Deleteomments(int id, int CommentId)
        {
            CommentRepository commentRepository = new CommentRepository();
            commentRepository.Delete(CommentId);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
