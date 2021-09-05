using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace MovementPatcher.ConfigHelpers {
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
		[SettingName("Movement Type"), Tooltip("This corresponds with a \"MOVT - Movement Type\" record. Each section has one target movement type.")]
		public FormLink<IMovementTypeGetter> MoveTypeFormLink;
		[Tooltip("This subsection contains all of the walk/run speed values associated with this movement type.")]
		public MovementSpeed MovementSpeed;
		[Tooltip("This subsection contains the 3 rotational speed values associated with this movement type.")]
		public RotationSpeed RotationSpeed;

		// Returns true if this instance is invalid and should be skipped.
		public bool ShouldSkip()
		{
			return MoveTypeFormLink.IsNull || ( MovementSpeed.ShouldSkip() && RotationSpeed.ShouldSkip() );
		}

		// takes a MovementType, changes its values if applicable, then returns it alongside the number of changed values.
		public MovementType SetMovementTypeValues(MovementType current, out int countChanges)
		{
			countChanges = 0;
			if ( ShouldSkip() )
				return current;
			(current.LeftWalk, current.LeftRun) = MovementSpeed.Left.GetResolvedValuePair(current.LeftWalk, current.LeftRun, out var countChangesMod);
			countChanges += countChangesMod;
			(current.RightWalk, current.RightRun) = MovementSpeed.Right.GetResolvedValuePair( current.RightWalk, current.RightRun, out countChangesMod );
			countChanges += countChangesMod;
			(current.ForwardWalk, current.ForwardRun) = MovementSpeed.Forward.GetResolvedValuePair(current.ForwardWalk, current.ForwardRun, out countChangesMod );
			countChanges += countChangesMod;
			(current.BackWalk, current.BackRun) = MovementSpeed.Back.GetResolvedValuePair(current.BackWalk, current.BackRun, out countChangesMod );
			countChanges += countChangesMod;
			(current.RotateInPlaceWalk, current.RotateInPlaceRun, current.RotateWhileMovingRun) = RotationSpeed.GetResolvedValueTuple( current.RotateInPlaceWalk, current.RotateInPlaceRun, current.RotateWhileMovingRun, out countChangesMod );
			countChanges += countChangesMod;
			return current;
		}
	}
}
