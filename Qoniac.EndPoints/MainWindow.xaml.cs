using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Qoniac.EndPoints
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly HttpClient _client = new();
        private string BaseUrl = "https://localhost:44319/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEvaluate_OnClick(object sender, RoutedEventArgs e)
        {
            var curlPath = "api/CurrencyToWord";
            var txtNumber = TxtNumber.Text;
            if (!string.IsNullOrWhiteSpace(txtNumber))
            {
                var query = new Dictionary<string, string>()
                {
                    ["number"] = txtNumber
                };
                StringBuilder str = new StringBuilder();
                str.AppendFormat("{0}{1}", BaseUrl, curlPath);
                var ss = str.ToString();
                var result = Task.Run(async () =>
                    await _client.GetWithQueryStringAsync(str.ToString(), query));
                var content = result.Result;
                if (content.IsSuccessStatusCode)
                    lblShow.Content = content.Content.ReadAsStringAsync().Result;
                else
                    MessageBox.Show("You have an error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Please Enter Number", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }



    }
}
