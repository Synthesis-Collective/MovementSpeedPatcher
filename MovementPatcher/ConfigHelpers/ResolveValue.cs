using Noggog;

namespace MovementPatcher.ConfigHelpers {
	public struct ResolveValue {
		public static (float, int) Float(float current, float setting)
		{
			if ( !setting.EqualsWithin( current ) && !( setting.EqualsWithin( Constants.NullFloat ) && setting < 0 ) )
				return (setting, 1);  // true; return setting value & 1
			return (current, 0); // false; return current value & 0
		}
	}
}
