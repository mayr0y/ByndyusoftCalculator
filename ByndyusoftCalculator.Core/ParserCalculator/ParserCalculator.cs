using System;
using System.Text.RegularExpressions;

namespace ByndyusoftCalculator.Core.ParserCalculator {
    public class ParserCalculator : ICalculator {
        private static (bool isValid, string errorMes) Validate(string value) {
            if (value == null || Regex.IsMatch(value, "^([-+/*]\\d+(\\.\\d+)?)*") == false || Regex.IsMatch(value, "\\d\\s+\\d")) {
                return (false, $"Нечитаемое выражение \"{value}\"");
            }
            if (Parentheses.ValidateParentheses(value) == false) {
                return (false, "Разное количество скобок");
            }
            return (true, null);
        }

        public Calculator Calculator(string value) {
            var validationResult = Validate(value);

            if(validationResult.isValid == false) {
                return new Calculator(validationResult.errorMes);
            }
            value = Regex.Replace(value, @"\s+", string.Empty);
            return Expression.Parse(value);
        }
    }
}
