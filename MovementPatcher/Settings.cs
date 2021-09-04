using Mutagen.Bethesda.WPF.Reflection.Attributes;
using MovementPatcher.ConfigHelpers;
using System.Collections.Generic;
using Mutagen.Bethesda.FormKeys.SkyrimSE;

namespace MovementPatcher {
	class Settings {
		[MaintainOrder]
		public GameSettings GameSettings = new(Constants.DefaultGameSettings.FastWalkInterpolation, Constants.DefaultGameSettings.JogInterpolation);

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
				)),
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
				)),
			new(Skyrim.MovementType.NPC_BowDrawn_MT,				
				moveSpeeds: new (
					left: new ( 
						walk: 76.809998F,
						run: 115F 
					),
					right: new(
						74.889999F, 
						115F
					),
					forward: new( 
						110F, 
						135F
					),
					back: new (
						65.110001F,
						98F 
					)
				), 
				rotateSpeeds: new(
					90F,
					90F, 
					90F
				)),
			new(Skyrim.MovementType.NPC_Default_MT,					
				moveSpeeds: new (
					new ( 
						walk: 110F,
						run: 305F 
					), 
					new(
						108F, 
						305F
					), 
					new(
						110F, 
						305F
					), 
					back: new (
						97F, 
						165F
					)
				), 
				rotateSpeeds: new(
					180F, 
					180F, 
					180F
				)),
			new(Skyrim.MovementType.NPC_Blocking_MT,				
				moveSpeeds: new (
					new ( 
						walk: 112F, 
						run: 112F 
					), 
					new( 
						112F, 
						112F 
					), 
					new( 
						112F,
						112F 
					), 
					back: new (
						97F, 
						97F
					) 
				), 
				rotateSpeeds: new( // TODO: Fill in all these goddamn values! -- have fun :)
				)),
			new(Skyrim.MovementType.NPC_Swimming_MT,				
				moveSpeeds: new (
					new ( 
						walk: 110F, 
						run: 305F
					),
					new(
					
					),
					new(
				
					), 
					back: new (
				
					)
				), 
				rotateSpeeds: new(
					
				)),
			new(Skyrim.MovementType.NPC_1HM_MT,					
				moveSpeeds: new (
					new (
					
					), 
					new(
					
					), 
					new(
					
					), 
					back: new (
					
					)
				),
				rotateSpeeds: new(
				
				)),
			new(Skyrim.MovementType.NPC_2HM_MT,		
				moveSpeeds: new (
					new (
					
					), 
					new(
					
					),
					new(
					
					), 
					back: new (
					
					)
				),
				rotateSpeeds: new(
				
				)),
			new(Skyrim.MovementType.NPC_Bow_MT,		
				moveSpeeds: new (
					new (
					
					),
					new(
					
					),
					new(
					
					),
					back: new (
					
					)
				), 
				rotateSpeeds: new(
				
				)),
			new(Skyrim.MovementType.NPC_MagicCasting_MT,		
				moveSpeeds: new (
					new (
				), 
					new(
				), 
					new(
				), 
					back: new (
				)
				), 
				rotateSpeeds: new(
				)),
			new(Skyrim.MovementType.NPC_Attacking_MT,				
				moveSpeeds: new (
					new (
				), 
					new(
				), 
					new(
				), 
					back: new (
				)
				), 
				rotateSpeeds: new(
				)),
			new(Skyrim.MovementType.NPC_PowerAttacking_MT,			
				moveSpeeds: new (
					new (
				), 
					new(
				), 
					new(
				), 
					back: new (
				)
				), 
				rotateSpeeds: new(
				)),
			new(Skyrim.MovementType.NPC_Attacking2H_MT,			
				moveSpeeds: new (
					new (
				),
					new(
				), 
					new(
				), 
					back: new (
				)
				),
				rotateSpeeds: new(
				)),
			new(Skyrim.MovementType.NPC_Blocking_ShieldCharge_MT,	
				moveSpeeds: new (
					new (
				),
					new(
				), 
					new(
				), 
					back: new (
				)
				), 
				rotateSpeeds: new(
				)),
			new(Skyrim.MovementType.NPC_BowDrawn_QuickShot_MT,	
				moveSpeeds: new (
					new (
				), 
					new(
				), 
					new(
				), 
					back: new (
				)
				), 
				rotateSpeeds: new(
				)),
		};
	}
}
