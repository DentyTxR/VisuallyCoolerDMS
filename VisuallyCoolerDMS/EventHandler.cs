using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using UnityEngine;

namespace VisuallyCoolerDMS
{
    public class EventHandler : CustomEventsHandler
    {
        private CoroutineHandle visualsCoroutine;

        public override void OnServerDeadmanSequenceActivated()
        {
            VisuallyCoolerDMS.PluginCoroutine = Timing.RunCoroutine(DMSCountdown());
        }

        public IEnumerator<float> VisualsCoroutine()
        {
            float elapsed = 0f;
            float flashSpeed = VisuallyCoolerDMS.instance.Config.FlashSpeed;
            float minIntensity = VisuallyCoolerDMS.instance.Config.minIntensity;
            float maxIntensity = VisuallyCoolerDMS.instance.Config.maxIntensity;

            foreach (var lights in LightsController.List)
            {
                lights.OverrideLightsColor = Color.red;
            }

            yield return Timing.WaitForSeconds(VisuallyCoolerDMS.instance.Config.IntWait);

            while (DeadmanSwitch.IsSequenceActive)
            {
                elapsed += Time.deltaTime * flashSpeed;

                float pulse = (Mathf.Sin(elapsed) + 1f) * 0.5f;

                float intensity = Mathf.Lerp(minIntensity, maxIntensity, pulse);

                Color color = Color.red * intensity;

                foreach (var room in Room.List)
                    room.LightController.OverrideLightsColor = color;

                yield return Timing.WaitForOneFrame;
            }
        }

        public IEnumerator<float> DMSCountdown()
        {
            int timeleft = 104;
            visualsCoroutine = Timing.RunCoroutine(VisualsCoroutine());

            while (true)
            {
                yield return MEC.Timing.WaitForSeconds(1);
                timeleft--;

                if (timeleft == 0)
                {
                    foreach (var room in Room.List)
                    {
                        var color = room.LightController.OverrideLightsColor;
                        color.a = 0.7f;
                        color.r = 0.8f;
                        color.b = 0f;
                        color.g = 0f;
                        room.LightController.OverrideLightsColor = color;
                    }

                    Timing.KillCoroutines(visualsCoroutine);
                    yield break;
                }
            }
        }
    }
}