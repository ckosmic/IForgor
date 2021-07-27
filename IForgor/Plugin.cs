using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine.SceneManagement;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using BS_Utils.Utilities;

namespace IForgor
{

	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		internal static Plugin Instance { get; private set; }
		internal static IPALogger Log { get; private set; }

		[Init]
		/// <summary>
		/// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
		/// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
		/// Only use [Init] with one Constructor.
		/// </summary>
		public void Init(IPALogger logger) {
			Instance = this;
			Log = logger;
			Log.Info("IForgor initialized.");
		}

		#region BSIPA Config
		//Uncomment to use BSIPA's config
		/*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
		#endregion

		[OnStart]
		public void OnApplicationStart() {
			Log.Debug("OnApplicationStart");
			BSEvents.gameSceneLoaded += OnGameSceneLoaded;
			BSEvents.songPaused += OnPaused;

		}

		[OnExit]
		public void OnApplicationQuit() {
			Log.Debug("OnApplicationQuit");
			BSEvents.gameSceneLoaded -= OnGameSceneLoaded;
			BSEvents.songPaused -= OnPaused;
		}

		private void OnGameSceneLoaded() {
			new GameObject("IFSaberRecorder").AddComponent<SaberRecorder>();
			new GameObject("IFNoteRecorder").AddComponent<NoteRecorder>();
			new GameObject("IFPauseUIManager").AddComponent<PauseUIManager>();

			NoteRecorder.instance.noteAData = null;
			NoteRecorder.instance.noteBData = null;
			NoteRecorder.instance.noteACutInfo = null;
			NoteRecorder.instance.noteBCutInfo = null;
		}

		private void OnPaused() {
			
		}
	}
}
