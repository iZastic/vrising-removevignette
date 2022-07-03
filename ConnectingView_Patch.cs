using HarmonyLib;
using ProjectM.UI;
using UnityEngine;
using UnityEngine.Rendering;

namespace RemoveVignette;

[HarmonyPatch]
internal static class ConnectingView_Patch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(ConnectingView), nameof(ConnectingView.BackgroundButton_OnClick))]
    private static void BackgroundButton_OnClick()
    {
        var postProcess = GameObject.Find("Scene PostProcess");
        Plugin.Logger.LogMessage($"Scene PostProcess Exists: {postProcess != null}");

        var volume = postProcess.GetComponent<Volume>();
        Plugin.Logger.LogMessage($"Volume Exists: {volume != null}");

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
