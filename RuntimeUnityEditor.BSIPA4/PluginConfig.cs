using IPA.Config.Stores.Attributes;
using System;
using UnityEngine;

namespace RuntimeUnityEditor.BSIPA4
{
    public class PluginConfig
    {
        public virtual string DnSpyPath { get; set; } = string.Empty;
        public virtual string AdditionalDnSpyArguments { get; set; } = string.Empty;
        public virtual bool ShowREPLConsole { get; set; } = true;
        [UseConverter]
        public virtual KeyCode ToggleRuntimeEditor { get; set; } = KeyCode.G; // To replace "UnityIPADebugger" default hotkey

        public event Action OnConfigChanged;

        public virtual void Changed()
        {
            OnConfigChanged?.Invoke();
        }
    }
}
