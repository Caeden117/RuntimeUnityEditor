using IPA.Utilities;
using RuntimeUnityEditor.Core;
using UnityEngine;

namespace RuntimeUnityEditor.BSIPA4
{
    public class RuntimeBehavior : MonoBehaviour
    {
        private RuntimeUnityEditorCore runtimeCore;

        private void Start()
        {
            runtimeCore = new RuntimeUnityEditorCore(this, Plugin.Log, UnityGame.UserDataPath);
            Plugin.Config.OnConfigChanged += Config_OnConfigChanged;
            runtimeCore.SettingsChanged += RuntimeCore_SettingsChanged;
            Config_OnConfigChanged(); // Call it ourselves to apply settings
        }

        private void OnDestroy()
        {
            Plugin.Config.OnConfigChanged -= Config_OnConfigChanged;
            runtimeCore.SettingsChanged -= RuntimeCore_SettingsChanged;
        }

        private void OnGUI()
        {
            runtimeCore?.OnGUI();
        }

        private void Update()
        {
            runtimeCore?.Update();
        }

        private void LateUpdate()
        {
            runtimeCore?.LateUpdate();
        }

        private void RuntimeCore_SettingsChanged(object sender, System.EventArgs e)
        {
            Plugin.Config.ToggleRuntimeEditor = runtimeCore.ShowHotkey;
            Plugin.Config.ShowREPLConsole = runtimeCore.ShowRepl;
        }

        private void Config_OnConfigChanged()
        {
            if (runtimeCore is null) return;
            runtimeCore.ShowRepl = Plugin.Config.ShowREPLConsole;
            DnSpyHelper.DnSpyPath = Plugin.Config.DnSpyPath;
            DnSpyHelper.DnSpyArgs = Plugin.Config.AdditionalDnSpyArguments;
            runtimeCore.ShowHotkey = Plugin.Config.ToggleRuntimeEditor;
        }
    }
}
