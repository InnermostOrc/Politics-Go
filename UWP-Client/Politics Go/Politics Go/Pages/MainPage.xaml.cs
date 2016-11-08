using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Politics_Go.Pages;
using Windows.UI.Popups;
using System.Net.Http;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Politics_Go.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void Login(object sender, RoutedEventArgs e)
        {
            Loader.Visibility = Visibility.Visible;
            //Get ready for wrong name/pass
            MessageDialog dialog = new MessageDialog("Incorrect Username or Password");
            //Prevent Sneak Peaks
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("http://api.nerdthings.ga/Politics_Go/Login/verifyUser.php?usr=" + UsernameBox.Text + "&pas=" + PasswordBox.Password));
            //Get result
            var validity = await response.Content.ReadAsStringAsync();
            if (validity == "1")
            {
                Loader.Visibility = Visibility.Collapsed;
                this.Frame.Navigate(typeof(MapPage));
            }
            else
            {
                Loader.Visibility = Visibility.Collapsed;
                await dialog.ShowAsync();
                PasswordBox.Password = "";
            }
        }
        private void UsernameBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //Login_Buttonless();
        }

        private void PasswordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //Login_Buttonless();
        }
    }
}