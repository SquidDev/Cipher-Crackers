using System.Windows.Controls;
using Cipher.Text.WordParser;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using Cipher.Text.WordParser.Storage;
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

        public void Space(string Input, IEnumerable<string> ToLoad)
        {
            try
            {
                GuessLoader Loader = new GuessLoader(ToLoad);
                if (Loader.Count <= 0)
                {
                    throw new Exception("Must include samples");
                }

                Loader.Load();

                WordGuesser Guesser = new WordGuesser(Input, Loader.Frequencies);
                string Result = String.Join(" ", Guesser.Result);
                Dispatcher.BeginInvoke(SuccessHandler, Result);
            }
            catch (ThreadAbortException)
            {
                // Pass
            }
            catch (Exception e)
            {
                // Catch other exceptions
                Dispatcher.BeginInvoke(ErrorHandler, e.Message);
            }
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

        private void ToggleProcessing_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ProcessingThread == null || !ProcessingThread.IsAlive)
            {
                string InputValue = Input;
                List<string> SourceFiles = new List<string>();
                foreach (object Item in Sources.Items)
                {
                    string Source = Item.ToString();
                    if (!String.IsNullOrWhiteSpace(Source))
                    {
                        SourceFiles.Add(Source);
                    }
                }

                Action Starter = delegate()
                {
                    Space(InputValue, SourceFiles);
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

        private void AddSource_Click(object sender, RoutedEventArgs e)
        {
            Sources.Items.Add(new FileBrowserControl());
        }

        private void ClearSources_Click(object sender, RoutedEventArgs e)
        {
            Sources.Items.Clear();
        }
    }
}