using IPA;
using IPALogger = IPA.Logging.Logger;
using RuntimeUnityEditor.Core;
using LogLevel = RuntimeUnityEditor.Core.LogLevel;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine;

namespace RuntimeUnityEditor.BSIPA4
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static Logger Log { get; set; }
        internal static PluginConfig Config { get; set; }

        private RuntimeBehavior runtimeBehavior;

        [Init]
        public Plugin(IPALogger logger, Config conf)
        {
            Instance = this;
            Log = new Logger(logger);
            Config = conf.Generated<PluginConfig>();
        }

        [OnEnable]
        public void OnEnable()
        {
            runtimeBehavior = new GameObject("RuntimeUnityEditor").AddComponent<RuntimeBehavior>();
            Object.DontDestroyOnLoad(runtimeBehavior.gameObject);
        }

        [OnDisable]
        public void OnDisable()
        {
            Object.Destroy(runtimeBehavior.gameObject);
        }

        internal sealed class Logger : ILoggerWrapper
        {
            private readonly IPALogger _ipaLogger;

            public Logger(IPALogger ipaLogger)
            {
                _ipaLogger = ipaLogger;
            }

            public void Log(LogLevel logLogLevel, object content)
            {
                _ipaLogger.Log(RuntimeToIPALogLevel(logLogLevel), content.ToString());
            }

            private IPALogger.Level RuntimeToIPALogLevel(LogLevel runtimeLogLevel)
            {
                switch (runtimeLogLevel)
                {
                    case LogLevel.None: return IPALogger.Level.None;
                    case LogLevel.Info: return IPALogger.Level.Info;
                    case LogLevel.Debug: return IPALogger.Level.Debug;
                    case LogLevel.Message: return IPALogger.Level.Notice;
                    case LogLevel.Warning: return IPALogger.Level.Warning;
                    case LogLevel.Error: return IPALogger.Level.Error;
                    case LogLevel.Fatal: return IPALogger.Level.Critical;
                    default: return IPALogger.Level.Notice;
                }
            }
        }
    }
}
