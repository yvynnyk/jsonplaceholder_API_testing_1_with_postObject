
namespace Flurl_with_object
{
    public class PostObject
    {
        public string title { get; }
        public string body { get; }
        public int userId { get; }
       
        public PostObject (string title, string body, int userId)
        {
            this.title = title;
            this.body = body;
            this.userId = userId;
        }
    }
}
