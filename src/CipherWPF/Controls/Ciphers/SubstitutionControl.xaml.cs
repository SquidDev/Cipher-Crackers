using System.Threading.Tasks;
using System.Windows.Controls;
using Cipher.Ciphers;
using Cipher.Text;

namespace Cipher.WPF.Controls.Ciphers
{
    /// <summary>
    /// Interaction logic for Substitution.xaml
    /// </summary>
    public partial class SubstitutionControl : UserControl, IDecode
    {
    	private readonly Substitution<LetterTextArray> Cipher = new Substitution<LetterTextArray>(TextScorers.ScoreQuadgrams);

        public SubstitutionControl()
        {
            this.InitializeComponent();
        }

        public string Decode(string input)
        {
        	return Cipher.Decode(input, KeyConverters.String.FromString(Key.Text)).ToString();
        }

        public async Task<string> Crack(string input)
        {
        	ICipherResult<byte[], LetterTextArray> result = await Task<ICipherResult<byte[], LetterTextArray>>.Run(() => Cipher.Crack(input));

            Key.Text = KeyConverters.String.ToString(result.Key);
            return result.Contents.ToString();
        }
    	
		public bool CanInvert()
		{
			return true;
		}
    	
		public void Invert()
		{
			Key.Text = KeyConverters.String.ToString(Cipher.Invert(KeyConverters.String.FromString(Key.Text)));
		}
    }
}
