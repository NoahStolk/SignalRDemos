using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRDemos.Hubs.SimpleChat;

namespace SignalRDemos
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IWebHostEnvironment WebHostEnvironment { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			Configuration = configuration;
			WebHostEnvironment = webHostEnvironment;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSignalR(options => options.EnableDetailedErrors = !WebHostEnvironment.IsProduction());

			services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
			services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
		}

		public void Configure(IApplicationBuilder app)
		{
			if (WebHostEnvironment.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<SimpleChatHub>("/simple-chat-hub");
			});
		}
	}
}