using TestTask.Application;
using TestTask.DAL.PostgreSQL.Extensions;
using TestTask.DAL.PostgreSQL.Infrastructure;
using TestTask.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddApplicationLayer();
builder.Services.AddDataAccessLayer(new DatabaseSettings(builder.Configuration.GetConnectionString("Default")!));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}
else
{
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
		
app.MapRazorPages();

if (app.Environment.IsDevelopment())
{
	app.MigrateDatabase();
}

app.Run();