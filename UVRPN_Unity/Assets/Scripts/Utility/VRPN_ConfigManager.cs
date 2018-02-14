using System;
using System.Collections;
using System.IO;
using SharpConfig;
using UnityEngine;
using UVRPN.Core;

namespace UVRPN.Utility
{
    /// <summary>
    /// This component allows saving and loading the host address of the VRPN_Manager to a config file.
    /// </summary>
    [RequireComponent(typeof(VRPN_Manager))]
    public class VRPN_ConfigManager : MonoBehaviour
    {
        public Configuration cfg = new Configuration();

        [SerializeField] private string filename = "vrpn.cfg";

        [SerializeField] private bool debugLog;

        private VRPN_Manager _manager;

        private VRPN_Manager Manager
        {
            get
            {
                if (!_manager) _manager = GetComponent<VRPN_Manager>();
                return _manager;
            }
        }


        IEnumerator Start()
        {
            if (!File.Exists(filename))
            {
                if (debugLog) Debug.Log("Setting up a default config since no file was found!");
                cfg = new Configuration();
                SaveConfig();
                yield break;
            }

//            yield return null;

            LoadConfig();
            SaveConfig();
        }

        /// <summary>
        /// Saves the current config to disc.
        /// </summary>
        public void SaveConfig()
        {
            if (debugLog) Debug.Log("Saving config...");

            cfg["VRPN Host"]["Hostname"].StringValue = Manager.Hostname;

            // Save the configuration.
            cfg.SaveToFile(filename);
        }

        /// <summary>
        /// Loads a config from disc applies it!
        /// </summary>
        public void LoadConfig()
        {
            if (debugLog) Debug.Log("Loading config from file ...");

            try
            {
                cfg = Configuration.LoadFromFile(filename);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Loading of config file failed! " + e);
                if (cfg == null) cfg = new Configuration();
                return;
            }

            var hostname = cfg["VRPN Host"]["Hostname"].StringValue;

            Manager.Hostname = hostname;
        }

        public string GetFilename()
        {
            return filename;
        }
    }
}