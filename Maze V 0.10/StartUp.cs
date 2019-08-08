using MazeV.MazeLogic;
using MazeV.MazeLogic.CollectableItems;
using MazeV.MazeLogic.Drawing;
using MazeV.MazeLogic.Keybindings;
using MazeV.MazeLogic.MazeNodes;
using MazeV.MazeLogic.MazeNodes.Settings;
using MazeV.MazeLogic.MazeViews;
using MazeV.MazeLogic.Movement;
using MazeV.MazeLogic.Rotation;
using MazeV.MazeLogic.Settings;
using MazeV.MazeLogic.Units;
using MazeV.MazeLogic.Validators;
using MazeV.MazeLogic.Visualizer;
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
            services.AddScoped<IVisualizer, CanvasVisualizer>();
            services.AddScoped<Maze>();
            services.AddScoped<UnitMover>();
            services.AddScoped<IMovementLogic, DefaultMovementLogic>();
            services.AddScoped<IUnitList, UnitList>();
            services.AddScoped<UnitFactory>();

            services.AddSingleton<DefaultSettings>();
            services.AddSingleton<NodeBuilder>();
            services.AddSingleton<CoinBuilder>();
            services.AddSingleton<Randomizer>();
            services.AddSingleton<Keybindings>();
            services.AddSingleton<Validator>();
        }

        public T GetService<T>()
        {
            var provider = Services.BuildServiceProvider();
            return provider.GetService<T>();
        }
    }
}