using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using FluentAssertions;
using NDbUnit_Sample;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTests
{
    public class BlogIntegrationTests
    {
        //[SetUp]

        [Test]
        public void InsertBlog_WhenCalled_InsertsBlog()
        {
            Database.SetInitializer(new TestInitializer());

            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);

                var newBlog = new Blog { Name = "New Blog", Text = "New Text" };
                context.Blogs.Add(newBlog);
                context.SaveChanges();
            }

            using (var context = new BlogContext())
            {
                context.Blogs
                    .First(blog => blog.Text.Contains("New"))
                    .Name.Should().Be("New Blog");
            }
        }

        [Test]
        public void UpdatingBlog_WhenCalled_UpdatesBlog()
        {
            Database.SetInitializer(new TestInitializer());

            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);
                var blog = context.Blogs.First();
                blog.Name = "Lambda";
                context.SaveChanges();
            }

            using (var context = new BlogContext())
            {
                context.Blogs.Count(blog => blog.Name.Contains("Lambda"))
                    .Should().Be(1);
            }
        }

        [Test]
        public void FindAll_WhenCalled_FindsAllInstances()
        {
            Database.SetInitializer(new TestInitializer());
            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);

                var result = context.Blogs.ToList();
                Assert.That(result.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void FindSpecificBlog_WhenCalled_FindsSpecificInstance()
        {
            Database.SetInitializer(new TestInitializer());
            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);

                var blog = context.Blogs.Single(b => b.Name.Contains("2"));
                Assert.That(blog.Name, Is.EqualTo("Name 2"));
            }
        }

        [Test]
        public void Delete_WhenCalled_DeletesBlog()
        {
            Database.SetInitializer(new TestInitializer());
            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);
                var blog = context.Blogs.First();
                context.Blogs.Remove(blog);
                context.SaveChanges();
            }

            using (var context = new BlogContext())
            {
                Assert.That(context.Blogs.Count(), Is.EqualTo(1));
            }

        }

        [Test]
        public void ThereShouldBe5PostsAllInAll()
        {
            Database.SetInitializer(new TestInitializer());
            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);
                Assert.That(context.Blogs.SelectMany(b=>b.Posts).Count(),Is.EqualTo(5));
            }
        }

        [Test]
        public void ItIsPossibleToChangePostTitle()
        {
            Database.SetInitializer(new TestInitializer());
            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);
                var blog = context.Blogs.Include(b=>b.Posts).FirstOrDefault();
                if (blog != null)
                {
                    var post = blog.Posts.FirstOrDefault();
                    if (post != null) post.Title = "Lambda";
                    context.SaveChanges();
                }
            }
            using (var context = new BlogContext())
            {
                Assert.That(context.Blogs.SelectMany(b=>b.Posts).Count(p => p.Title=="Lambda"), Is.EqualTo(1));
            }
        }

        [Test]
        public void ItIsPossibleToAddPostToTheBlog()
        {
            Database.SetInitializer(new TestInitializer());
            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);
                var blog = context.Blogs.FirstOrDefault();
                if (blog != null)
                {
                   blog.Posts.Add(new Post {Title = "Lambda" });
                   context.SaveChanges();
                }
            }
            using (var context = new BlogContext())
            {
                Assert.That(context.Blogs.SelectMany(b => b.Posts).Count(), Is.EqualTo(6));
            }
        }

        [Test]
        public void ItIsPossibleToDeletePost()
        {
            Database.SetInitializer(new TestInitializer());
            using (var context = new BlogContext())
            {
                context.Database.Initialize(force: true);
                var blog = context.Blogs.Include(b=>b.Posts).FirstOrDefault();
                if (blog != null)
                {
                    var post = blog.Posts.FirstOrDefault();
                    blog.Posts.Remove(post);
                    context.Entry(post).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            using (var context = new BlogContext())
            {
                Assert.That(context.Blogs.SelectMany(b => b.Posts).Count(), Is.EqualTo(4));
            }
        }

    }
}
