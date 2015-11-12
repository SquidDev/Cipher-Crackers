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
		
        public string Decode(string input)
        {
            Hill cipher = new Hill(TextScorers.ScoreQuadgrams, (int)KeyLength.Value);
            return cipher.Decode(input, KeyConverters.Matrix.FromString(Key.Text)).ToString();
        }
		
        public async Task<string> Crack(string input)
        {
        	Func<ICipherResult<Matrix<float>, NGramArray>> crack;
            if (Cribs.Text.Length == 0) 
            {
            	HillBrute cipher = new HillBrute(TextScorers.ScoreMonograms, (int)KeyLength.Value);
            	crack = () => cipher.Crack(input);
            }
            else
            {
            	CribSpace space = new CribSpace((int)KeyLength.Value);
            	HillCribbed cipher = new HillCribbed(TextScorers.ScoreMonograms, (int)KeyLength.Value);
            	foreach (string item in Cribs.Text.Split(';'))
	            {
	                int index = item.IndexOf(':');
	                if (index == -1) throw new ArgumentException("Invalid mapping: " + item);
					
	                space.Add(item.Substring(0, index), item.Substring(index + 1));
	            }
            	
            	crack = () => cipher.Crack(input, space);
            }
			
            
            ICipherResult<Matrix<float>, NGramArray> result = await Task<ICipherResult<Matrix<float>, NGramArray>>.Run(crack);

            Key.Text = KeyConverters.Matrix.ToString(result.Key);
            return result.Contents.ToString();
        }
    }
}
