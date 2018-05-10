﻿using System;
using System.Text;
namespace MyWeatherApp
{
    class Common
    {
        public static string API_KEY = "8d9ca48603e4ff1235ee1ed9078f474b";
        public static string API_LINK = "https://api.openweathermap.org/data/2.5/weather";
        public static string APIRequest(string lat, string lng)
        {
            StringBuilder sb = new StringBuilder(API_LINK);
            sb.AppendFormat("?lat={0}&lon={1}&APPID={2}&units=metric", lat, lng, API_KEY);
            return sb.ToString();
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTimeStamp).ToLocalTime();
            return dt;
        }
        public static string GetImage(string icon)
        {
            return  "http://openweathermap.org/img/w/{icon}.png";
        }
    }
}