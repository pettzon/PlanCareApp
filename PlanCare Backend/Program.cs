using System.Diagnostics;
using PlanCare_Backend.Extension;
using PlanCare_Backend.Hub;
using Scalar.AspNetCore;

public sealed class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
#if DEBUG
        builder.Services.AddCors(o => o.AddPolicy("AllOrigins", builder =>
        {
            builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        }));
#endif

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddSignalR();
        builder.Services.AddExtraServices();

        WebApplication app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        
#if DEBUG
        app.UseCors("AllOrigins");
#endif
        
        app.UseHttpsRedirection();
        //app.UseAuthentication();
        app.MapHub<RegistrationLiveHub>("/registration");
        app.MapControllers();
        app.Run();
    }
}