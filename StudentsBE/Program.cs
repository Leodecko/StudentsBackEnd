using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentsBE.Context;
using StudentsBE.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("webApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddDbContext<ContextDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
  var context = scope.ServiceProvider.GetRequiredService<ContextDB>();
  //context.Database.EnsureCreated();
  context.Database.Migrate();
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("webApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

