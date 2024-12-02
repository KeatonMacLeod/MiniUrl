using System.ComponentModel.DataAnnotations;

namespace MiniUrl.Models
{
    public class UrlMapping
    {
        public int Id { get; set; }

        public string HashedUrl { get; set; }

        public string OriginalUrl { get; set; }

    }
}
