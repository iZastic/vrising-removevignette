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
