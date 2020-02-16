using UnityEngine;

namespace TVDM
{
    namespace ExtendedFunctions
    {

        public static class Math
        {
            public static T Remap<T>(T value, T from1, T to1, T from2, T to2)
            {
                return ((dynamic)value - (dynamic)from1) / ((dynamic)to1 - (dynamic)from1) * ((dynamic)to2 - (dynamic)from2) + (dynamic)from2;
            }

            public static T Normalize<T>(T value, T from1, T to1)
            {
                return Remap(value, from1, to1, (dynamic)0, (dynamic)1);
            }
        }
    }
}
