﻿using Cipher.Analysis.CipherGuess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cipher.WPF.Controls
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

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            Analysis = new CipherAnalysis(Input);
            CipherResults = Analysis.Deviations
                .Select(KV => new KeyValuePair<string, int>(
                    KV.Key.Name, 
                    (int)Math.Round(KV.Value, MidpointRounding.AwayFromZero))
                ).OrderBy(KV => KV.Value).ToList();
            OnChanged("CipherResults");
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