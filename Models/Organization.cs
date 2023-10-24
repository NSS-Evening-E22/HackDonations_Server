namespace HackDonations_Server.Models
{
    public class Organization
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int UserId { get; set; }

        public List<string> tagList { get; set; }


    }
}
