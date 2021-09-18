using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace MovementPatcher {
	internal class GameSettings {
		public GameSettings()
		{
			Enabled = true;
			FastWalkInterpolation = Constants.VanillaGameSettings.FastWalkInterpolation;
			JogInterpolation = Constants.VanillaGameSettings.JogInterpolation;
		}

		public GameSettings(float fastWalkInterpolation, float jogInterpolation)
		{
			Enabled = true;
			FastWalkInterpolation = fastWalkInterpolation;
			JogInterpolation = jogInterpolation;
		}

		[MaintainOrder]
		[Tooltip("Unchecking this will disable all game setting modifications.")]
		public bool Enabled;
		public float FastWalkInterpolation;
		public float JogInterpolation;
	}
}
