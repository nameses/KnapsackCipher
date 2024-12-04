using System.Collections.Generic;

namespace KnapsackCipher.Logic
{
    public class KnapsackEncryptor
    {
        public static int EncryptBlock(List<int> publicKey, List<int> messageBlock)
        {
            int cipher = 0;
            for(int i = 0; i < publicKey.Count; i++)
            {
                cipher += publicKey[i] * messageBlock[i];
            }
            return cipher;
        }
    }
}
