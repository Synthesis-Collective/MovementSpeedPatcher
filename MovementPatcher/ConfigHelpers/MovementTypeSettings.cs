using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Noggog;

namespace MovementPatcher.ConfigHelpers {
	public struct ResolveValue {
		public static (float, int) Float(float current, float setting)
		{
			return !setting.EqualsWithin( current ) && !setting.EqualsWithin( Constants.NullFloat ) 
				? (setting, 1)	// true; return setting value & 1
				: (current, 0); // false; return current value & 0
		}
	}
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

		[Tooltip("Actor speed when walking.")]
		public float Walk;

		[Tooltip("Actor speed when running.")]
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
			(currentWalk, countModifier) = ResolveValue.Float(currentWalk, Walk);
			countChanges += countModifier;
			(currentRun, countModifier) = ResolveValue.Float(currentRun, Run);
			countChanges += countModifier;
			return (currentWalk, currentRun);
		}

		public bool ShouldSkip()
		{
			return Walk.EqualsWithin(Constants.NullFloat) && Run.EqualsWithin(Constants.NullFloat);
		}
	}

	// Container for walk/run speeds for all 4 directions
	public class MovementSpeed {
		// Default constructor
		public MovementSpeed()
		{
			Left = new();
			Right = new();
			Forward = new();
			Back = new();
		}
		// Constructor
		public MovementSpeed(DirectionSpeed left, DirectionSpeed right, DirectionSpeed forward, DirectionSpeed back)
		{
			Left = left;
			Right = right;
			Forward = forward;
			Back = back;
		}

		[MaintainOrder]

		[Tooltip("Actor speeds when strafing left.")]
		public DirectionSpeed Left;

		[Tooltip("Actor speeds when strafing right.")]
		public DirectionSpeed Right;

		[Tooltip("Actor speeds when walking forward.")]
		public DirectionSpeed Forward;

		[Tooltip("Actor speeds when walking backward.")]
		public DirectionSpeed Back;

		public void Deconstruct(out DirectionSpeed left, out DirectionSpeed right, out DirectionSpeed forward, out DirectionSpeed back)
		{
			left = Left;
			right = Right;
			forward = Forward;
			back = Back;
		}

		public bool ShouldSkip()
		{
			return Left.ShouldSkip() && Right.ShouldSkip() && Forward.ShouldSkip() && Back.ShouldSkip();
		}
	}

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
		public RotationSpeed(float rotateInPlaceWalk, float rotateInPlaceRun, float rotateRunning)
		{
			InPlace = new( rotateInPlaceWalk, rotateInPlaceRun );
			Running = rotateRunning;
		}

		[MaintainOrder]

		[Tooltip("Actor speeds when turning on the spot.")]
		public DirectionSpeed InPlace;

		[Tooltip( "Actor turning speed while sprinting." )]
		public float Running;

		public void Deconstruct(out DirectionSpeed inPlace, out float running)
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
			(currentInPlaceWalk, countModifier) = ResolveValue.Float(currentInPlaceWalk, InPlace.Walk);
			countChanges += countModifier;
			(currentInPlaceRun, countModifier) = ResolveValue.Float(currentInPlaceRun, InPlace.Run);
			countChanges += countModifier;
			(currentRunning, countModifier) = ResolveValue.Float( currentRunning, Running);
			countChanges += countModifier;
			return (currentInPlaceWalk, currentInPlaceRun, currentRunning);
		}

		public bool ShouldSkip()
		{
			return InPlace.ShouldSkip() && Running.EqualsWithin(Constants.NullFloat);
		}
	}

	// Container of settings for an entire MovementType, and methods to apply it.
	public class MovementTypeSettings {
		public MovementTypeSettings()
		{
			MoveTypeFormLink = new();
			MoveTypeFormLink.SetToNull();
			MovementSpeed = new();
			RotationSpeed = new();
		}
		public MovementTypeSettings(FormLink<IMovementTypeGetter> movementType, MovementSpeed moveSpeeds, RotationSpeed rotateSpeeds)
		{
			MoveTypeFormLink = movementType;
			MovementSpeed = moveSpeeds;
			RotationSpeed = rotateSpeeds;
		}

		[MaintainOrder]

		public FormLink<IMovementTypeGetter> MoveTypeFormLink;

		public MovementSpeed MovementSpeed;

		public RotationSpeed RotationSpeed;

		public bool ShouldSkip()
		{
			return MoveTypeFormLink.IsNull || ( MovementSpeed.ShouldSkip() && RotationSpeed.ShouldSkip() );
		}

		public MovementType SetMovementTypeValues(MovementType current, out int countChanges)
		{
			countChanges = 0;
			if ( ShouldSkip() )
				return current;
			(current.LeftWalk, current.LeftRun) = MovementSpeed.Left.GetResolvedValuePair(current.LeftWalk, current.LeftRun, out var countChangesMod);
			countChanges += countChangesMod;
			( current.RightWalk, current.RightRun) = MovementSpeed.Left.GetResolvedValuePair(current.RightWalk, current.RightRun, out countChangesMod );
			countChanges += countChangesMod;
			(current.ForwardWalk, current.ForwardRun) = MovementSpeed.Left.GetResolvedValuePair(current.ForwardWalk, current.ForwardRun, out countChangesMod );
			countChanges += countChangesMod;
			(current.BackWalk, current.BackRun) = MovementSpeed.Left.GetResolvedValuePair(current.BackWalk, current.BackRun, out countChangesMod );
			countChanges += countChangesMod;
			(current.RotateInPlaceWalk, current.RotateInPlaceRun, current.RotateWhileMovingRun) = RotationSpeed.GetResolvedValueTuple( current.RotateInPlaceWalk, current.RotateInPlaceRun, current.RotateWhileMovingRun, out countChangesMod );
			countChanges += countChangesMod;
			return current;
		}
	}
}
