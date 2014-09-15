using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Windows.Controls;

namespace Cipher.WPF.Controls
{
    /// <summary>
    /// Interaction logic for FileBrowser.xaml
    /// </summary>
    public partial class FileBrowserControl : UserControl
    {
        protected CommonOpenFileDialog OpenFile;
        protected CommonOpenFileDialog OpenFolder;
        public FileBrowserControl()
        {
            InitializeComponent();

            OpenFile = new CommonOpenFileDialog();
            OpenFile.Title = "Select text sources";

            OpenFile.Filters.Add(new CommonFileDialogFilter("All files (*.*)", "*.*"));
            OpenFile.Filters.Add(new CommonFileDialogFilter("Text files (*.txt)", "*.txt"));
            OpenFile.Filters.Add(new CommonFileDialogFilter("Dictionaries (*.dict)", "*.dict"));
            OpenFile.Filters.Add(new CommonFileDialogFilter("Xml Dictionaries (*.xml)", "*.xml"));

            OpenFolder = new CommonOpenFileDialog();
            OpenFolder.Title = "Select text sources";
            OpenFolder.IsFolderPicker = true;
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {

            if (OpenFile.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Input.Text = OpenFile.FileName;
            }
        }

        private void LoadFolder_Click(object sender, RoutedEventArgs e)
        {
            if (OpenFolder.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Input.Text = OpenFolder.FileName;
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            ((ItemsControl)Parent).Items.Remove(this);
        }

        public override string ToString()
        {
            return Input.Text;
        }
    }
}
