using Cipher.Analysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Cipher.WPF.Controls.Analysis
{
    /// <summary>
    /// Interaction logic for Substitution.xaml
    /// </summary>
    public partial class NGramControl : UserControl, INotifyPropertyChanged
    {
        public List<KeyValuePair<string, int>> Frequencies { get; protected set; }

        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(String), typeof(NGramControl), new FrameworkPropertyMetadata(String.Empty));
        public string Input
        {
            get { return GetValue(InputProperty).ToString(); }
            set { SetValue(InputProperty, value); }
        }

        public NGramControl()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            Frequencies = NGrams.GatherNGrams(Input, (int)NGramLength.Value).OrderByDescending(A => A.Value).ToList();
            OnChanged("Frequencies");
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChanged(string Property)
        {
            if(!String.IsNullOrEmpty(Property) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }

        #endregion
    }
}
