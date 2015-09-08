using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using NDbUnit_Sample;
using Newtonsoft.Json;

namespace IntegrationTests
{
    public class TestInitializer : DropCreateDatabaseAlways<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            var blogs = new List<Blog>
            {
                new Blog
                {
                    Name = "Name 1",
                    Text = "Text 1",
                    Posts =
                    {
                        new Post { Title = "Post 1"},
                        new Post { Title = "Post 2"}
                    }
                },
                new Blog
                {
                    Name = "Name 2",
                    Text = "Text 2",
                    Posts =
                    {
                        new Post { Title = "Post 3"},
                        new Post { Title = "Post 4"},
                        new Post { Title = "Post 5"}
                    }
                }
            };
            context.Blogs.AddRange(blogs);
            base.Seed(context);
        }
    }
}
