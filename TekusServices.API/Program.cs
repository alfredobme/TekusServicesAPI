using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TekusServices.API.Extensions;
using TekusServices.Application.Mappings;
using TekusServices.Domain.Entities;
using TekusServices.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Para habilitar CORS
// Desactivar en Produccion
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Para que funcione la API de paises
System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("name=SQLConnection")
);

builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();

// Para AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TekusServiceAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and the JWT token.\n\nExample: **Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...**"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Autenticacion JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar migraciones al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User
            {
                Code = "admin",
                Password = "admin321"
            },
            new User
            {
                Code = "pruebas",
                Password = "pruebas"
            }
        );
        db.SaveChanges();
    }

    if (!db.Providers.Any())
    {
        db.Providers.AddRange(
            new Provider
            {
                Nit = "900123456",
                Name = "Importaciones Tekus S.A.S.",
                Active = true,
            },
            new Provider
            {
                Nit = "800987654",
                Name = "Soluciones Digitales S.A.",
                Active = true,
            },
            new Provider
            {
                Nit = "8779654321",
                Name = "Viajes Sin Regreso S.A.",
                Active = true,
            },
            new Provider
            {
                Nit = "9001112233",
                Name = "Soluciones Digitales Ltda.",
                Active = true,
            },
            new Provider
            {
                Nit = "9012345678",
                Name = "Transporte Galáctico S.A.S.",
                Active = false,
            },
            new Provider
            {
                Nit = "9023456789",
                Name = "Importadora Andina S.A.",
                Active = true,
            },
            new Provider
            {
                Nit = "9034567890",
                Name = "Comercializadora Total",
                Active = true,
            },
            new Provider
            {
                Nit = "9045678901",
                Name = "Servicios Tecnológicos del Norte",
                Active = false,
            },
            new Provider
            {
                Nit = "9056789012",
                Name = "Agroindustrias del Caribe S.A.S.",
                Active = true,
            },
            new Provider
            {
                Nit = "9067890123",
                Name = "Construcciones Futuro Verde",
                Active = true,
            }
        );
        db.SaveChanges();
    }

    if (!db.ProviderCustomFields.Any())
    {
        db.ProviderCustomFields.AddRange(
            new ProviderCustomField
            {
                ProviderId = 1,
                FieldName = "Tipo de Proveedor",
                FieldValue = "Tecnología"
            },
            new ProviderCustomField
            {
                ProviderId = 2,
                FieldName = "Contacto Adicional",
                FieldValue = "31532123321"
            },
            new ProviderCustomField
            {
                ProviderId = 2,
                FieldName = "Contacto Adicional",
                FieldValue = "31532123321"
            },
            new ProviderCustomField
            {
                ProviderId = 3,
                FieldName = "Correo secundario",
                FieldValue = "contacto@servicio.com"
            },
            new ProviderCustomField
            {
                ProviderId = 3,
                FieldName = "Persona de contacto",
                FieldValue = "Laura Martínez"
            },
            new ProviderCustomField
            {
                ProviderId = 4,
                FieldName = "Teléfono de oficina",
                FieldValue = "+57 601 5556677"
            },
            new ProviderCustomField
            {
                ProviderId = 4,
                FieldName = "Horario de atención",
                FieldValue = "Lunes a viernes de 8:00 a.m. a 6:00 p.m."
            },
            new ProviderCustomField
            {
                ProviderId = 5,
                FieldName = "Canal de soporte",
                FieldValue = "WhatsApp +57 312 9988776"
            },
            new ProviderCustomField
            {
                ProviderId = 6,
                FieldName = "Régimen tributario",
                FieldValue = "Simplificado"
            },
            new ProviderCustomField
            {
                ProviderId = 7,
                FieldName = "Sitio web",
                FieldValue = "www.futuroverde.com"
            }

        );
        db.SaveChanges();
    }

    if (!db.Services.Any())
    {
        db.Services.AddRange(
            new Service
            {
                Name = "Descarga espacial de contenidos",
                HourlyRate = 5.5M,
                Active = true,
                ProviderId = 1,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                    new() { Code = "PE", Name = "Perú" },
                    new() { Code = "MX", Name = "México" }
                ]
            },
            new Service
            {
                Name = "Desaparición forzada de bytes",
                HourlyRate = 500,
                Active = true,
                ProviderId = 1,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                    new() { Code = "PE", Name = "Perú" },
                ]
            },
            new Service
            {
                Name = "Gestión remota de dispositivos",
                HourlyRate = 5000,
                Active = true,
                ProviderId = 2,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                ]
            },
            new Service
            {
                Name = "Envio de Satelites",
                HourlyRate = 15000,
                Active = true,
                ProviderId = 2,
                Countries =
                [
                    new() { Code = "AR", Name = "Argentina" },
                    new() { Code = "CL", Name = "Chile" },
                ]
            },
            new Service
            {
                Name = "Transporte de Mascotas",
                HourlyRate = 15.5M,
                Active = true,
                ProviderId = 3,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                    new() { Code = "MX", Name = "México" }
                ]
            },
            new Service
            {
                Name = "Guaderia de Mascotas",
                HourlyRate = 25.5M,
                Active = true,
                ProviderId = 3,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                    new() { Code = "MX", Name = "México" }
                ]
            },
            new Service
            {
                Name = "Adiestramiento de Mascotas",
                HourlyRate = 5,
                Active = true,
                ProviderId = 3,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                    new() { Code = "MX", Name = "México" }
                ]
            },
            new Service
            {
                Name = "Paseos para Mascotas",
                HourlyRate = 50,
                Active = true,
                ProviderId = 3,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                    new() { Code = "MX", Name = "México" },
                    new() { Code = "AR", Name = "Argentina" },
                ]
            },
            new Service
            {
                Name = "Limpieza Virtual de Oficinas",
                HourlyRate = 75,
                Active = true,
                ProviderId = 4,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                    new() { Code = "CL", Name = "Chile" },
                    new() { Code = "PE", Name = "Perú" },
                ]
            },
            new Service
            {
                Name = "Soporte Técnico Remoto",
                HourlyRate = 120,
                Active = true,
                ProviderId = 5,
                Countries =
                [
                    new() { Code = "CO", Name = "Colombia" },
                    new() { Code = "MX", Name = "México" },
                    new() { Code = "EC", Name = "Ecuador" },
                ]
            }
        );
        db.SaveChanges();
    }
}

// Para habilitar CORS
// Desactivar en Produccion
app.UseCors("AllowAllOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();