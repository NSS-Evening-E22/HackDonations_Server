using System.Text.Json.Serialization;

namespace HackDonations_Server.Models
{
    public class Tag
    {
        public int id { get; set; }

        public string name { get; set; }

        public List<Tag> tagList { get; set; }

    }
}
