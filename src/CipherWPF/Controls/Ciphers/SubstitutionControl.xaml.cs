using Cipher.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Subs = Cipher.Ciphers.Substitution<Cipher.Text.QuadgramScoredLetterArray>;

namespace Cipher.WPF.Controls.Ciphers
{
    /// <summary>
    /// Interaction logic for Substitution.xaml
    /// </summary>
    public partial class SubstitutionControl : UserControl, IDecode
    {
        public SubstitutionControl()
        {
            this.InitializeComponent();
        }

        public string Decode(string Input)
        {
            Subs Cipher = new Subs(Input);
            return Cipher.Decode(new LetterArray(Key.Text)).ToString();
        }

        public async Task<string> Crack(string Input)
        {
            Subs.CipherResult Result = await Task<Subs.CipherResult>.Run(() => new Subs(Input).Crack());

            Key.Text = Result.Key.ToString();
            return Result.Text.ToString();
        }
    }
}
