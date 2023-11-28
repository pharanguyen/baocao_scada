using Microsoft.Extensions.Configuration;

namespace ERPProject
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor()
              .AddCircuitOptions(opt =>
              {
                  opt.DetailedErrors = _env.IsDevelopment();
              });
        }
    }
}
