using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using MiniEcommerceWebApi;
using MiniEcommerceWebApi.MappingProfile;
using MiniEcommerceWebApi.Repository;


using Microsoft.Data.SqlClient;
using MiniEcommerceWebApi.Filters;


var builder = WebApplication.CreateBuilder(args);

var sqldb = builder.Configuration.GetConnectionString("SqlDb");
var connectionString = new SqlConnection(sqldb);

builder.Services.AddDbContextPool<EcommerceDbContext>(
            options => options.UseSqlServer(connectionString,
            providerOptions => providerOptions.EnableRetryOnFailure())
          .EnableSensitiveDataLogging());

builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//            .AddCookie(options =>
//            {
//                options.LoginPath = "/Account/Login";
//            });

//var myapp = builder.Build();
//// Configure the HTTP request pipeline.
//if (!myapp.Environment.IsDevelopment())
//{
//    myapp.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    myapp.UseHsts();
//}


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce.Web.Api", Version = "v1" });



});

builder.Services.AddHttpContextAccessor();

var MyAllowedOrigins = "_myCORS";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowedOrigins,
                        policy =>
                        {
                            policy.WithOrigins("http://localhost:4200")
                             .AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowCredentials();
                        });
});

ClientApiDependency.Resgister(builder);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilters));
    options.Filters.Add(typeof(HttpsOnlyFilter));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce.Web.Api v1");

});

app.UseCors(MyAllowedOrigins);
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();