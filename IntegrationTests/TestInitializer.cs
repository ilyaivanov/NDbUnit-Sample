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
            
            using (StreamReader r = new StreamReader("D:\\NDbUnit-Sample\\blogs.json"))
            {
                string json = r.ReadToEnd();
                var blogs = JsonConvert.DeserializeObject<List<Blog>>(json);
                context.Blogs.AddRange(blogs);
                base.Seed(context);
            }


        }
    }
}
