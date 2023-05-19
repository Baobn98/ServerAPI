using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServerAPI.Filter;
using ServerAPI.Models;
using ServerAPI.Models.Context;
using ServerAPI.Services;
using ServerAPI.Services.Interface;
using System.Configuration;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            AddSwagger(builder);
            builder.Services.AddDbContext<HocSinhDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext"))
            );
            builder.Services.Configure<IdentityOptions>(options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

            });
            builder.Services.AddControllers(options =>
            options.Filters.Add(typeof(AuthorizeActionFilter))
            );
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                // Đọc chuỗi kết nối
                string connectstring = builder.Configuration.GetConnectionString("AppDbContext");
                // Sử dụng MS SQL Server
                options.UseSqlServer(connectstring);
            });
            
            builder.Services.AddIdentity<AppUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();
            RegisterService(builder.Services);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();   // Phục hồi thông tin đăng nhập (xác thực)
            app.UseAuthorization();   // Phục hồi thông tinn về quyền của User
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void RegisterService(IServiceCollection services)
        {
            services.AddTransient<IHocSinhService, HocSinhService>();
            services.AddTransient<IMonHocService, MonHocService>();
            services.AddTransient<ILopService, LopService>();
            services.AddTransient<IUserLoginService, UserLoginService>();
            
        }
        private static void AddSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

                };
            });

            builder.Services.AddSwaggerGen(options =>
            {
                // options.DocumentFilter<CustomModelDocumentFilter>();
                //        //options.UseReferencedDefinitionsForEnums();
                //        //options.DescribeAllEnumsAsStrings();
                //        //options.UseReferencedDefinitionsForEnums();

                options.OperationFilter<AuthorizeCheckOperationFilter>();
                options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Password = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{builder.Configuration.GetValue(typeof(string), "Identity").ToString()?.TrimEnd('/')}/connect/authorize"),
                            TokenUrl = new Uri($"{builder.Configuration.GetValue(typeof(string), "Identity").ToString()?.TrimEnd('/')}/connect/token"),
                        }
                        
                    },
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Name = "Authorization",

                });


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Name = "Authorization",
                    BearerFormat = "Bearer {token}",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""

                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme(){ Reference = new OpenApiReference(){ Type = ReferenceType.SecurityScheme, Id="OAuth2" } }, new List<string>()
                    },
                    {
                        new OpenApiSecurityScheme(){ Reference = new OpenApiReference(){ Type = ReferenceType.SecurityScheme, Id="Bearer" } }, new List<string>()
                    }
                });
            });
        }
    }
}