using System.Collections.Generic;

namespace KnapsackCipher.Logic
{
    public class KnapsackDecryptor
    {
        public static List<int> DecryptBlock(int cipher, List<int> superIncreasingSequence, int multiplierInverse, int modulus)
        {
            List<int> result = new List<int>();
            int reducedCipher = (cipher * multiplierInverse) % modulus;

            for(int i = superIncreasingSequence.Count - 1; i >= 0; i--)
            {
                if(reducedCipher >= superIncreasingSequence[i])
                {
                    result.Insert(0, 1);
                    reducedCipher -= superIncreasingSequence[i];
                }
                else
                {
                    result.Insert(0, 0);
                }
            }

            return result;
        }
    }
}
