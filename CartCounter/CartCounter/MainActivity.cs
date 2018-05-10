using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V4.View;

namespace CartCounter
{
    [Activity(Label = "CartCounter", MainLauncher = true,Theme ="@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.cartItem, menu);
            IMenuItem item = menu.FindItem(Resource.Id.badge);
            MenuItemCompat.SetActionView(item,Resource.Layout.Main);
            RelativeLayout notifCount = (RelativeLayout)MenuItemCompat.GetActionView(item);

            TextView tv = (TextView)notifCount.FindViewById(Resource.Id.actionbar_notifcation_textview);
            tv.Text = "12";

            return base.OnCreateOptionsMenu(menu);
        }
    }
}

