using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace MovementPatcher {
	class GameSettings {
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

		public bool AddGameSettingsToPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
		{
			if ( !Enabled ) return false;
			state.PatchMod.GameSettings.Add(new GameSettingFloat( state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease ) {
				EditorID = Constants.FastWalkInterpolationEditorID,
				Data = FastWalkInterpolation
			} );
			state.PatchMod.GameSettings.Add(new GameSettingFloat( state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease ) {
				EditorID = Constants.JogInterpolationEditorID,
				Data = JogInterpolation
			} );
			return true;
		}
	}
}
