using Mutagen.Bethesda.WPF.Reflection.Attributes;

namespace MovementPatcher.ConfigHelpers {
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

		[Tooltip( "Actor speeds when strafing left." )]
		public DirectionSpeed Left;

		[Tooltip( "Actor speeds when strafing right." )]
		public DirectionSpeed Right;

		[Tooltip( "Actor speeds when walking forward." )]
		public DirectionSpeed Forward;

		[Tooltip( "Actor speeds when walking backward." )]
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
}
