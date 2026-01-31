using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System.Windows.Shapes;
using System.IO;
namespace SystemFlow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitWebView();

        }

        private async void InitWebView()
        {
            await webView.EnsureCoreWebView2Async(null); //initialize webView2


            //Expose C# object to javascript file
            webView.CoreWebView2.AddHostObjectToScript(
                "system",
                new SystemInfoComponents()
                );

            //Build absolute path to ui folder in the output directory
            var uiPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ui");
            //var uiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ui");
            //var indexPath = Path.Combine(uiPath, "index.html");

            webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "app.local",
                uiPath,
                Microsoft.Web.WebView2.Core.CoreWebView2HostResourceAccessKind.Allow
                );


            webView.CoreWebView2.Navigate("https://app.local/index.html");
            //webView.CoreWebView2.OpenDevToolsWindow();




        }

        private async void LoadTestPage()
        {
            await webView.EnsureCoreWebView2Async();

            webView.CoreWebView2.Navigate("https://www.google.com/");
        }

    }
}