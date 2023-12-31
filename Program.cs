using HackDonations_Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using HackDonations_Server;
using EFCore.NamingConventions;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7183",
                                              "http://localhost:3000")
                                               .AllowAnyHeader()
                                               .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HackDonationsDbContext>(builder.Configuration["HackDonations_ServerDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

#region User API

// Register New User
app.MapPost("/register", (HackDonationsDbContext db, User payload) =>
{
    User NewUser = new User()
    {
        Name = payload.Name,
        Bio = payload.Bio,
        Email = payload.Email,
        PhoneNumber = payload.PhoneNumber,
        ImageUrl = payload.ImageUrl,
        Uid = payload.Uid,
    };
    db.Users.Add(NewUser);
    db.SaveChanges();
    return Results.Ok(NewUser.Name);
});

// Edit User
app.MapPut("/users/update/{userId}", (HackDonationsDbContext db, int userId, User NewUser) =>
{
    User SelectedUser = db.Users.FirstOrDefault(x => x.Id == userId);
    if (SelectedUser == null)
    {
        return Results.NotFound("This User is not found in the database. Please Try again!");
    }

    SelectedUser.Name = NewUser.Name;
    SelectedUser.Bio = NewUser.Bio;
    SelectedUser.Email = NewUser.Email;
    SelectedUser.PhoneNumber = NewUser.PhoneNumber;
    SelectedUser.ImageUrl = NewUser.ImageUrl;
    db.SaveChanges();
    return Results.Created("/users/update/{uid}", SelectedUser);

});

// Check User
app.MapGet("/users/{uid}", (HackDonationsDbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.Uid == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound("Sorry, User not found!");
    }
    else
    {
        return Results.Ok(user);
    }
});

// Get User by Id
app.MapGet("/users/return/{iden}", (HackDonationsDbContext db, int iden) =>
{
    return db.Users.FirstOrDefault(x => x.Id == iden);
});

// View All Users
app.MapGet("/users", (HackDonationsDbContext db) => {

    return db.Users.ToList();

});

// Get User's Donations
app.MapGet("/user/{userId}/donations", (HackDonationsDbContext db, int userId) => {

    return db.Donations.Where(x => x.UserId == userId);
});


// Get User's Organizations
app.MapGet("/user/{userId}/organizations", (HackDonationsDbContext db, int userId) => {

    return db.Organizations.Where(x => x.UserId == userId);
});

// Get User's Comments
app.MapGet("/user/{userId}/comments", (HackDonationsDbContext db, int userId) => {

    return db.Comments.Where(x => x.UserId == userId);
});
#endregion

#region Organization API
// View All Organizations
app.MapGet("/organizations", (HackDonationsDbContext db) => {

    return db.Organizations.ToList();

});

// View An Organization Page
app.MapGet("/organization/{OrgId}", (HackDonationsDbContext db, int OrgId) => {

    return db.Organizations.FirstOrDefault(o => o.Id == OrgId);

});

// Create An Organization
app.MapPost("/organizations/new", (HackDonationsDbContext db, Organization payload) => {

    db.Organizations.Add(payload);
    db.SaveChanges();
    return Results.Created("/organizations/new", payload);

});

// Update An Organization Page
app.MapPut("/organizations/{OrgId}/update", (HackDonationsDbContext db, int OrgId, Organization payload) => {

    Organization SelectedOrg = db.Organizations.FirstOrDefault(o => o.Id == OrgId);

    if (SelectedOrg == null)
    {
        return Results.NotFound("Organization was not found!");
    }

    SelectedOrg.Title = payload.Title;
    SelectedOrg.Description = payload.Description;
    SelectedOrg.ImageUrl = payload.ImageUrl;

    db.SaveChanges();
    return Results.Ok("The existing Organization has been updated.");

});

// Delete An Organization
app.MapDelete("/organizations/{OrgId}/remove", (HackDonationsDbContext db, int OrgId) => {

    Organization SelectedOrg = db.Organizations.FirstOrDefault(o => o.Id == OrgId);

    db.Organizations.Remove(SelectedOrg);
    db.SaveChanges();
    return Results.Ok("Organization has been removed.");

});
#endregion

#region Tags
// View All Tags
app.MapGet("/tags", (HackDonationsDbContext db) => {

    return db.Tags.ToList();

});

// Add Tag to Organization
app.MapPost("/organizations/{OrgId}/tags/new", (HackDonationsDbContext db, int OrgId, int tId) =>
{
    // Retrieve object reference of Organizations in order to manipulate (Not a query result)
    var organi = db.Organizations
    .Where(o => o.Id == OrgId)
    .Include(o => o.TagList)
    .FirstOrDefault();

    var SelectedTag = db.Tags
    .Where(db => db.Id == tId)
    .FirstOrDefault();

    if (organi == null)
    {
        return Results.NotFound("Organization not found.");
    }
    organi.TagList.Add(SelectedTag);
    db.SaveChanges();
    return Results.Ok(organi);
});

// Delete Tags from Organization
app.MapDelete("/organizations/{OrgId}/tags/{TagId}/remove", (HackDonationsDbContext db, int OrgId, int TagId) =>
{
    try
    {
        // Include should come first before selecting
        var SingleOrg = db.Organizations
            .Include(Org => Org.TagList)
            .FirstOrDefault(x => x.Id == OrgId);
        if (SingleOrg == null)
        {
            return Results.NotFound("Sorry for the inconvenience! This organization does not exist.");
        }
        // The reason why it didn't work before is because I didnt have a method after TagList
        var SelectedTagList = SingleOrg.TagList.FirstOrDefault(t => t.Id == TagId);
        SingleOrg.TagList.Remove(SelectedTagList);
        db.SaveChanges();
        return Results.Ok(SingleOrg.TagList);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

// Get Tags from Organization
app.MapGet("/organizations/{OrgId}/tags", (HackDonationsDbContext db, int OrgId) =>
{
    try
    {
        var SingleOrg = db.Organizations
            .Where(db => db.Id == OrgId)
            .Include(Org => Org.TagList)
            .ToList();
        if (SingleOrg == null)
        {
            return Results.NotFound("Sorry for the inconvenience! This Organization does not exist.");
        }
        return Results.Ok(SingleOrg);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
#endregion

#region Donation

// View All Donations
app.MapGet("/donations", (HackDonationsDbContext db) => {

    return db.Donations.ToList();

});

// Get Single Donation
app.MapGet("/donations/{DonId}", (HackDonationsDbContext db, int DonId) =>
{
    return db.Donations.Where(s => s.Id == DonId);
});

// Get Donations from Organization
app.MapGet("/organizations/{OrgId}/donationlist", (HackDonationsDbContext db, int OrgId) =>
{
    try
    {
        var SingleOrg = db.Organizations
            .Where(db => db.Id == OrgId)
            .Include(Org => Org.DonationList)
            .ToList();
        if (SingleOrg == null)
        {
            return Results.NotFound("Sorry for the inconvenience! This organization does not exist.");
        }
        return Results.Ok(SingleOrg);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});


// Add Donation to Organization
app.MapPost("/organizations/{OrgId}/donations", (HackDonationsDbContext db, int OrgId, Donation payload) =>
{
    // Retrieve object reference of Organizations in order to manipulate (Not a query result)
    var organi = db.Organizations
    .Where(o => o.Id == OrgId)
    .Include(o => o.DonationList)
    .FirstOrDefault();
    if (organi == null)
    {
        return Results.NotFound("Organization not found.");
    }
    organi.DonationList.Add(payload);
    db.SaveChanges();
    return Results.Ok(organi);
});

// Delete Donations
app.MapDelete("/donations/{DonId}/remove", (HackDonationsDbContext db, int OrgId, int DonId) =>
{
    try
    {
        // Include should come first before selecting
        var SingleDon = db.Donations
            .FirstOrDefault(x => x.Id == DonId);

        if (SingleDon == null)
        {
            return Results.NotFound("Sorry for the inconvenience! This organization does not exist.");
        }
        // The reason why it didn't work before is because I didnt have a method after ProductList
        db.Donations.Remove(SingleDon);

        db.SaveChanges();
        return Results.Ok("Comment has been removed!");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

#endregion

#region Comments

// Get All Comments
app.MapGet("/comments", (HackDonationsDbContext db) => {

    return db.Comments.ToList();

});

// View A Comment
app.MapGet("/comments/{cId}", (HackDonationsDbContext db, int cId) => {

    return db.Comments.FirstOrDefault(o => o.Id == cId);

});




// Create A Comment
app.MapPost("/organizations/{OrgId}/comments/new", (HackDonationsDbContext db, int OrgId, Comment payload) =>
{

    // Retrieve object reference of Orders in order to manipulate (Not a query result)
    var Org = db.Organizations
    .Where(o => o.Id == OrgId)
    .Include(o => o.CommentList)
    .FirstOrDefault();

    if (Org == null)
    {
        return Results.NotFound("Order not found.");
    }

    Org.CommentList.Add(payload);

    db.SaveChanges();

    return Results.Ok(Org);

});

// Update A Comment
app.MapPut("/comments/update/{cId}", (HackDonationsDbContext db, int OrgId, int cId, Comment payload) =>
{
    // Retrive the Organization page you're trying to retrieve a comment from.
    Comment SelectedCom = db.Comments.FirstOrDefault(o => o.Id == cId);
    if (SelectedCom == null)
    {
        return Results.NotFound("Comment does not exist.");
    }

    SelectedCom.Description = payload.Description;

    db.SaveChanges();
    return Results.Ok("The existing comment has been updated.");

});

// Delete A Comment
app.MapDelete("/comments/{cId}/remove", (HackDonationsDbContext db, int cId) => {

try
{
    // Include should come first before selecting
    var SingleComm = db.Comments
        .FirstOrDefault(x => x.Id == cId);

    if (SingleComm == null)
    {
        return Results.NotFound("Sorry for the inconvenience! This organization does not exist.");
    }
    // The reason why it didn't work before is because I didnt have a method after ProductList
    db.Comments.Remove(SingleComm);

    db.SaveChanges();
    return Results.Ok("Comment has been removed!");
}
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
#endregion

app.Run();