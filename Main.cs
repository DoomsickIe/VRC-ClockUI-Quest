using MelonLoader;
using UnityEngine;
using ReMod.Core.VRChat;
using ReMod.Core.Unity;
using System.Collections;
using UnhollowerRuntimeLib;

[assembly: MelonInfo(typeof(ClockUI.ClockUI), ClockUI.BuildInfo.Name, ClockUI.BuildInfo.Version, ClockUI.BuildInfo.Author, ClockUI.BuildInfo.DownloadLink)]
[assembly: MelonGame("VRChat")]

namespace ClockUI;

public static class BuildInfo
{
    public const string Name = "ClockUI"; // Name of the Mod.  (MUST BE SET)
    public const string Description = "Mod that displays the user's current time in menus"; // Description for the Mod.  (Set as null if none)
    public const string Author = "Doomsickle"; // Author of the Mod.  (MUST BE SET)
    public const string Company = null; // Company that made the Mod.  (Set as null if none)
    public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
    public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
}

public class ClockUI : MelonMod
{
    public static GameObject UserInterface;
    
    #pragma warning disable CS0672
    public override void OnApplicationStart()
    #pragma warning restore CS0672
    {
        ClassInjector.RegisterTypeInIl2Cpp<EnableDisableListener>();
        MelonCoroutines.Start(WaitForUI());
    }

    IEnumerator WaitForUI()
    {
        yield return MenuEx.WaitForUInPatch();
        UserInterface = MenuEx.userInterface;
        Interface.UIinit();
        Config.Init();
    }

    public override void OnLateUpdate()
    {
        Interface.ClockUpdate();
    }
}