using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Politics_Go.Classes;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Json;
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Politics_Go.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    ///
    public sealed partial class PollingStationPage : Page
    {
        GetPollingStationData PollStationDataManager;

        public static string ID { get; private set; }

        public PollingStationPage()
        {
            this.InitializeComponent();
            PollStationDataManager = new GetPollingStationData();
            LoadData();
        }
        public void LoadData()
        {
            var id = PollingStationPage.ID;
            PollName.Text = id;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string id = e.Parameter as string;
            PollingStationPage.ID = id;
        }
    }
}