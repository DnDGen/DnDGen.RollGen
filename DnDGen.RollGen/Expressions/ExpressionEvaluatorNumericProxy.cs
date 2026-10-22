using System.Globalization;

namespace DnDGen.RollGen.Expressions
{
    internal class ExpressionEvaluatorNumericProxy(ExpressionEvaluator innerEvaluator) : ExpressionEvaluator
    {
        public T Evaluate<T>(string expression)
        {
            if (!double.TryParse(expression, NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
                return innerEvaluator.Evaluate<T>(expression);

            return Utils.ChangeType<T>(number);
        }

        public bool IsValid(string expression)
        {
            if (double.TryParse(expression, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                return true;

            return innerEvaluator.IsValid(expression);
        }
    }
}
