using Cipher.Analysis.AutoSpace;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Cipher.WPF.Controls.Analysis
{
	/// <summary>
	/// Interaction logic for AutoSpaceControl.xaml
	/// </summary>
	public partial class AutoSpaceControl : UserControl
	{
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(String), typeof(AutoSpaceControl), new FrameworkPropertyMetadata(String.Empty));
        public String Input
        {
            get { return GetValue(InputProperty).ToString(); }
            set { SetValue(InputProperty, value); }
        }

        public AutoSpaceControl()
		{
			this.InitializeComponent();
		}

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.IsRunning = true;
            Exception Error = null;
            try
            {
                string In = Input;
                ResultText.Text = await Task<string>.Run(() => new WordGuesser(In).Result);
            }
            catch(Exception Er)
            {
                Error = Er;
            }
            
            Start.IsRunning = false;

            if(Error != null)
            {
                // We have to use this override to get the parent element.
                if(Window.GetWindow(this) is MainWindow)
                {
                    ((MainWindow)Window.GetWindow(this)).ShowErrorMessage(Error);
                }
                else
                {
                    throw Error;
                }
            }
        }
    }
}