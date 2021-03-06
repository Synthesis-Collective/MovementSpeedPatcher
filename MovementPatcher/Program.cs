using System;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace MovementPatcher {
	public static class Program {
		private static Lazy<Settings> _lazySettings = null!;
		private static Settings Settings => _lazySettings.Value;

        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
				.SetAutogeneratedSettings( "Settings", "settings.json", out _lazySettings )
                .SetTypicalOpen(GameRelease.SkyrimSE, "MovementSpeedPatcher.esp")
                .Run(args)
                .ConfigureAwait(false);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
		{
			var counter = 0;

			Console.WriteLine("\n\n=== Movement Speed Patcher ===\n\nRunning Patcher...\n");

			// Check if settings are valid
			if ( Settings.ShouldSkip() ) {
				PrintLogEnd(counter, "Patcher Failed! No settings were enabled.");
				return;
			}

			// Game Settings
			if (Settings.GameSettings.Enabled)
            {
				state.PatchMod.GameSettings.Add(new GameSettingFloat(state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease)
				{
					EditorID = Constants.FastWalkInterpolationEditorID,
					Data = Settings.GameSettings.FastWalkInterpolation
				});
				Console.WriteLine($"Set Game Setting {Constants.FastWalkInterpolationEditorID} = {Settings.GameSettings.FastWalkInterpolation}");
				++counter;
				state.PatchMod.GameSettings.Add(new GameSettingFloat(state.PatchMod.GetNextFormKey(), state.PatchMod.SkyrimRelease)
				{
					EditorID = Constants.JogInterpolationEditorID,
					Data = Settings.GameSettings.JogInterpolation
				});
				Console.WriteLine($"Set Game Setting {Constants.JogInterpolationEditorID} = {Settings.GameSettings.JogInterpolation}");
				++counter;
			}

			// Movement Types
			foreach ( var movt in state.LoadOrder.PriorityOrder.MovementType().WinningOverrides() ) {
				// skip null records, records from blacklisted mods, and skip any movement types without editor IDs as they probably shouldn't be touched
				if ( movt == null || movt.EditorID == null || Settings.IsModKeyBlacklisted(movt.FormKey.ModKey) )
					continue;
				var moveType = movt.DeepCopy(); // deep copy movement type to temp obj
				if ( moveType == null )
					continue;
				Console.WriteLine( "Processing Movement Type: \"" + movt.EditorID + '\"' );
				moveType = Settings.ApplySettingsToMovementType( moveType, out var modifiedRecordCount );
				if ( modifiedRecordCount > 0 ) {
					state.PatchMod.MovementTypes.Set( moveType );
					++counter;
					Console.WriteLine($"\tModified {modifiedRecordCount} values.");
				}
				Console.WriteLine(); // keep a newline between entries
			}
			PrintLogEnd(counter);
        }

		private static void PrintLogEnd(int changeCount, string message = "Patcher Complete.")
        {
			Console.Write($"\n\n{message}\nModified {changeCount} records.\n");
        }
    }
}
