using AppControleFinanceiro.Models;
using System.Globalization;


namespace AppControleFinanceiro.Libraries.Converters
{
    internal class TransactionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Transaction transaaction = (Transaction)value;

            if (transaaction == null)
            {
                return string.Empty;
            }
            if (transaaction.Type == TransactionType.Income)
            {
                return transaaction.Value.ToString("C");
            }
            else
            {
                return $"- {transaaction.Value.ToString("C")}";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
