using System;
using System.Linq;
using FluentAssertions;
using NDbUnit_Sample;
using NUnit.Framework;

namespace IntegrationTests
{
    public class BlogIntegrationTests
    {
        [Test]
        public void SavingABlogShouldSaveIt()
        {
            var context = new BlogContext();

            var newBlog = new Blog()
            {
                Name = "My Blog"
            };
            context.Blogs.Add(newBlog);
            context.SaveChanges();
            context.Dispose();

            context = new BlogContext();
            context.Database.Log = logString => Console.WriteLine(logString);
            context.Blogs.First().Name.Should().Be("My Blog");
        }

        
    }
}
