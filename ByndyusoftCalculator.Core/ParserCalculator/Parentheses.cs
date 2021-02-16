using System;
using System.Collections.Generic;

namespace ByndyusoftCalculator.Core.ParserCalculator {
    public static class Parentheses {
        public static Dictionary<int, int> GetTopLevelParentheses(string input, bool reversed = false) {
            var returnDictionary = new Dictionary<int, int>();

            var startIndex = 0;
            var parenthesesLevel = 0;

            for (var inputIndex = 0; inputIndex < (input?.Length ?? 0); inputIndex++) {
                if (input[inputIndex] == '(') {
                    if (parenthesesLevel == 0) {
                        startIndex = inputIndex;
                    }

                    parenthesesLevel++;
                }
                else if (input[inputIndex] == ')') {
                    if (parenthesesLevel == 1) {
                        if (reversed)
                            returnDictionary.Add(inputIndex, startIndex);
                        else
                            returnDictionary.Add(startIndex, inputIndex);
                    }

                    parenthesesLevel--;
                }
            }

            return returnDictionary;
        }

        public static string TrimParentheses(string input) {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var startIndex = 0;
            var trimmed = false;
            while (input[startIndex] == '(' && input[^(startIndex + 1)] == ')') {
                if (ValidateParentheses(input, startIndex)) {
                    startIndex++;
                    trimmed = true;
                }
                else
                    break;
            }

            if (trimmed && ValidateParentheses(input, startIndex) == false)
                startIndex--;

            return input[startIndex..^startIndex];
        }

        public static bool ValidateParentheses(string input) => ValidateParentheses(input, 0);

        public static bool ValidateParentheses(string input, int offset) {
            if (string.IsNullOrWhiteSpace(input))
                return true;

            var parenthesesLevel = 0;

            for (var inputCharIndex = offset; inputCharIndex < input.Length - offset; inputCharIndex++) {
                switch (input[inputCharIndex]) {
                    case '(':
                        parenthesesLevel++;
                        break;
                    case ')' when parenthesesLevel > 0:
                        parenthesesLevel--;
                        break;
                }
            }

            return parenthesesLevel == 0;
        }

        private static readonly HashSet<char> Digits = new HashSet<char>()
        { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
        public static bool IsDigit(this char c) => Digits.Contains(c);
    }
}
