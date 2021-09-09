using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Noggog;

namespace MovementPatcher.ConfigHelpers {
	// Container for rotation speed settings
	public class RotationSpeed {
		// Default constructor
		public RotationSpeed()
		{
			InPlace = new();
			Running = Constants.NullFloat;
		}
		// Constructor
		public RotationSpeed(DirectionSpeed rotateInPlace, float rotateRunning)
		{
			InPlace = rotateInPlace;
			Running = rotateRunning;
		}
		// Constructor
		public RotationSpeed(float rotateInPlaceWalk, float rotateInPlaceRun, float rotateRunning = Constants.NullFloat)
		{
			InPlace = new( rotateInPlaceWalk, rotateInPlaceRun );
			Running = rotateRunning;
		}

		[MaintainOrder]

		[Tooltip( "Actor speeds when turning on the spot." )]
		public DirectionSpeed InPlace;

		[Tooltip( "Actor turning speed while sprinting." )]
		public float Running;

		public void Deconstruct(out DirectionSpeed inPlace, out float? running)
		{
			inPlace = InPlace;
			running = Running;
		}

		public (float, float, float) GetResolvedValueTuple(float currentInPlaceWalk, float currentInPlaceRun, float currentRunning, out int countChanges)
		{
			countChanges = 0;
			if ( ShouldSkip() )
				return (currentInPlaceWalk, currentInPlaceRun, currentRunning);
			int countModifier;
			(currentInPlaceWalk, countModifier) = ResolveValue.Float( currentInPlaceWalk, InPlace.Walk );
			countChanges += countModifier;
			(currentInPlaceRun, countModifier) = ResolveValue.Float( currentInPlaceRun, InPlace.Run );
			countChanges += countModifier;
			(currentRunning, countModifier) = ResolveValue.Float( currentRunning, Running );
			countChanges += countModifier;
			return (currentInPlaceWalk, currentInPlaceRun, currentRunning);
		}

		public bool ShouldSkip()
		{
			return InPlace.ShouldSkip() && Running.EqualsWithin( Constants.NullFloat );
		}
	}
}
