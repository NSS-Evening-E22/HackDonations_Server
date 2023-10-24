using System.Text.Json.Serialization;

﻿namespace HackDonations_Server.Models
{
    public class Organization
    {
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string imageUrl { get; set; }

        public int userId { get; set; }

        public List<Tag> tagList { get; set; }


    }
}
