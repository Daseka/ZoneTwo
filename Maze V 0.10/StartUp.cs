using MazeV.MazeLogic;
using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.Units;
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
            services.AddScoped<IMazeNodeDataBuilderSettings, MazeNodeDataBuilderSettings>();

            services.AddScoped<MazeNodeDataBuilder>();
            services.AddSingleton<Randomizer>();
            services.AddScoped<IVisualizer, CanvasVisualizer>();

            services.AddScoped<Maze>();
            services.AddScoped<UnitMover>();
            services.AddScoped<IMovementLogic, DefaultMovementLogic>();

            services.AddScoped<IUnitList, UnitList>();

            services.AddScoped<UnitFactory>();
            services.AddSingleton<DefaultSettings>();

            services.AddSingleton<NodeBuilder>();
            services.AddSingleton<CoinBuilder>();
        }

        public T GetService<T>()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetService<T>();
        }
    }
}