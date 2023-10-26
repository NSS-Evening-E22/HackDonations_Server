using HackDonations_Server.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HackDonations_Server.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PaymentType { get; set; }
        public int DonationAmount { get; set; }
        public string Comment { get; set; }
        public List<Organization> OrganizationList { get; set; }
    }
}
