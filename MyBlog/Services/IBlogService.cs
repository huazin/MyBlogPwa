using System.Collections.Generic;

namespace MyBlog.Controllers
{
    public interface IBlogService
    {
        List<BlogPost> GetLatestPosts();
        string GetPostText(string link);

        List<BlogPost> GetOlderPosts(int oldestPostId);
        List<BlogPost> FindPost(string title);
        void Inserir(BlogPost item);
    }
}