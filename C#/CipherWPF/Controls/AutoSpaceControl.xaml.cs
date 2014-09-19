using System.Windows.Controls;
using Cipher.Analysis.AutoSpace;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows;

namespace Cipher.WPF.Controls
{
	/// <summary>
	/// Interaction logic for AutoSpaceControl.xaml
	/// </summary>
	public partial class AutoSpaceControl : UserControl
	{
        Thread ProcessingThread;
        Action<string> ErrorHandler;
        Action<string> SuccessHandler;


        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(String), typeof(AutoSpaceControl), new FrameworkPropertyMetadata(String.Empty));
        public String Input
        {
            get { return GetValue(InputProperty).ToString(); }
            set { SetValue(InputProperty, value); }
        }

        public AutoSpaceControl()
		{
			this.InitializeComponent();

            ErrorHandler = ErrorMessage;
            SuccessHandler = SetOutText;
		}

        public void Space(string Input)
        {
#if !DEBUG
            try
            {
#endif
                WordGuesser Guesser = new WordGuesser(Input);
                Dispatcher.BeginInvoke(SuccessHandler, Guesser.Result);
#if !DEBUG
            }
            catch (ThreadAbortException)
            {
                // Pass
            }
            catch (ArgumentOutOfRangeException e)
            {
                // Catch other exceptions
                Dispatcher.BeginInvoke(ErrorHandler, e.Message);
            }
#endif
        }

        public void ErrorMessage(string Error)
        {
            ErrorMessages.Text = Error;
            ToggleProcessing.Content = "Add spaces";
        }

        public void SetOutText(string Result)
        {
            ResultText.Text = Result;
            ToggleProcessing.Content = "Add spaces";
        }

        private void ToggleProcessing_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessingThread == null || !ProcessingThread.IsAlive)
            {
                string InputValue = Input;

                Action Starter = delegate()
                {
                    Space(InputValue);
                };

                ProcessingThread = new Thread(new ThreadStart(Starter));
                ProcessingThread.SetApartmentState(ApartmentState.STA);
                ProcessingThread.IsBackground = true;

                ProcessingThread.Start();
                ToggleProcessing.Content = "Abort";
            }
            else
            {
                ProcessingThread.Abort();
                ToggleProcessing.Content = "Add spaces";
            }
        }
    }
}