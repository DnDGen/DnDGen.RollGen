using System.Globalization;

namespace DnDGen.RollGen.Expressions
{
    internal class ExpressionEvaluatorNumericProxy(ExpressionEvaluator innerEvaluator) : ExpressionEvaluator
    {
        private const NumberStyles Number = NumberStyles.Float;

        public T Evaluate<T>(string expression)
        {
            if (!double.TryParse(expression, Number, CultureInfo.InvariantCulture, out var number))
                return innerEvaluator.Evaluate<T>(expression);

            var evaluatedExpression = Utils.BooleanOrType<T>(number);
            return Utils.ChangeType<T>(evaluatedExpression);
        }

        public bool IsValid(string expression)
        {
            if (double.TryParse(expression, Number, CultureInfo.InvariantCulture, out _))
                return true;

            return innerEvaluator.IsValid(expression);
        }
    }
}
