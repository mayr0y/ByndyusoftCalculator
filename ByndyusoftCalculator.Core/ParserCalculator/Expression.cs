using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace ByndyusoftCalculator.Core.ParserCalculator {
    public static class Expression {
        public static Calculator Parse(string value) {
            value = Parentheses.TrimParentheses(value);
            var topLevelParentheses = Parentheses.GetTopLevelParentheses(value, true);

            if (Regex.IsMatch(value, @"^-?(\d+|\d+\.\d+)$")) {
                return double.TryParse(value, out double parseResult) ? new Calculator(parseResult) :
                    new Calculator($"Нечитаемое выражение \"{value}\"");
            } else {
                foreach(var operation in Operation) {
                    for(var valueIndex = value.Length - 1; valueIndex >=0; valueIndex--) {
                        if (value[valueIndex] == '-' && (valueIndex == 0 || value[valueIndex - 1].IsDigit() == false)) //проверка на отрицательное число
                            continue;

                        if (operation.Value.Contains(value[valueIndex])) {
                            var firstExpression = Parse(value[..valueIndex]);
                            var secondExpression = Parse(value[(valueIndex + 1)..]);

                            if (firstExpression.IsSuccess == false && secondExpression.IsSuccess == false)
                                return new Calculator($"{firstExpression.ErrorMessage}{Environment.NewLine}{secondExpression.ErrorMessage}");
                            else if (firstExpression.IsSuccess == false)
                                return new Calculator(firstExpression.ErrorMessage);
                            else if (secondExpression.IsSuccess == false)
                                return new Calculator(secondExpression.ErrorMessage);

                            try {
                                var resultOperation = ResultOperation[value[valueIndex]](firstExpression.Result, secondExpression.Result);
                                return double.IsInfinity(resultOperation)
                                    ? new Calculator("Результат вышел за пределы чисел или было деление на ноль")
                                    : new Calculator(resultOperation);
                            }
                            catch (DivideByZeroException e) {
                                return new Calculator(e.Message);
                            }
                            catch (Exception e) {
                                return new Calculator(e.Message);
                            }
                        }

                        if (value[valueIndex] == ')') {
                            valueIndex = topLevelParentheses[valueIndex];
                        }
                    }
                }
            }

            return new Calculator($"Нечитаемое выражение \"{value}\"");
        }

        private static readonly Dictionary<int, char[]> Operation = new Dictionary<int, char[]>{
            {1, new[]{'+', '-'}},
            {2, new[]{'*', '/'}}
        };

        private static readonly Dictionary<char, Func<double, double, double>> ResultOperation = new Dictionary<char, Func<double, double, double>> {
            { '+', (d1, d2) => d1 + d2 },
            { '-', (d1, d2) => d1 - d2 },
            { '*', (d1, d2) => d1 * d2 },
            { '/', (d1, d2) => d1 / d2 },
        };

    }
}
