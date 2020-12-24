using Inventory_with_Repository_Pattern.Repositories;
using Social_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Social_API.Repositories
{
    public class PostRepository : Repository<Post>
    {
        CommentRepository commentRepository = new CommentRepository();
        public List<Comment> GetCommentByPost(int id)
        {
            return commentRepository.GetAll().Where(x => x.PostId == id).ToList();
        }
        public List<Comment> GetAllCommentsByPostByCommentId(int id, int CommentId)
        {
            return commentRepository.GetAll().Where(x => x.PostId == id && x.CommentId == CommentId).ToList();
        }
    }
}