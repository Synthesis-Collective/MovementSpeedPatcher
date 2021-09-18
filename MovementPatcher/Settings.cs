using MovementPatcher.ConfigHelpers;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Noggog;
using System.Collections.Generic;

namespace MovementPatcher
{
    internal class Settings {
		[MaintainOrder]

		public GameSettings GameSettings = new(Constants.DefaultGameSettings.FastWalkInterpolation, Constants.DefaultGameSettings.JogInterpolation);

		[SettingName("Movement Types")]
		public List<MovementTypeSettings> MovementTypes = new() {
			new(Skyrim.MovementType.NPC_Sprinting_MT,
				moveSpeeds: new (
					left: new (
						walk: 0F,
						run: 0F
					),
					right: new(
						walk: 0F,
						run: 0F
					),
					forward: new(
						walk: 450F,
						run: 450F
					),
					back: new (
						walk: 250.839996F,
						run: 250.839996F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 90F,
					rotateInPlaceRun: 90F,
					rotateRunning: 90F
				)
			),
			new(Skyrim.MovementType.NPC_Sneaking_MT,
				moveSpeeds: new (
					left: new (
						walk: 95F,
						run: 145F
					),
					right: new(
						walk: 95F,
						run: 145F
					),
					forward: new(
						walk: 95.5F,
						run: 160F
					),
					back: new (
						walk: 70F,
						run: 110F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 90F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_BowDrawn_MT,
				moveSpeeds: new (
					left: new (
						walk: 76.809998F,
						run: 115F
					),
					right: new(
						walk: 74.889999F,
						run: 115F
					),
					forward: new(
						walk: 110F,
						run: 135F
					),
					back: new (
						walk: 65.110001F,
						run: 98F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 90F,
					rotateInPlaceRun: 90F,
					rotateRunning: 90F
				)
			),
			new(Skyrim.MovementType.NPC_Default_MT,
				moveSpeeds: new (
					new (
						walk: 110F,
						run: 305F
					),
					new(
						walk: 108F,
						run: 305F
					),
					new(
						walk: 110F,
						run: 305F
					),
					back: new (
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_Blocking_MT,
				moveSpeeds: new (
					new (
						walk: 112F,
						run: 112F
					),
					new(
						walk: 112F,
						run: 112F
					),
					new(
						walk: 112F,
						run: 112F
					),
					back: new (
						walk: 97F,
						run: 97F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F
				)
			),
			new(Skyrim.MovementType.NPC_Swimming_MT,
				moveSpeeds: new (
					left: new (
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new (
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_1HM_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_2HM_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_Bow_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_MagicCasting_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 140F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_Attacking_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_PowerAttacking_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_Attacking2H_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_Blocking_ShieldCharge_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			),
			new(Skyrim.MovementType.NPC_BowDrawn_QuickShot_MT,
				moveSpeeds: new(
					left: new(
						walk: 110F,
						run: 305F
					),
					right: new(
						walk: 108F,
						run: 305F
					),
					forward: new(
						walk: 110F,
						run: 305F
					),
					back: new(
						walk: 97F,
						run: 165F
					)
				),
				rotateSpeeds: new(
					rotateInPlaceWalk: 180F,
					rotateInPlaceRun: 180F,
					rotateRunning: 180F
				)
			)
		};

		[Tooltip("Any plugins listed here will not be overridden. (This setting will be improved in future versions!)")]
		public List<ModKey> BlacklistedMods = new()
        {
			ModKey.FromNameAndExtension("Mortal Enemies.esp"),
			ModKey.FromNameAndExtension("Wildcat - Combat of Skyrim.esp"),
			ModKey.FromNameAndExtension("consistent sensitivity.esp"),
        };

		[SettingName("Enable Exceptions")]
		[Tooltip("If settings are invalid, throws an exception to interrupt the patcher process. Disabled by default.")]
		public bool ThrowExceptions;

		public bool IsModKeyBlacklisted(ModKey modkey)
		{
			return BlacklistedMods.Contains(modkey);
		}

		private MovementTypeSettings GetApplicableMovementType(MovementType movt)
		{
			foreach ( var movtSetting in MovementTypes ){
				if ( movtSetting.ShouldSkip() )
					continue;
				if ( movtSetting.MoveTypeFormLink.FormKey.Equals( movt.FormKey ) )
					return movtSetting;
			}
			return Constants.NullSettings;
		}

		public MovementType ApplySettingsToMovementType(MovementType movt, out int modified)
		{
			modified = 0;
			var stats = GetApplicableMovementType(movt);

			if (stats == Constants.NullSettings || stats.ShouldSkip() )
				return movt;

			return stats.SetMovementTypeValues(movt, out modified);
		}

		public bool ShouldSkip()
		{
			return !MovementTypes.Any() && !GameSettings.Enabled;
		}
	}
}
