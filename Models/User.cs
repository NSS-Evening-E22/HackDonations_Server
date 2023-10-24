using System.Text.Json.Serialization;

namespace HackDonations_Server.Models
{
    public class User
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string bio { get; set; }

        public string phoneNumber { get; set; }

        public string imageUrl { get; set; }


    }
}
