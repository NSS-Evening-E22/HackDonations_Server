using HackDonations_Server.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HackDonations_Server.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public List<Organization> OrganizationList { get; set; }
    }
}
