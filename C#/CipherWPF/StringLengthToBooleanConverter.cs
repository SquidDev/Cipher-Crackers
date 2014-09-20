using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Cipher.WPF
{
    public class StringLengthToBooleanConverter : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            if (Value is string)
            {
                string SValue = (string)Value;
                return !String.IsNullOrWhiteSpace(SValue);
            }

            return false;
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            throw new NotImplementedException();
        }
    }
}
