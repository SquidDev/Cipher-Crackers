using Cipher.Ciphers;
using Cipher.Text;
using System.Windows.Controls;

namespace Cipher.WPF.Controls
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
            Substitution<QuadgramScoredLetterArray> Cipher = new Substitution<QuadgramScoredLetterArray>(Input);
            return Cipher.Decode(new LetterArray(Key.Text)).ToString();
        }

        public string Crack(string Input)
        {
            Substitution<QuadgramScoredLetterArray> Cipher = new Substitution<QuadgramScoredLetterArray>(Input);
            Substitution<QuadgramScoredLetterArray>.CipherResult Result = Cipher.Crack();

            Key.Text = Result.Key.ToString();
            return Result.Text.ToString();
        }
    }
}
