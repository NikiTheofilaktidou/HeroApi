using Microsoft.EntityFrameworkCore;
using HeroApi;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HeroApiDatabase");
// Add services to the container.
//builder.Services.AddTransient<DataSeeder>();
builder.Services.AddControllers();
builder.Services.AddDbContext<HeroContext>(opt =>
    opt.UseSqlServer(connectionString));
var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {
        //builder.WithOrigins("http://localhost:800").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        //builder.SetIsOriginAllowed(origin => true);
    });
});

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "HeroApi", Version = "v1" });
//});

var app = builder.Build();

//if (args.Length == 1 && args[0].ToLower() == "seeddata")
//    SeedData(app);

////Seed Data
//void SeedData(IHost app)
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var service = scope.ServiceProvider.GetService<DataSeeder>();
//        service.Seed();
//    }
//}

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors(devCorsPolicy);
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


