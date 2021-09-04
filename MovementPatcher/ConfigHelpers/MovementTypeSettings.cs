using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;
using Noggog;

namespace MovementPatcher.ConfigHelpers {
	public struct ResolveValue {
		public static float Float(float current, float setting, out bool modified)
		{
			modified = !setting.EqualsWithin( current ) && !setting.EqualsWithin(Constants.NullFloat);
			return modified ? setting : current; // return setting val if valid, else return current
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

		public (float, float) GetResolvedValuePair(float currentWalk, float currentRun, out bool modified)
		{
			modified = false;
			if ( ShouldSkip() )
				return (currentWalk, currentRun);
			currentWalk = ResolveValue.Float(currentWalk, Walk, out var changedWalk);
			currentRun = ResolveValue.Float(currentRun, Run, out var changedRun);
			modified = changedWalk || changedRun;
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

		public (float, float, float) GetResolvedValueTuple(float currentInPlaceWalk, float currentInPlaceRun, float currentRunning, out bool modified)
		{
			modified = false;
			if ( ShouldSkip() )
				return (currentInPlaceWalk, currentInPlaceRun, currentRunning);
			currentInPlaceWalk = ResolveValue.Float(currentInPlaceWalk, InPlace.Walk, out var changedInPlaceWalk);
			currentInPlaceRun = ResolveValue.Float(currentInPlaceRun, InPlace.Run, out var changedInPlaceRun);
			currentRunning = ResolveValue.Float( currentRunning, Running, out var changedRunning );
			modified = changedInPlaceWalk || changedInPlaceRun || changedRunning;
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
			Type = new();
			Type.SetToNull();
			MovementSpeed = new();
			RotationSpeed = new();
		}
		public MovementTypeSettings(FormLink<IMovementTypeGetter> type, MovementSpeed moveSpeeds, RotationSpeed rotateSpeeds)
		{
			Type = type;
			MovementSpeed = moveSpeeds;
			RotationSpeed = rotateSpeeds;
		}

		[MaintainOrder]

		public FormLink<IMovementTypeGetter> Type;

		public MovementSpeed MovementSpeed;

		public RotationSpeed RotationSpeed;

		public bool ShouldSkip()
		{
			return Type.IsNull || ( MovementSpeed.ShouldSkip() && RotationSpeed.ShouldSkip() );
		}

		public MovementType SetMovementTypeValues(MovementType current, out bool modified)
		{
			modified = false;
			if ( ShouldSkip() )
				return current;
			(current.LeftWalk, current.LeftRun) = MovementSpeed.Left.GetResolvedValuePair(current.LeftWalk, current.LeftRun, out bool changedLeft );
			(current.RightWalk, current.RightRun) = MovementSpeed.Left.GetResolvedValuePair(current.RightWalk, current.RightRun, out bool changedRight );
			(current.ForwardWalk, current.ForwardRun) = MovementSpeed.Left.GetResolvedValuePair(current.ForwardWalk, current.ForwardRun, out bool changedForward );
			(current.BackWalk, current.BackRun) = MovementSpeed.Left.GetResolvedValuePair(current.BackWalk, current.BackRun, out bool changedBack );
			(current.RotateInPlaceWalk, current.RotateInPlaceRun, current.RotateWhileMovingRun) = RotationSpeed.GetResolvedValueTuple( current.RotateInPlaceWalk, current.RotateInPlaceRun, current.RotateWhileMovingRun, out var changedRotation);
			modified = changedLeft || changedRight || changedForward || changedBack || changedRotation;
			return current;
		}
	}
}
