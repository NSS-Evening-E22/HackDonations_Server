using HackDonations_Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using HackDonations_Server;
using EFCore.NamingConventions;

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
app.MapPut("/organizations/update/{OrgId}", (HackDonationsDbContext db, int OrgId, Organization payload) => {

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
app.MapDelete("/organizations/remove/{OrgId}", (HackDonationsDbContext db, int OrgId) => {

    Organization SelectedOrg = db.Organizations.FirstOrDefault(o => o.Id == OrgId);

    db.Organizations.Remove(SelectedOrg);
    db.SaveChanges();
    return Results.Ok("Organization has been removed.");

});

// Add Tag to Organization
app.MapPost("/organizations/{OrgId}/list", (HackDonationsDbContext db, int OrgId, Tag payload) =>
{
    // Retrieve object reference of Organizations in order to manipulate (Not a query result)
    var organi = db.Organizations
    .Where(o => o.Id == OrgId)
    .Include(o => o.TagList)
    .FirstOrDefault();
    if (organi == null)
    {
        return Results.NotFound("Organization not found.");
    }
    organi.TagList.Add(payload);
    db.SaveChanges();
    return Results.Ok(organi);
});

// Delete Tags from Organization
app.MapDelete("/organizations/{OrgId}/list/{TagId}/remove", (HackDonationsDbContext db, int OrgId, int TagId) =>
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

app.Run();