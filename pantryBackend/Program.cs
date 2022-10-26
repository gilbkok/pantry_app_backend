using Microsoft.EntityFrameworkCore;
using pantryBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()//I used this to resolve the json reference handling error==>search the meaning of this service????
    .AddNewtonsoftJson(options =>//this is the error in swagger: Net Core 3.0 possible object cycle was detected which is not supported
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<pantryDbContext>(options =>
{
    options.UseSqlServer("Data Source=localhost,1433; Initial Catalog=pantryBackend; Integrated Security=False;User Id=sa;Password=Your_password123");
    //TODO: add using, trycatch
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<pantryDbContext>();
        pantryBackendInitializer.Initialize1(context);
        pantryBackendInitializer.Initialize2(context);
        pantryBackendInitializer.Initialize3(context);
    }
}

app.UseHttpsRedirection();

app.UseCors("Policy1");

app.UseAuthorization();

app.MapControllers();

app.Run();
