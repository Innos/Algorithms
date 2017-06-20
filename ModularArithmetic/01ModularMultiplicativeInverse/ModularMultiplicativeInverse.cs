namespace _01ModularMultiplicativeInverse
{
    using System;

    public class ModularMultiplicativeInverse
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(FindModularMultiplicativeInverse(5, 7));
            Console.WriteLine(FindModularMultiplicativeInverse(4, 9));
            Console.WriteLine(FindModularMultiplicativeInverse(7, 5));
            Console.WriteLine(FindModularMultiplicativeInverse(9, 4));
        }

        // Solves at = 1 (mod n) - congruence 
        private static int FindModularMultiplicativeInverse(int a, int n)
        {
            var t = 0;
            var newT = 1;
            var r = n;
            var newR = a;

            while (newR != 0)
            {
                var q = r / newR;
                var oldT = t;
                t = newT;
                newT = oldT - q * newT;

                var oldR = r;
                r = newR;
                newR = oldR - q * newR;
            }

            if (r > 1) return -1;
            if (t < 0) t = t + n;
            return t;
        }
    }
}
