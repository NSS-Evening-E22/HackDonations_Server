namespace HackDonations_Server.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Tag> TagList { get; set; }

    }
}
