using HackDonations_Server.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HackDonations_Server.Models
{
    public class Organization
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int UserId { get; set; }

        public List<Tag> TagList { get; set; }

        public List<Donation> DonationList { get; set; }

        public List<Comment> CommentList { get; set; }

    }
}
