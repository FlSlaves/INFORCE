using System.Text.Json.Serialization;

namespace INFORCE.Models
{
    public class URLs
    {

        public Guid Id { get; set; }

        public string UrlName { get; set; }
        public string Url { get; set; }

        public string ShortUrl { get; set; }


        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CodedUrl { get; set; }
    }
}
