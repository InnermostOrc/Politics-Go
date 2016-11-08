using Politics_Go.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Politics_Go.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        NearbyPollingStations pollManager;
        CatchableCreatures catchableCreaturesManager;
        NearbyCreatures nearbyCreaturesManager;
        NearbyHousesOfParliament nearbyHopsManager;
        PlayerMarkerManager pMarkerManager;
        public MapPage()
        {
            this.InitializeComponent();
            MainMap.Loaded += MapLoaded;
        }
        private void MapLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            pollManager = new NearbyPollingStations();
            catchableCreaturesManager = new CatchableCreatures();
            nearbyCreaturesManager = new NearbyCreatures();
            nearbyHopsManager = new NearbyHousesOfParliament();
            pMarkerManager = new PlayerMarkerManager();
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            // Set the map location.
            MainMap.Center = cityCenter;
            MainMap.ZoomLevel = 18;
            MainMap.LandmarksVisible = true;
            SetMapCentrePoint();
            MainMap.Visibility = Visibility.Collapsed;
            Loader.Visibility = Visibility.Visible;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainMap.Visibility = Visibility.Collapsed;
            Loader.Visibility = Visibility.Visible;
        }
        private async void SpawnTestEntities()
        {
            PollingStations.ItemsSource = await pollManager.FetchPOLLs(MainMap.Center.Position);
            Creatures.ItemsSource = await catchableCreaturesManager.FetchCreatures(MainMap.Center.Position);
            NearbyCreatures.ItemsSource = await nearbyCreaturesManager.FetchCreatures(MainMap.Center.Position);
            HousesOfParliament.ItemsSource = await nearbyHopsManager.FetchHOPs(MainMap.Center.Position);
            MainMap.Visibility = Visibility.Visible;
            Loader.Visibility = Visibility.Collapsed;
        }
        private async void SetMapCentrePoint()
        {
            // Set your current location.
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // Get the current location.
                    Geolocator geolocator = new Geolocator();
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    Geopoint myLocation = pos.Coordinate.Point;

                    // Set the map location.
                    MainMap.Center = myLocation;
                    MainMap.ZoomLevel = 18;
                    MainMap.LandmarksVisible = true;
                    MapIcon CurrentLocationMarker = new MapIcon();
                    /*// Locate your MapIcon  
                    CurrentLocationMarker.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/UI/PlayerMarker.png"));
                    // Setting up MapIcon location  
                    CurrentLocationMarker.Location = myLocation;
                    // Positon of the MapIcon  
                    CurrentLocationMarker.NormalizedAnchorPoint = new Point(0.5, 1);
                    MainMap.MapElements.Add(CurrentLocationMarker);*/
                    PlayerMarker.ItemsSource = pMarkerManager.FetchMarker(myLocation);
                    //Activate the map and it's items
                    SpawnTestEntities();
                    break;
                case GeolocationAccessStatus.Denied:
                    // Handle the case  if access to location is denied.
                    var dialog = new MessageDialog(Utils.Resources.Dialogs.GetString("GPSDenied"));

                    dialog.Commands.Add(new UICommand(Utils.Resources.Dialogs.GetString("Yes")) { Id = 0 });
                    dialog.Commands.Add(new UICommand(Utils.Resources.Dialogs.GetString("No")) { Id = 1 });
                    dialog.DefaultCommandIndex = 0;
                    dialog.CancelCommandIndex = 1;

                    var result = await dialog.ShowAsync();

                    if ((int)result.Id == 0)
                    {
                        SetMapCentrePoint();
                    }
                    else
                    {
                        App.Current.Exit();
                    }
                    break;

                case GeolocationAccessStatus.Unspecified:
                    // Handle the case if  an unspecified error occurs.
                    MessageDialog dialogUnspecified = new MessageDialog(Utils.Resources.Dialogs.GetString("UnspecifiedErrorOccured"));
                    await dialogUnspecified.ShowAsync();
                    App.Current.Exit();
                    break;
                default:
                    MessageDialog dialogOther = new MessageDialog(Utils.Resources.Dialogs.GetString("SomethingBad"));
                    await dialogOther.ShowAsync();
                    App.Current.Exit();
                    break;
            }
        }
        private async void PollingStationClicked(object sender, RoutedEventArgs e)
        {
            var buttonSender = sender as Button;
            PollingStation poll = buttonSender.DataContext as PollingStation;
            MessageDialog dialog = new MessageDialog(poll.DisplayName + " Clicked");
            await dialog.ShowAsync();
            //this.Frame.Navigate(typeof(PollingStationPage), poll.ID);
        }
        private async void creatureClicked(object sender, RoutedEventArgs e)
        {
            var buttonSender = sender as Button;
            Creature creature = buttonSender.DataContext as Creature;
            MessageDialog dialog = new MessageDialog(creature.Name + " Clicked");
            await dialog.ShowAsync();
        }
        private async void hopClicked(object sender, RoutedEventArgs e)
        {
            var buttonSender = sender as Button;
            HouseOfParliament hop = buttonSender.DataContext as HouseOfParliament;
            MessageDialog dialog = new MessageDialog(hop.DisplayName + " Clicked");
            await dialog.ShowAsync();
        }

        private void MenuOpen(object sender, RoutedEventArgs e)
        {
            if (Menu.Visibility == Visibility.Collapsed)
            {
                Menu.Visibility = Visibility.Visible;
            }
        }

        private void MenuClose(object sender, RoutedEventArgs e)
        {
            if (Menu.Visibility == Visibility.Visible)
            {
                Menu.Visibility = Visibility.Collapsed;
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
