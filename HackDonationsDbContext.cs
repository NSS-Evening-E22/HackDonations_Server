using HackDonations_Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HackDonations_Server
{
    public class HackDonationsDbContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public HackDonationsDbContext(DbContextOptions<HackDonationsDbContext> context) : base(context)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(new User[]
            {
            new User {Id = 1, Name="Riley Tullis", Bio="The greatest person you'll ever meet.", PhoneNumber="123-456-7890", ImageUrl="https://th.bing.com/th/id/R.f08431063da214d8c07452cca215447f?rik=7gKQvCXgiLVQXw&pid=ImgRaw&r=0", Email="riley@email.com", Uid="" },
            new User {Id = 2, Name="Jovanni Feliz", Bio="Eh, he's ok.", PhoneNumber="098-765-4321", ImageUrl="https://th.bing.com/th/id/R.e733fb390ae9a3c28ca2389bd2466be7?rik=tGXnQ7Yf6T1kBQ&pid=ImgRaw&r=0", Email="jovanni@email.com", Uid="",}

            });

            modelBuilder.Entity<Organization>().HasData(new Organization[]
           {
            new Organization {Id = 1, Title="UNICEF", Description="UNICEF works in over 190 countries and territories to save children's lives, to defend their rights, and to help them fulfill their potential, from early childhood through adolescence.", ImageUrl="https://th.bing.com/th/id/R.7b5717ff47e176e9ac2a5580df80d6db?rik=N4afUMyWSF696Q&pid=ImgRaw&r=0", UserId=1},
            new Organization {Id = 2, Title="Doctors Without Borders", Description="Doctors Without Borders provides medical care to those affected by conflict, epidemics, disasters, or exclusion from healthcare. They are an international medical humanitarian organization that delivers emergency aid to people in need.", ImageUrl="https://4.bp.blogspot.com/-IJAS3gWYhCE/V8SZeGbKD8I/AAAAAAAAQFI/SyJeu-SzCpkh8R1NzePSysZ3Kni5oDtbQCLcB/s1600/msf_dual_english_cmyk_0.jpg", UserId=2},

           });

            modelBuilder.Entity<Tag>().HasData(new Tag[]
           {
            new Tag {Id = 1, Name="Health and Medical"},
            new Tag {Id = 2, Name="Environmental and Conservation"},
            new Tag {Id = 3, Name="Child Welfare and Development"},
            new Tag {Id = 4, Name="Social Welfare and Poverty Alleviation"},

           });
        }

    };

};