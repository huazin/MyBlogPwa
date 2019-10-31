using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyBlog.Controllers
{
    public class BlogService : IBlogService
    {
        private IHostingEnvironment _env;
        public BlogService(IHostingEnvironment env)
        {
            _env = env;

            if (Posts == null || Posts.Count == 0)
            {
                Posts = new List<BlogPost>();
                var arquivos = Directory.GetFiles($"{_env.WebRootPath}/Posts/");
                foreach (var item in arquivos)
                {
                    var nome = item.Replace($"{_env.WebRootPath}/Posts/", "");
                    var atributos = nome.Split("_");
                    var texto = File.ReadLines(item);
                    if (atributos.Length != 3 || texto.Count() == 0)
                        continue;
                    Posts.Add(
                        new BlogPost()
                        {
                            PostId = Int32.Parse(atributos[1]),
                            Title = atributos[0],
                            ShortDescription = texto.FirstOrDefault(),
                            LinkImage = texto.Skip(1).FirstOrDefault()
                        }
                    );
                }
            }
        }

        private static List<BlogPost> Posts
        {
            get; set;
        }

        public string GetPostText(string link)
        {
            var post = Posts.FirstOrDefault(_ => _.Link == link);

            return File.ReadAllText($"{_env.WebRootPath}/Posts/{post.Title}_{post.PostId}_post.md");
        }

        public List<BlogPost> GetLatestPosts()
        {
            return Posts.OrderByDescending(_ => _.PostId).Take(3).ToList();
        }

        public List<BlogPost> FindPost(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Posts;
            var post = Posts.Where(_ => _.Title.ToUpper().Contains(title.ToUpper()));
            return post.ToList();

        }


        public List<BlogPost> GetOlderPosts(int oldestPostId)
        {
            var posts = Posts.Where(_ => _.PostId < oldestPostId).OrderByDescending(_ => _.PostId).ToList();

            if (posts.Count < 3)
                return posts;

            return posts.Take(3).ToList();
        }

        public void Inserir(BlogPost item)
        {
            try
            {
                item.PostId = Posts.Max(p => p.PostId) + 1;

                string nomeArquivo = $"{_env.WebRootPath}/Posts/{item.Title}_{item.PostId}_post.md";

                StreamWriter writer = new StreamWriter(nomeArquivo);

                writer.WriteLine(item.ShortDescription);
                writer.WriteLine(item.LinkImage);

                writer.WriteLine("");
                writer.WriteLine(item.Texto);

                writer.Close();

                Posts.Add(item);
            }
            catch (System.Exception e)
            {

                throw;
            }

        }
    }
}
