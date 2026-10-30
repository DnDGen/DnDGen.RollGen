using Albatross.Expression;
using Albatross.Expression.Parsing;
using System;
using System.Text.RegularExpressions;

namespace DnDGen.RollGen.Expressions
{
    internal partial class AlbatrossExpressionEvaluator(IParser parser) : ExpressionEvaluator
    {
        private readonly Regex strictRollRegex = strictRollPartialRegex();

        public T Evaluate<T>(string expression)
        {
            var match = strictRollRegex.Match(expression);
            if (match.Success)
                throw new ArgumentException($"Cannot evaluate unrolled die roll {match.Value}");

            try
            {
                var exp = parser.Build(expression);
                var evaluation = exp.Eval(null);

                //INFO: Booleans convert through their string, so a boolean expression is not silently read as 1 or 0
                if (evaluation is bool)
                    evaluation = evaluation.ToString();

                return Utils.ChangeType<T>(evaluation);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Expression '{expression}' is invalid", e);
            }
        }

        public bool IsValid(string expression)
        {
            //HACK: The "IsValidExpression" method doesn't always handle this correctly and return TRUE when it shouldn't.
            //So, have to use ugly error catching instead.
            //return parser.IsValidExpression(expression);

            var match = strictRollRegex.Match(expression);
            if (match.Success)
                return false;

            try
            {
                var unevaluatedMatch = parser.Build(expression).Eval(null);
                return unevaluatedMatch != null;
            }
            catch
            {
                return false;
            }
        }

        [GeneratedRegex(RegexConstants.StrictRollPattern)]
        private static partial Regex strictRollPartialRegex();
    }
}
