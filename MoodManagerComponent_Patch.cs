using HarmonyLib;
using ProjectM.Presentation;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace RemoveVignette
{
    [HarmonyPatch]
    internal static class MoodManagerComponent_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(MoodManagerComponent), nameof(MoodManagerComponent.OnEnable))]
        private static void OnEnable(MoodManagerComponent __instance)
        {
            var sppTransform = __instance.transform.FindChild("Scene PostProcess");
            var volume = sppTransform.GetComponent<Volume>();
            foreach (var vc in volume.profile.components)
            {
                if (vc.name.StartsWith("Vignette"))
                {
                    vc.active = false;
                    break;
                }
            }
        }
    }
}
