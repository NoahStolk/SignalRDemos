using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRDemos.Hubs.SimpleChat;
using System.Linq;

namespace SignalRDemos
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Env { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			Env = env;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSignalR(options => options.EnableDetailedErrors = !Env.IsProduction());

			services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
			services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

			// Blazor
			services.AddRazorPages();
			services.AddServerSideBlazor();

			services.AddResponseCompression(opts =>
			{
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				// Blazor
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");

				endpoints.MapHub<SimpleChatHub>("/simple-chat-hub");
			});
		}
	}
}