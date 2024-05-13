using HarmonyLib;
using ProjectM.UI;
using UnityEngine;
using UnityEngine.Rendering;

namespace RemoveVignette;

[HarmonyPatch]
internal static class ConnectingView_Patch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(ConnectingView), nameof(ConnectingView.Update))]
    private static void Update(ConnectingView __instance)
    {
        if (__instance._Ready)
        {
            var postProcess = GameObject.Find("Scene PostProcess");
            var volume = postProcess.GetComponent<Volume>();

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
