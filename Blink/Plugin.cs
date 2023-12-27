using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinkv2
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class BlinkBasev2 : BaseUnityPlugin
    {

        private const string modGUID = "Poseiden.Blink";
        private const string modName = "Blink";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static BlinkBasev2 instance;

        internal ManualLogSource mls;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Don't forget to blink");

            harmony.PatchAll(typeof(BlinkBasev2));

        }

    }
}