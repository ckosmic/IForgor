using IForgor.Installers;
using IPA;
using IPA.Logging;
using ModestTree;
using SiraUtil.Zenject;

namespace IForgor
{
	[Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
	public class Plugin
	{
		internal static Logger Log { get; private set; }

		[Init]
		/// <summary>
		/// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
		/// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
		/// Only use [Init] with one Constructor.
		/// </summary>
		public void Init(Logger logger, Zenjector zenject)
		{
			Log = logger;

			zenject.Install<IFAppInstaller>(Location.App);
			zenject.Install<IFGameInstaller>(Location.StandardPlayer);
		}
	}
}
