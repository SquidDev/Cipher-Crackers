using System.Threading.Tasks;
using System.Windows.Controls;
using Cipher.Ciphers;
using Cipher.Text;

namespace Cipher.WPF.Controls.Ciphers
{
    /// <summary>
    /// Interaction logic for CaeserShift.xaml
    /// </summary>
    public partial class CaeserShiftControl : UserControl, IDecode
    {
    	private readonly CaeserShift<LetterTextArray> Cipher = new CaeserShift<LetterTextArray>(TextScorers.ScoreQuadgrams);
        public CaeserShiftControl()
        {
            this.InitializeComponent();
        }

        public string Decode(string input)
        {
            return Cipher.Decode(input, (byte)Key.Value).ToString();
        }

        public async Task<string> Crack(string input)
        {
            ICipherResult<byte, LetterTextArray> result = await Task<ICipherResult<byte, LetterTextArray>>.Run(() => Cipher.Crack(input));

            Key.Value = result.Key;
            return result.Contents.ToString();
        }
    	
		public bool CanInvert()
		{
			return true;
		}
    	
		public void Invert()
		{
			Key.Value = Cipher.Invert((byte)Key.Value);
		}
    }
}
