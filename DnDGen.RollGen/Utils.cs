using System;

namespace DnDGen.RollGen
{
    internal static class Utils
    {
        public static T ChangeType<T>(object rawEvaluatedExpression)
        {
            if (rawEvaluatedExpression is T t)
                return t;

            return (T)Convert.ChangeType(rawEvaluatedExpression, typeof(T));
        }
    }
}
