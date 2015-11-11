using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

using Cipher.Ciphers;
using Cipher.Text;
using Cipher.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.WPF.Controls.Ciphers
{
    public partial class HillControl : UserControl, IDecode
    {
        public HillControl()
        {
            InitializeComponent();
        }
		
        public string Decode(string Input)
        {
            Hill<NGramArray> cipher = new Hill<NGramArray>(Input, (int)KeyLength.Value);
            return cipher.Decode(MatrixExtensions.ReadMatrix(Key.Text)).ToString();
        }
		
        public async Task<string> Crack(string Input)
        {
            HillCribbed<NGramArray> hill = new HillCribbed<NGramArray>(Input);
            if (Cribs.Text.Length == 0) throw new ArgumentException("No mappings");
			
            foreach (string item in Cribs.Text.Split(';'))
            {
                int index = item.IndexOf(':');
                if (index == -1) throw new ArgumentException("Invalid mapping: " + item);
				
                hill.Add(item.Substring(0, index), item.Substring(index + 1));
            }
            HillCribbed<NGramArray>.CipherResult result = await Task<MonogramVigenere.CipherResult>.Run(() => hill.Crack());

            Key.Text = String.Join(";", result.Key.EnumerateColumns().Select(x => String.Join(",", x.Enumerate())));
            return result.Text.ToString();
        }
    }
}
