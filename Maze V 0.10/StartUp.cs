using MazeV.MazeLogic;
using Microsoft.Extensions.DependencyInjection;

namespace MazeV
{
    public class StartUp
    {
        public ServiceCollection Services { get; }        

        public StartUp()
        {
            Services = new ServiceCollection();
            ConfigureServices(Services);            
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Form1>();
            services.AddScoped<IAxisFactory, AxisFactory>();
            services.AddScoped<IMazeViewDataFactory, MazeViewDataFactory>();
        }

        public T GetService<T>()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetService<T>();
        }
    }
}