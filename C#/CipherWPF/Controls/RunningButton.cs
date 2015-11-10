using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class RunningButton : Button
    {
        public static readonly DependencyProperty IsRunningProperty;
        static RunningButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RunningButton), new FrameworkPropertyMetadata(typeof(RunningButton)));
            IsRunningProperty = DependencyProperty.Register("IsRunning", typeof(Boolean), typeof(RunningButton), new FrameworkPropertyMetadata(false));
        }

        protected BindingExpression Enabled_BindingExpression;
        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set
            {
                SetValue(IsRunningProperty, value);

                // We want to disable the button when processing
                if (value)
                {
                    Enabled_BindingExpression = GetBindingExpression(IsEnabledProperty);
                    IsEnabled = false;
                }
                else
                {
                    // But re-set binding if we need to
                    IsEnabled = true;
                    SetBinding(IsEnabledProperty, Enabled_BindingExpression.ParentBindingBase);
                }
            }
        }
    }
}
