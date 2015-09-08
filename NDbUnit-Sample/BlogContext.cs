using System.Collections.Generic;
using System.Data.Entity;

namespace NDbUnit_Sample
{
    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
    }

    public class Blog
    {
        public Blog()
        {
            Posts = new List<Post>();
        }
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
