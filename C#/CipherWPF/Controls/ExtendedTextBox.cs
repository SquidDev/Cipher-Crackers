using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cipher.WPF.Controls
{
    /// <summary>
    /// Mulitline TextBox that adds extra items to the context menu.
    /// </summary>
    public class ExtendedTextBox : TextBox
    {
        static ExtendedTextBox()
        {
            // Need to keep styles
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedTextBox), new FrameworkPropertyMetadata(typeof(ExtendedTextBox)));
        }

        protected OpenFileDialog Open;
        protected SaveFileDialog Save;
        public ExtendedTextBox()
            : base()
        {
            // Set default properties
            TextWrapping = System.Windows.TextWrapping.Wrap;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            AcceptsReturn = true;

            Loaded += ExtendedTextBox_Loaded;

            // Add some dialogs.
            Open = new OpenFileDialog()
            {
                Title = "Open",
                Filter = "Text file (*.txt)|*.txt|All files (*.*)|All files",
            };

            Save = new SaveFileDialog()
            {
                Title = "Save",
                Filter = "Text file (*.txt)|*.txt|All files (*.*)|All files",
            };
        }

        protected void ExtendedTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            ContextMenu = new ContextMenu();

            // Add pre-existing items
            ContextMenu.Items.Add(new MenuItem()
                {
                    Header = "Cut",
                    Command = ApplicationCommands.Cut,
                    InputGestureText = "Ctrl + X",
                });

            ContextMenu.Items.Add(new MenuItem()
                {
                    Header = "Copy",
                    Command = ApplicationCommands.Copy,
                    InputGestureText = "Ctrl + C",
                });

            ContextMenu.Items.Add(new MenuItem()
                {
                    Header = "Paste",
                    Command = ApplicationCommands.Paste,
                    InputGestureText = "Ctrl + V",
                });

            ContextMenu.Items.Add(new Separator());

            ContextMenu.Items.Add(new MenuItem()
                {
                    Header = "Remove",
                    Items =
                    {
                        ItemFactory("Spaces", () => Text.Where(C => !Char.IsWhiteSpace(C))),
                        ItemFactory("Non-letters", () => Text.Where(C => Char.IsLetter(C))),
                        ItemFactory("Non-letters/numbers", () => Text.Where(C => Char.IsLetterOrDigit(C))),
                    },
                });

            ContextMenu.Items.Add(new MenuItem()
                {
                    Header = "Format",
                    Items =
                    {
                        ItemFactory("Upper", () => Text.ToUpper()),
                        ItemFactory("Lower", () => Text.ToLower()),
                    },
                });

            ContextMenu.Items.Add(new Separator());

            // Add file system items
            ContextMenu.Items.Add(ItemFactory("Save", delegate()
                    {
                        bool? Result = Save.ShowDialog();
                        if (Result.HasValue && Result.Value && !String.IsNullOrWhiteSpace(Save.FileName))
                        {
                            using (StreamWriter Writer = new StreamWriter(Save.FileName))
                            {
                                Writer.Write(Text);
                            }
                        }
                    }));

            ContextMenu.Items.Add(ItemFactory("Open", delegate()
                    {
                        bool? Result = Open.ShowDialog();
                        if (Result.HasValue && Result.Value && !String.IsNullOrWhiteSpace(Open.FileName))
                        {
                            using (StreamReader Reader = new StreamReader(Open.FileName))
                            {
                                Text = Reader.ReadToEnd();
                            }
                        }
                    }));
        }

        #region Factories
        /// <summary>
        /// Create a menu item
        /// </summary>
        /// <param name="Header">Text of the menu item</param>
        /// <param name="Execute">A function that is run on click. 
        /// The result is set to the text of the box</param>
        /// <returns>The created menu item</returns>
        protected MenuItem ItemFactory(string Header, Func<string> Execute)
        {
            MenuItem Item = new MenuItem();
            Item.Header = Header;
            Item.Click += (S, E) => Text = Execute();
            return Item;
        }

        /// <summary>
        /// Create a menu item
        /// </summary>
        /// <param name="Header">Text of the menu item</param>
        /// <param name="Execute">A function that is run on click. 
        /// The result is set to the text of the box</param>
        /// <returns>The created menu item</returns>
        protected MenuItem ItemFactory(string Header, Func<IEnumerable<char>> Execute)
        {
            MenuItem Item = new MenuItem();
            Item.Header = Header;
            Item.Click += (S, E) => Text = String.Concat(Execute());
            return Item;
        }

        /// <summary>
        /// Create a menu item
        /// </summary>
        /// <param name="Header">Text of the menu item</param>
        /// <param name="Execute">A function that is run on click</param>
        /// <returns>The created menu item</returns>
        protected MenuItem ItemFactory(string Header, Action Execute)
        {
            MenuItem Item = new MenuItem();
            Item.Header = Header;
            Item.Click += (S, E) => Execute();
            return Item;
        }
        #endregion
    }
}
