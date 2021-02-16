using System;
using System.Text;
using ByndyusoftCalculator.Core;
using ByndyusoftCalculator.Core.ParserCalculator;

namespace ByndyusoftCalculator {
    class Program {
        static void Main(string[] args) {
            Console.OutputEncoding = Encoding.UTF8;
            ICalculator calculator = new ParserCalculator();

            while (true) {
                Console.WriteLine();
                Console.Write("Введите выражение: ");

                var value = Console.ReadLine();
                var result = calculator.Calculator(value);
                Console.WriteLine(result.IsSuccess ? $" = {result.Result}" : $"Ошибка: {result.ErrorMessage}");
            }
        }
    }
}
