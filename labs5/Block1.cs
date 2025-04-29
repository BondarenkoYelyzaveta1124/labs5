using System;

using MyFrac = (long nom, long denom);

class Block1
{
    public static void Block11()
    {
        Console.WriteLine("Enter two numbers for first fraction (numerator and denominator):");
        long numb11 = long.Parse(Console.ReadLine());
        long numb12 = long.Parse(Console.ReadLine());

        Console.WriteLine("Enter two numbers for second fraction (numerator and denominator):");
        long numb21 = long.Parse(Console.ReadLine());
        long numb22 = long.Parse(Console.ReadLine());

        MyFrac f1 = (numb11, numb12);
        MyFrac f2 = (numb21, numb22);

        Console.WriteLine("f1: " + MyFracToString(f1));
        Console.WriteLine("f2: " + MyFracToString(f2));
        Console.WriteLine("\nNormalized f1: " + MyFracToString(Normalize(f1)));
        Console.WriteLine("Normalized f2: " + MyFracToString(Normalize(f2)));
        Console.WriteLine("\nTo string with int part f1: " + ToStringWithIntPart(f1));
        Console.WriteLine("To string with int part f2: " + ToStringWithIntPart(f2));
        Console.WriteLine("\nDouble value of f1: " + DoubleValue(f1));
        Console.WriteLine("Double value of f2: " + DoubleValue(f2));
        Console.WriteLine("\nSum (normalized): " + MyFracToString(Plus(f1, f2)));
        Console.WriteLine("\nDifference (normalized): " + MyFracToString(Minus(f1, f2)));
        Console.WriteLine("\nMultiplication (normalized): " + MyFracToString(Multiply(f1, f2)));
        Console.WriteLine("\nDivision (normalized): " + MyFracToString(Divide(f1, f2)));
        Console.WriteLine("\nExpression 1 (1 + 1/2 + 1/3 + ... + 1/n): " + MyFracToString(CalcExpr1(5)));
        Console.WriteLine("\nExpression 2 (1 / (1 + 1 / (2 + 1 / (...)))): " + MyFracToString(CalcExpr2(5)));
    }
    static string MyFracToString(MyFrac f)
    {
        return $"{f.nom} / {f.denom}";
    }
    static MyFrac Normalize(MyFrac t)
    {
        long gcd = GCD(Math.Abs(t.nom), Math.Abs(t.denom));
        long nom = t.nom / gcd;
        long denom = t.denom / gcd;
        if (denom < 0)
        {
            nom = -nom;
            denom = -denom;
        }
        return (nom, denom);
    }
    static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    static string ToStringWithIntPart(MyFrac f)
    {
        MyFrac norm = Normalize(f);
        long whole = norm.nom / norm.denom;
        long remainder = Math.Abs(norm.nom % norm.denom);
        if (norm.denom == 1) return $"({whole})";
        if (whole == 0) return $"({norm.nom} / {norm.denom})";
        if (norm.nom < 0)
            return $"-({Math.Abs(whole)} + {remainder} / {norm.denom})";
        else
            return $"({whole} + {remainder} / {norm.denom})";
    }
    static double DoubleValue(MyFrac f)
    {
        return (double)f.nom / f.denom;
    }
    static MyFrac Plus(MyFrac f1, MyFrac f2)
    {
        return Normalize((f1.nom * f2.denom + f2.nom * f1.denom, f1.denom * f2.denom));
    }
    static MyFrac Minus(MyFrac f1, MyFrac f2)
    {
        return Normalize((f1.nom * f2.denom - f2.nom * f1.denom, f1.denom * f2.denom));
    }
    static MyFrac Multiply(MyFrac f1, MyFrac f2)
    {
        return Normalize((f1.nom * f2.nom, f1.denom * f2.denom));
    }
    static MyFrac Divide(MyFrac f1, MyFrac f2)
    {
        if (f2.nom == 0) Console.WriteLine("Cannot divide by a fraction with zero numerator.");
        return Normalize((f1.nom * f2.denom, f1.denom * f2.nom));
    }
    static MyFrac CalcExpr1(int n)
    {
        MyFrac sum = (0, 1);
        for (int i = 1; i <= n; i++)
        {
            sum = Plus(sum, (1, i * (i + 1)));
        }
        return Normalize(sum);
    }
    static MyFrac CalcExpr2(int n)
    {
        MyFrac product = (1, 1);
        for (int i = 2; i <= n; i++)
        {
            MyFrac term = Minus((1, 1), (1, i * i));
            product = Multiply(product, term);
        }
        return Normalize(product);
    }
}