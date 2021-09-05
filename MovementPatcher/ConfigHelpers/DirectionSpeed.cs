using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Noggog;

namespace MovementPatcher.ConfigHelpers {
	// Container for walk/run speeds for one direction
	public class DirectionSpeed {
		// Default constructor
		public DirectionSpeed()
		{
			Walk = Constants.NullFloat;
			Run = Constants.NullFloat;
		}
		// Constructor
		public DirectionSpeed(float walk, float run)
		{
			Walk = walk;
			Run = run;
		}

		[MaintainOrder]

		[Tooltip( "Actor speed when walking." )]
		public float Walk;

		[Tooltip( "Actor speed when running." )]
		public float Run;

		public void Deconstruct(out float walk, out float run)
		{
			walk = Walk;
			run = Run;
		}

		public (float, float) GetResolvedValuePair(float currentWalk, float currentRun, out int countChanges)
		{
			countChanges = 0;
			if ( ShouldSkip() )
				return (currentWalk, currentRun);
			int countModifier;
			(currentWalk, countModifier) = ResolveValue.Float( currentWalk, Walk );
			countChanges += countModifier;
			(currentRun, countModifier) = ResolveValue.Float( currentRun, Run );
			countChanges += countModifier;
			return (currentWalk, currentRun);
		}

		public bool ShouldSkip()
		{
			return Walk.EqualsWithin( Constants.NullFloat ) && Run.EqualsWithin( Constants.NullFloat );
		}
	}
}
