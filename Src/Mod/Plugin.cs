using BepInEx;
using BullshitForm;

namespace Mod
{
    [BepInPlugin("zanou.op.mod", ConstMODInfo.MAIN_PLUGIN_NAME, ConstMODInfo.MAIN_PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo($"Plugin <Op MOD from Zanou> is loaded!");

            // Winform
            Wtf.Open();
        }
    }
}