using System;

namespace ByndyusoftCalculator.Core {
    public class Calculator {
        public double Result { get; }
        public string ErrorMessage { get; }
        public bool IsSuccess { get; }

        public Calculator(string errorMessage) {
            IsSuccess = false;
            Result = double.NaN;
            ErrorMessage = errorMessage;
        }

        public Calculator(double result) {
            IsSuccess = true;
            Result = result;
            ErrorMessage = null;
        }
    }
}
