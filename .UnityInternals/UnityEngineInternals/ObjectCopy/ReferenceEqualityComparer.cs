namespace SolidUtilities.UnityEngineInternals
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class ReferenceEqualityComparer : EqualityComparer<object>
    {
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }

        public override int GetHashCode(object obj)
        {
            // The RuntimeHelpers.GetHashCode method always calls the Object.GetHashCode method non-virtually,
            // even if the object's type has overridden the Object.GetHashCode method.
            return RuntimeHelpers.GetHashCode(obj);
        }
    }
}