using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using MEC;

namespace VisuallyCoolerDMS
{
    public class VisuallyCoolerDMS : Plugin<Config>
    {
        public override string Name { get; } = "VisuallyCoolerDMS";
        public override string Description { get; } = "Makes base game Dead Mans Switch lights flash, with configuration";
        public override string Author { get; } = "Denty";
        public override Version Version { get; } = new Version(1, 0, 0, 0);
        public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);

        public static VisuallyCoolerDMS instance { get; private set; }

        private readonly EventHandler EventHandler = new();

        public static CoroutineHandle PluginCoroutine;

        public override void Enable()
        {
            instance = this;
            CustomHandlersManager.RegisterEventsHandler(EventHandler);
        }

        public override void Disable()
        {
            instance = null;
            Timing.KillCoroutines(PluginCoroutine);
            CustomHandlersManager.UnregisterEventsHandler(EventHandler);
        }
    }
}