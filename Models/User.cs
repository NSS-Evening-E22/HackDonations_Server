using HackDonations_Server.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HackDonations_Server.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string Uid { get; set; }

        public List<Comment> CommentList { get; set; }

        public List<Donation> DonationList { get; set; }
        public List<Organization> OrganizationList { get; set; }
        
    }
}
