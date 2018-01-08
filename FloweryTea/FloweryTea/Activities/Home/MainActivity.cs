using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using FloweryTea.Activities.Account;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Views;
using Firebase.Auth;
using Firebase;
using Firebase.Xamarin.Database;

namespace FloweryTea
{
    [Activity(Label = "Beauty Day", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        public static FirebaseAuth auth;
        private FirebaseApp app;
        public static FirebaseClient firebase;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //change back arrow with icon
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.icon);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            //set action bar
            SupportActionBar.Title = "Flowery Tea";
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //call click event for left menu
            navigationView.NavigationItemSelected += HomeNavigationView_NavigationItemSelected;

            // Init firebase auth
            if (auth == null)
            {
                InitFirebaseAuth();
            }

            // Init firebase database
            if (firebase == null)
            {
                firebase = new FirebaseClient("https://flowerytea-a127c.firebaseio.com/");
            }
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:1050847703550:android:47d80f38b97b9b68")
                .SetApiKey("AIzaSyDSj76SrsCs3V7uHfDd0PKQ1IsoxENkIHw")
                .Build();
            if (app == null)
            {
                app = FirebaseApp.InitializeApp(this, options);
            }
            auth = FirebaseAuth.GetInstance(app);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void HomeNavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var menuItem = e.MenuItem;
            menuItem.SetChecked(!menuItem.IsChecked);
            Intent intent;
            switch (menuItem.ItemId)
            {
                case Resource.Id.nav_account:
                    if (auth.CurrentUser != null)
                    {
                        intent = new Intent(this, typeof(InformationActivity));
                    }
                    else
                    {
                        intent = new Intent(this, typeof(LoginActivity));
                    }
                    StartActivity(intent);
                    break;
            }
        }
    }
}

