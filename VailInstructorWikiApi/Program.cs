using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VailInstructorWikiApi;
using VailInstructorWikiApi.Auth;
using VailInstructorWikiApi.DTOs.User;
using VailInstructorWikiApi.Models.Configuration;
using VailInstructorWikiApi.Repos;
using VailInstructorWikiApi.Services;

var builder = WebApplication.CreateBuilder(args);
IMapper mapper = AutoMapperConfig.Configure();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});
builder.Services.AddAutoMapper(typeof(IStartup));
builder.Services.AddSingleton(mapper);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Add services to the container.
builder.Services.AddTransient<AreaService>();
builder.Services.AddTransient<DrillService>();
builder.Services.AddTransient<LevelService>();
builder.Services.AddTransient<RunDisciplineDrillService>();
builder.Services.AddTransient<RunDisciplineService>();
builder.Services.AddTransient<RunService>();
builder.Services.AddTransient<SkillService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        policy => policy.Requirements.Add(new RoleRequirement(AuthRole.Admin)));
    options.AddPolicy("Contributor",
        policy => policy.Requirements.Add(new RoleRequirement(AuthRole.Contributor)));
});

builder.Services.Configure<MailSettings>(options => builder.Configuration.GetSection("MailSettings").Bind(options));

// Add Repos
builder.Services.AddTransient<UserRepo>();

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AnyOrigin", builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AnyOrigin");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


app.Run();

