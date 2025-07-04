using FirstApi.Contexts;
using FirstApi.Interfaces;
using FirstApi.Repositories; 
using FirstApi.Models; 
using FirstApi.Services; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FirstApi.Misc;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using FirstApi.Policies;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Clinic API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.WriteIndented = true;
                });

builder.Logging.AddLog4Net();


#region  Repositories
builder.Services.AddTransient<IRepository<int, Doctor>, DoctorRepository>();
builder.Services.AddTransient<IRepository<int, Patient>, PatientRepository>();
builder.Services.AddTransient<IRepository<int, Speciality>, SpecialityRepository>();
builder.Services.AddTransient<IRepository<string, Appointment>, AppointmentRepository>();
builder.Services.AddTransient<IRepository<int, DoctorSpeciality>, DoctorSpecialityRepository>();
builder.Services.AddTransient<IRepository<string, User>, UserRepository>();
builder.Services.AddTransient<IRepository<int, Files>, FileRepository>();
#endregion

#region Services
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IOtherContextFunctionities, OtherFuncinalitiesImplementation>();
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IFileService, FileService>();
#endregion


// #region AuthenticationFilter
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                 .AddJwtBearer(options =>
//                 {
//                     options.TokenValidationParameters = new TokenValidationParameters
//                     {
//                         ValidateAudience = false,
//                         ValidateIssuer = false,
//                         ValidateLifetime = true,
//                         ValidateIssuerSigningKey = true,
//                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Keys:JwtTokenKey"]))
//                     };
//                 });
// #endregion
// JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath = "/signin-google";
    options.SaveTokens = true;
});



#region Authorization
builder.Services.AddTransient<IAuthorizationHandler, DoctorCanDeleteAppointmentHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DoctorCanDeleteAppointment", policy =>
        policy.Requirements.Add(new DoctorCanDeleteAppointmentRequirement()));
});
#endregion

#region  Misc
builder.Services.AddAutoMapper(typeof(User));
builder.Services.AddTransient<AppointmentMapper>();
builder.Services.AddScoped<CustomExceptionFilter>();
#endregion


builder.Services.AddDbContext<ClinicContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#region CORS
builder.Services.AddCors(options=>{
    options.AddDefaultPolicy(policy=>{
        policy.WithOrigins("http://127.0.0.1:5501")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseCors();
app.MapHub<NotificationHub>("/notificationhub");
 
app.MapControllers();

app.Run();