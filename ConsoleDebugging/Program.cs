using AreaCalculatorNS;

IShape shape1 = new Circle(5);
IShape shape2 = new Triangle(5, 4, 2);

Console.WriteLine("IShape: Площадь круга: " + shape1.CalculateArea());
Console.WriteLine("IShape: Площадь треугольника: " + shape2.CalculateArea());

Console.WriteLine("AreaCalculator: Площадь круга: " + AreaCalculator.Circle(5));
Console.WriteLine("AreaCalculator: Площадь треугольника: " + AreaCalculator.Triangle(5, 4, 2));