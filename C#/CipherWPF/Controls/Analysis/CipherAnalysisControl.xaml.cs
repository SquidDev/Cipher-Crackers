using Cipher.Analysis.CipherGuess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Cipher.WPF.Controls.Analysis
{
    /// <summary>
    /// Interaction logic for CipherAnalysisControl.xaml
    /// </summary>
    public partial class CipherAnalysisControl : UserControl, INotifyPropertyChanged
    {
        public Dictionary<string, int> AnalysisResults { get; set; }
        public CipherAnalysis Analysis { get; set; }
        public List<KeyValuePair<string, int>> CipherResults { get; set; }

        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(String), typeof(CipherAnalysisControl), new FrameworkPropertyMetadata(String.Empty));
        public string Input
        {
            get { return GetValue(InputProperty).ToString(); }
            set { SetValue(InputProperty, value); }
        }

        public CipherAnalysisControl()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        private async void Go_Click(object sender, RoutedEventArgs e)
        {
            Go.IsRunning = true;

            string In = Input;
            Analysis = await Task<CipherAnalysis>.Run(() => new CipherAnalysis(In));

            // Sort the results and round
            CipherResults = Analysis.Deviations
                .Select(KV => new KeyValuePair<string, int>(
                    KV.Key.Name, 
                    (int)Math.Round(KV.Value, MidpointRounding.AwayFromZero))
                ).OrderBy(KV => KV.Value).ToList();

            OnChanged("CipherResults");

            AnalysisResults = new Dictionary<string,int>()
            {
                {"IC", (int)Math.Round(Analysis.TextData.IC, MidpointRounding.AwayFromZero)},
                {"MIC", (int)Math.Round(Analysis.TextData.MIC, MidpointRounding.AwayFromZero)},
                {"MKA", (int)Math.Round(Analysis.TextData.MKA, MidpointRounding.AwayFromZero)},
                {"DIC", (int)Math.Round(Analysis.TextData.DIC, MidpointRounding.AwayFromZero)},
                {"EDI", (int)Math.Round(Analysis.TextData.EDI, MidpointRounding.AwayFromZero)},
                {"LR", (int)Math.Round(Analysis.TextData.LR, MidpointRounding.AwayFromZero)},
                {"ROD", (int)Math.Round(Analysis.TextData.ROD, MidpointRounding.AwayFromZero)},
                {"LDI", (int)Math.Round(Analysis.TextData.LDI, MidpointRounding.AwayFromZero)},
                {"SDD", (int)Math.Round(Analysis.TextData.SDD, MidpointRounding.AwayFromZero)},
            };

            OnChanged("AnalysisResults");

            Go.IsRunning = false;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChanged(string Property)
        {
            if (!String.IsNullOrEmpty(Property) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }

        #endregion
    }
}
