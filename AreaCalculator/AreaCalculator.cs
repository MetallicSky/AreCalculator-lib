using System.Drawing;

namespace AreaCalculatorNS
{
    public interface IShape
    {
        double CalculateArea();
    }

    public class Circle : IShape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }
        public double CalculateArea()
        {
            return AreaCalculator.Circle(Radius);
        }
    }

    public class Triangle : IShape
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }

        public Triangle(double sideA, double sideB, double sideC)
        {
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }

        public double CalculateArea()
        {
            return AreaCalculator.Triangle(SideA, SideB, SideC);
        }
    }

    public static class AreaCalculator
    {
        // Проверка обобщенного типа на численность
        /*private static void NumericCheck(in object o, in string paramName)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return;
                default:
                    {
                        Console.Error.WriteLine("Parameter is not a number");
                        throw new ArgumentException("Parameter is not a number", paramName);
                    }
            }
        }*/

        // Для круга и треугольника (и любых других потенциальных фигур) значения меньшие или равные 0 маловероятны, поэтому имеет смысл сделать одну проверку общую для всех фигур
        private static void MoreThan0Check(in double param, in string paramName)
        {
            if (param <= 0)
            {
                Console.Error.WriteLine("Parameter is less or equals 0 (" + param + ")");
                throw new ArgumentOutOfRangeException("Parameter is less or equals 0", param, paramName);
            }
        }

        // Вычисление площади круга
        public static double Circle(in double radius)
        {
            MoreThan0Check(radius, nameof(radius));
            return Math.Pow(radius, 2) * Math.PI;
        }

        //Вычисление площади треугольника
        public static double Triangle(in double sideA, in double sideB, in double sideC)
        {
            double[] sides = [sideA, sideB, sideC]; // Массив, содержащий входные данные, для последющего использования в циклах 
            string[] sidesNames = [nameof(sideA), nameof(sideB), nameof(sideC)]; // массив, содержащий названия переменных входных данных, для исключений

            for (int i = 0; i < sides.Length; i++)
            {
                MoreThan0Check(sides[i], sidesNames[i]);
            }

            for (int i = 0; i < sides.Length; i++) // Использование sides.Length здесь позволяет компилятору не проверять нахождения указателя в пределах границы массивы и ненамного увеличивает производительность
            {
                if (sides[(i + 1) % 3] + sides[(i + 2) % 3] < sides[i % 3]) // Такая форма записи сравнивает все 3 стороны с 2 остальными, проверяя их валидность (одна сторона не может быть больше двух других)
                {
                    Console.Error.WriteLine("{0} is greater than {1} + {2} ({3} > {4} + {5})", sidesNames[i % 3], sidesNames[(i + 1) % 3], sidesNames[(i + 2) % 3], sides[i % 3],  sides[(i + 1) % 3], sides[(i + 2) % 3]);

                    //Console.Error.WriteLine(sidesNames[i % 3] + " is greater than " + sidesNames[(i + 1) % 3] + " + " + sidesNames[(i + 2) % 3] + " (" + sides[i % 3] + " > " + sides[(i + 1) % 3] + " + " + sides[(i + 2) % 3] + ")"); // Альтернативная форма записи, на которую можно переключиться при желании
                    throw new ArgumentOutOfRangeException(sidesNames[i % 3] + " is greater than " + sidesNames[(i + 1) % 3] + " + " + sidesNames[(i + 2) % 3], sides[i % 3], sidesNames[i % 3]);
                }
            }

            // Альтернативная форма записи, без использования цикла
            /*
            if (sideA + sideB < sideC)
            {
                Console.Error.WriteLine("Side C is greater than Side A + Side B (" + sideC + " > " + sideA + " + " + sideB + ")");
                throw new ArgumentOutOfRangeException("Side C is greater than Side A + Side B", sideC, nameof(sideC));
            }
            if (sideA + sideC < sideB)
            {
                Console.Error.WriteLine("Side B is greater than Side A + Side C (" + sideB + " > " + sideA + " + " + sideC + ")");
                throw new ArgumentOutOfRangeException("Side B is greater than Side A + Side C", sideB, nameof(sideB));
            }
            if (sideB + sideC < sideA)
            {
                Console.Error.WriteLine("Side A is greater than Side B + Side C (" + sideA + " > " + sideB + " + " + sideC + ")");
                throw new ArgumentOutOfRangeException("Side A is greater than Side B + Side C", sideA, nameof(sideA));
            }
            */

            // Проверка на прямоугольность треугольника с целью потенциальной оптимизации процесса поиска площади
            for (int i = 0; i < sides.Length; i++)
            {
                if (Math.Pow(sides[(i + 1) % 3], 2) + Math.Pow(sides[(i + 2) % 3], 2) == Math.Pow(sides[i % 3], 2))
                {
                    return sides[(i + 1) % 3] * sides[(i + 2) % 3] / 2;
                }
            }
            double SemiPerimeter = sides.Sum() / 2;
            return Math.Sqrt(SemiPerimeter * (SemiPerimeter - sides[0]) * (SemiPerimeter - sides[1]) * (SemiPerimeter - sides[2]));
        }
    }
}
