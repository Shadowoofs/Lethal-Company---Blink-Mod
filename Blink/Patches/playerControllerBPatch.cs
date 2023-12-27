using UnityEngine;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections;
using GameNetcodeStuff;

namespace Blink.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        private static bool simulateLossOfLineOfSight = false;
        private static ManualLogSource logger;
        private static float lastBlinkTime = 0f;
        private static float blinkInterval = 4.5f;
        private static bool ignoreLineOfSight = false;
        private static float ignoreLineOfSightDuration = 1.5f;

        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        private static void AwakePatch(PlayerControllerB __instance)
        {
            BepInEx.Logging.Logger.CreateLogSource("Blink");
            BepInEx.logging.LogInfo("Eyes are getting dry!");
        }

        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static void UpdatePatch(PlayerControllerB __instance)
        {
            if (Time.time - lastBlinkTime >= blinkInterval)
            {
                SimulateLossOfLineOfSight(__instance);
                lastBlinkTime = Time.time;
            }

            if (ignoreLineOfSight)
            {
                ignoreLineOfSightDuration -= Time.deltaTime;
                if (ignoreLineOfSightDuration <= 1.5f)
                {
                    ignoreLineOfSight = false;
                }
            }
        }

        private static void SimulateLossOfLineOfSight(PlayerControllerB playerController)
        {
            simulateLossOfLineOfSight = true;

            ignoreLineOfSight = true;

            playerController.HasLineOfSightToPosition(Vector3.zero, 45f, 15, -1f);

            simulateLossOfLineOfSight = false;

          
        }
    }
}
