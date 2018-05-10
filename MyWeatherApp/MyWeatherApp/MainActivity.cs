﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Runtime;
using System;
using Newtonsoft.Json;
using Android.Content;
using Com.Squareup.Picasso;

namespace MyWeatherApp
{
    [Activity(Label = "MyWeatherApp", MainLauncher = true)]
    public class MainActivity : Activity, ILocationListener
    {
        TextView txtCity, txtLastUpdate, txtDescription, txtHumidity, txtTime, txtCelsius;
        ImageView imgView;
        LocationManager locationManager;
        string provider;
        static double lat, lng;
        OpenWeatherMap openWeatherMap = new OpenWeatherMap();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Main);
            locationManager = (LocationManager)GetSystemService(Context.LocationService);
            provider = locationManager.GetBestProvider(new Criteria(), false);
            Location location = locationManager.GetLastKnownLocation(provider);
            if (location == null) System.Diagnostics.Debug.WriteLine("No Location");
        }
        protected override void OnResume()
        {
            base.OnResume();
            locationManager.RequestLocationUpdates(provider, 400, 1, this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }
        public void OnLocationChanged(Location location)
        {
            lat = Math.Round(location.Latitude, 4);
            lng = Math.Round(location.Longitude, 4);
            new GetWeather(this, openWeatherMap).Execute(Common.APIRequest(lat.ToString(), lng.ToString()));
        }
        public void OnProviderDisabled(string provider) { }
        public void OnProviderEnabled(string provider) { }
        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) { }
        private class GetWeather : AsyncTask<string, Java.Lang.Void, string>
        {
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            private MainActivity activity;
            OpenWeatherMap openWeatherMap;
            public GetWeather(MainActivity activity, OpenWeatherMap openWeatherMap)
            {
                this.activity = activity;
                this.openWeatherMap = openWeatherMap;
            }
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
                pd.SetTitle("Please wait....");
                pd.Show();
            }
            protected override string RunInBackground(params string[] @params)
            {
                string stream = null;
                string urlString = @params[0];
                Helper http = new Helper();
                //urlString = Common.Common.APIRequest(lat.ToString(), lng.ToString());  
                stream = http.GetHTTPData(urlString);
                return stream;
            }
            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);
                if (result.Contains("Error: Not City Found"))
                {
                    pd.Dismiss();
                    return;
                }
                openWeatherMap = JsonConvert.DeserializeObject<OpenWeatherMap>(result);
                pd.Dismiss();
                //Controls   
                activity.txtCity = activity.FindViewById<TextView>(Resource.Id.txtCity);
                activity.txtLastUpdate = activity.FindViewById<TextView>(Resource.Id.txtLastUpdate);
                activity.txtDescription = activity.FindViewById<TextView>(Resource.Id.txtDescription);
                activity.txtHumidity = activity.FindViewById<TextView>(Resource.Id.txtHumidity);
                activity.txtTime = activity.FindViewById<TextView>(Resource.Id.txtTime);
                activity.txtCelsius = activity.FindViewById<TextView>(Resource.Id.txtCelsius);
                activity.imgView = activity.FindViewById<ImageView>(Resource.Id.imageView);
                //Add Data   
                activity.txtCity.Text =  "{openWeatherMap.name},{openWeatherMap.sys.country}";
                activity.txtLastUpdate.Text =  "Last Updated: {DateTime.Now ()}";
                activity.txtDescription.Text =  "{openWeatherMap.weather[0].description}";
                activity.txtHumidity.Text =  "Humidity: {openWeatherMap.main.humidity} %";
              //  activity.txtTime.Text =  "{Common.Common.UnixTimeStampToDateTime(openWeatherMap.sys.sunrise).ToString("HH: mm ")}/{Common.Common.UnixTimeStampToDateTime(openWeatherMap.sys.sunset).ToString("HH: mm")}";
                activity.txtCelsius.Text =  "{openWeatherMap.main.temp} °C";
                if (!string.IsNullOrEmpty(openWeatherMap.weather[0].icon))
                {
                    Picasso.With(activity.ApplicationContext).Load(Common.GetImage(openWeatherMap.weather[0].icon)).Into(activity.imgView);
                }
            }
        }
    }
 
}

