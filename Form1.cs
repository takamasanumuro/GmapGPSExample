using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using Microsoft.VisualBasic.ApplicationServices;

namespace GPSShow
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        PointLatLng location = new PointLatLng(-22.8570241, -43.0955684);

        public Form1()
        {
            InitializeComponent();
        }

        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = BingMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmap.Position = location;

            SetupGPSMarker();
           

            timerGPS.Start();
            timerGPS.Interval = 1000;
        }

        private void timerGPS_Tick(object sender, EventArgs e)
        {
            
            double lat_offset = Convert.ToDouble(random.Next(-3, 3)) / 10000.0;
            double lng_offset = Convert.ToDouble(random.Next(-3, 3)) / 10000.0;
            location.Lat += lat_offset;
            location.Lng += lng_offset;
            Console.WriteLine($"{location.Lat}, {location.Lng}");
            UpdateGPSIcon();          
                
        }

        private void SetupGPSMarker()
        {
            string iconPath = "Resources/boaticon1.bmp";
            Bitmap boatIcon = new Bitmap(iconPath);
            boatIcon.MakeTransparent();
            
            GMapOverlay boatOverlay = new GMapOverlay("boats");
            GMapMarker boatMarker = new GMarkerGoogle(location, boatIcon);
            boatOverlay.Markers.Add(boatMarker);
            gmap.Overlays.Add(boatOverlay);
        }

        private void UpdateGPSIcon()
        {
            gmap.Overlays[0].Markers[0].Position = location;
        }

        // Log message with thread id
        private void Log(string message)
        {
            Console.WriteLine("Thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, message);
        }
    }
}