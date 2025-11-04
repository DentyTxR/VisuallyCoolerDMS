using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using System.ComponentModel;

namespace VisuallyCoolerDMS
{
    public class Config
    {
        [Description("how long it takes for the lights to flash after DMS has been activated")]
        public float IntWait { get; set; } = 15f;

        [Description("how fast the lights will flash")]
        public float FlashSpeed { get; set; } = 2.5f;

        [Description("min light intensity")]
        public float minIntensity { get; set; } = 0.4f;

        [Description("max light intensity")]
        public float maxIntensity { get; set; } = 1f;
    }
}