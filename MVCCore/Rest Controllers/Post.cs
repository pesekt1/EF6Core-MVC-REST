using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MVCCore
{
    public class Post
    {
        public Post(int userId, string title, string body)
        {
            UserId = userId;
            Title = title;
            Body = body;
        }

        public Post()
        {
        }

        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

    }
}