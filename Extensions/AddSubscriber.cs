using event_bus.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace event_bus.Extensions
{
    public static class AppBuilderExtensions<T> where T:class
    {
        private static Subscriber<T> _listener { get; set; }

        public static IApplicationBuilder UseSubscriber<T>(this IApplicationBuilder app)
        {
            _listener = <Subscriber<T>>app.ApplicationServices.GetService<Subscriber<T>>();

            var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            _listener.Register();
        }

        private static void OnStopping()
        {
            _listener.Deregister();
            Console.WriteLine("Closed subscriber");
        }
    }
}