using System;
using System.Collections.Generic;

namespace DnDGen.RollGen.Expressions
{
    internal class ExpressionEvaluatorCachingProxy(ExpressionEvaluator innerEvaluator) : ExpressionEvaluator
    {
        private readonly Dictionary<(string Expression, Type Type), object> cachedEvaluations = [];
        private readonly Dictionary<string, bool> cachedValidations = [];
        private readonly object myLock = new();

        public T Evaluate<T>(string expression)
        {
            var key = (expression, typeof(T));

            lock (myLock)
            {
                if (!cachedEvaluations.ContainsKey(key))
                {
                    var evaluation = innerEvaluator.Evaluate<T>(expression);
                    cachedEvaluations.Add(key, evaluation);
                }

                return (T)cachedEvaluations[key];
            }
        }

        public bool IsValid(string expression)
        {
            lock (myLock)
            {
                if (!cachedValidations.ContainsKey(expression))
                {
                    var valid = innerEvaluator.IsValid(expression);
                    cachedValidations.Add(expression, valid);
                }

                return cachedValidations[expression];
            }
        }
    }
}
