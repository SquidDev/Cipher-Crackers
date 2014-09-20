
namespace Testing.Ciphers
{
    public abstract class CipherTest<TKey> : DataTest
    {
        protected abstract void InternalDecode(string Ciphertext, string Plaintext, TKey Key);
        protected abstract void InternalCrack(string Ciphertext, string Plaintext, TKey Key);
    }
}
