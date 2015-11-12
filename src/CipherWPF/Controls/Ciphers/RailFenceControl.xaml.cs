using Cipher.Ciphers;
using Cipher.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cipher.WPF.Controls.Ciphers
{
    /// <summary>
    /// Interaction logic for CaeserShift.xaml
    /// </summary>
    public partial class RailFenceControl : UserControl, IDecode
    {
    	private readonly RailFence<CharacterTextArray, char> Cipher = new RailFence<CharacterTextArray, char>(TextScorers.ScoreQuadgrams);

        public RailFenceControl()
        {
            this.InitializeComponent();
        }

        public string Decode(string input)
        {
            return Cipher.Decode(input, (byte)Key.Value).ToString();
        }

        public async Task<string> Crack(string input)
        {
        	ICipherResult<int, CharacterTextArray> result = await Task<ICipherResult<int, CharacterTextArray>>.Run(() => Cipher.Crack(input));

            Key.Value = result.Key;
            return result.Contents.ToString();
        }
    }
}
