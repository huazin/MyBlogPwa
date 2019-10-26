using Microsoft.AspNetCore.Hosting;
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
        }

        private List<BlogPost> Posts
        {
            get
            {
                return new List<BlogPost>() {
                new BlogPost { PostId = 1, Title = "API", ShortDescription = "Oque é? veja aqui em primeira mão" },
                new BlogPost { PostId = 2, Title = "Indexed DB", ShortDescription = "O fim do sql server?" },
                new BlogPost { PostId = 3, Title = "Cache", ShortDescription = "Amigo ou inimigo?" },
                new BlogPost { PostId = 4, Title = "Service Worker", ShortDescription = "Um grande ferramenta para front end" },
                new BlogPost { PostId = 5, Title = "PWA", ShortDescription = "aplicações super rapidas" },
                new BlogPost { PostId = 6, Title = "Notificações push", ShortDescription = "Como envia-las?" },
                new BlogPost { PostId = 7, Title = "Micro front ends", ShortDescription = "Nova moda Micro front ends" },
                new BlogPost { PostId = 8, Title = "Blazor", ShortDescription = "Oque é?" },
                new BlogPost { PostId = 9, Title = "Xamarim", ShortDescription = "Aplicativos nativos em c#?" },
                new BlogPost { PostId = 10, Title = "Unity", ShortDescription = "criando jogos com C#" },
                new BlogPost { PostId = 11, Title = "Angular", ShortDescription = "a evolução do JavaScript?" },
                new BlogPost { PostId = 12, Title = "React", ShortDescription = "A nova solução do facebook" }
            };
            }
        }

        public string GetPostText(string link)
        {
            var post = Posts.FirstOrDefault(_ => _.Link == link);

            return File.ReadAllText($"{_env.WebRootPath}/Posts/{post.PostId}_post.md");
        }

        public List<BlogPost> GetLatestPosts()
        {
            return Posts.OrderByDescending(_ => _.PostId).Take(3).ToList();
        }

        public List<BlogPost> FindPost(string title)
        {
            var post = Posts.Where(p => p.Title.ToUpper().Contains(title));
            return post.ToList();

        }


        public List<BlogPost> GetOlderPosts(int oldestPostId)
        {
            var posts = Posts.Where(_ => _.PostId < oldestPostId).OrderByDescending(_ => _.PostId).ToList();

            if (posts.Count < 3)
                return posts;

            return posts.Take(3).ToList();
        }
    }
}
