using System;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackCipher.Logic
{
    public class KnapsackKeyGenerator
    {
        public List<int> SuperIncreasingSequence { get; private set; }
        public List<int> PublicKey { get; private set; }
        public int Modulus { get; private set; }
        public int Multiplier { get; private set; }
        public int MultiplierInverse { get; private set; }

        public KnapsackKeyGenerator()
        {
            SuperIncreasingSequence = new List<int>();
            PublicKey = new List<int>();
        }

        public void GenerateKeys(int length)
        {
            GenerateSuperIncreasingSequence(length);
            GenerateModulusAndMultiplier();
            GeneratePublicKey();
        }

        private void GenerateSuperIncreasingSequence(int length)
        {
            Random random = new Random();
            SuperIncreasingSequence.Clear();

            int sum = 0;
            for(int i = 0; i < length; i++)
            {
                int nextValue = sum + random.Next(1, 10);
                SuperIncreasingSequence.Add(nextValue);
                sum += nextValue;
            }
        }

        private void GenerateModulusAndMultiplier()
        {
            int sum = SuperIncreasingSequence.Sum();
            Modulus = sum + new Random().Next(1, 10);

            do
            {
                Multiplier = new Random().Next(2, Modulus);
            } while(Gcd(Multiplier, Modulus) != 1);

            MultiplierInverse = FindMultiplicativeInverse(Multiplier, Modulus);
        }

        private void GeneratePublicKey()
        {
            PublicKey.Clear();
            foreach(var value in SuperIncreasingSequence)
            {
                PublicKey.Add((value * Multiplier) % Modulus);
            }
        }

        private int Gcd(int a, int b)
        {
            return b == 0 ? a : Gcd(b, a % b);
        }

        private int FindMultiplicativeInverse(int a, int m)
        {
            int m0 = m, t, q;
            int x0 = 0, x1 = 1;

            if(m == 1)
                return 0;

            while(a > 1)
            {
                q = a / m;
                t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            return x1 < 0 ? x1 + m0 : x1;
        }
    }
}
