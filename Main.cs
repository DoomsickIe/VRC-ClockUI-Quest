using MelonLoader;
using UnityEngine;
using ReMod.Core.VRChat;
using ReMod.Core.Unity;
using System.Collections;
using UnhollowerRuntimeLib;
using ClockUI.Internals;

[assembly: MelonInfo(typeof(ClockUI.Main), ClockUI.BuildInfo.Name, ClockUI.BuildInfo.Version, ClockUI.BuildInfo.Author, ClockUI.BuildInfo.DownloadLink)]
[assembly: MelonGame("VRChat")]

namespace ClockUI;

public class Main : MelonMod
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
    }

    public override void OnLateUpdate() => Clock.ClockUpdate();
}