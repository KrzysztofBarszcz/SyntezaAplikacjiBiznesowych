using System.Collections.Generic;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using System;
using System.Linq;

namespace Blog.DAL.Repository
{
    public class BlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository()
        {
            _context = new BlogContext();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments;
        }

        public void AddComment(int id, string content, string author, int postId)
        {
            _context.Comments.Add(new Comment
            {
                Id = id,
                Author = author,
                Content = content,
                PostId = postId
            });
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetCommentsForPost(int postId)
        {
            var comments = GetAllComments();
            return comments.Where(element => element.PostId == postId);
        }
    }
}
