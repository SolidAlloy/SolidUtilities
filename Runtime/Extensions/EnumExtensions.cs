namespace SolidUtilities
{
    using System;
    using JetBrains.Annotations;

#if ! CSHARP_7_3_OR_NEWER
    using Unity.Collections.LowLevel.Unsafe;
#endif

    public static class EnumExtensions
    {
#if CSHARP_7_3_OR_NEWER
        public static bool DoesNotContainFlag<TEnum>(this TEnum thisEnum, TEnum flag) where TEnum : unmanaged, Enum
        {
            unsafe
            {
                switch (sizeof(TEnum))
                {
                    case 1:
                        return (*(byte*)&thisEnum | ~*(byte*)&flag) != byte.MaxValue;
                    case 2:
                        return (*(ushort*)&thisEnum | ~*(ushort*)&flag) != ushort.MaxValue;
                    case 4:
                        return (*(uint*)&thisEnum | ~*(uint*)&flag) != uint.MaxValue;
                    case 8:
                        return (*(ulong*)&thisEnum | ~*(ulong*)&flag) != ulong.MaxValue;
                    default:
                        throw new Exception("Size does not match a known Enum backing type.");
                }
            }
        }
#endif

        /// <summary>
        /// Allocation-less version of <see cref="Enum.HasFlag"/> that still runs very fast.
        /// </summary>
        /// <param name="thisEnum">The enum to check for flags.</param>
        /// <param name="flag">The flag to search for in <paramref name="thisEnum"/>.</param>
        /// <typeparam name="TEnum">The type of enum.</typeparam>
        /// <returns>Whether <paramref name="thisEnum"/> contains the specified <paramref name="flag"/>.</returns>
        /// <exception cref="Exception">If it was not possible to determine the underlying type of the enum.</exception>
        /// <remarks>
        /// Taken from here https://forum.unity.com/threads/c-hasaflag-method-extension-how-to-not-create-garbage-allocation.616924/#post-4420699
        /// </remarks>
        [PublicAPI]
        public static bool ContainsFlag<TEnum>(this TEnum thisEnum, TEnum flag) where TEnum :
#if CSHARP_7_3_OR_NEWER
            unmanaged, Enum
#else
            struct
#endif
        {
            unsafe
            {
#if CSHARP_7_3_OR_NEWER
                switch (sizeof(TEnum))
                {
                    case 1:
                        return (*(byte*)&thisEnum & *(byte*)&flag) > 0;
                    case 2:
                        return (*(ushort*)&thisEnum & *(ushort*)&flag) > 0;
                    case 4:
                        return (*(uint*)&thisEnum & *(uint*)&flag) > 0;
                    case 8:
                        return (*(ulong*)&thisEnum & *(ulong*)&flag) > 0;
                    default:
                        throw new Exception("Size does not match a known Enum backing type.");
                }
#else
                switch (UnsafeUtility.SizeOf<TEnum>())
                {
                    case 1:
                        {
                            byte valLhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref thisEnum, &valLhs);
                            byte valRhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref flag, &valRhs);
                            return (valLhs & valRhs) > 0;
                        }
                    case 2:
                        {
                            ushort valLhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref thisEnum, &valLhs);
                            ushort valRhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref flag, &valRhs);
                            return (valLhs & valRhs) > 0;
                        }
                    case 4:
                        {
                            uint valLhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref thisEnum, &valLhs);
                            uint valRhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref flag, &valRhs);
                            return (valLhs & valRhs) > 0;
                        }
                    case 8:
                        {
                            ulong valLhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref thisEnum, &valLhs);
                            ulong valRhs = 0;
                            UnsafeUtility.CopyStructureToPtr(ref flag, &valRhs);
                            return (valLhs & valRhs) > 0;
                        }
                    default:
                        throw new Exception("Size does not match a known Enum backing type.");
                }
#endif
            }
        }
    }
}