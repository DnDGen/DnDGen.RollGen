using System;

namespace DnDGen.RollGen
{
    public static class Utils
    {
        public static T ChangeType<T>(object rawEvaluatedExpression)
        {
            if (rawEvaluatedExpression is T)
                return (T)rawEvaluatedExpression;

            return (T)Convert.ChangeType(rawEvaluatedExpression, typeof(T));
        }
    }
}
