using System.Threading.Tasks;

namespace Cipher.WPF
{
    /// <summary>
    /// Used for controls that decode text
    /// </summary>
    /// <remarks>IDecode, do you?</remarks>
	public interface IDecode
	{
        string Decode(string Input);
        Task<string> Crack(string Input);
	}
}