using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Xamarin.Database.Query;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using static Android.Views.View;

namespace FloweryTea.Activities.Account
{
    [Activity(Label = "Information", Theme = "@style/MyTheme")]
    public class InformationActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener, IFacebookCallback
    {
        private TextView account_text_email;
        private EditText account_new_password;
        private Button button_change_pass;
        private Button button_logout;
        private LoginButton button_facebook;
        private LinearLayout activity_account;

        private ICallbackManager mFBCallManager;

        private FirebaseAuth auth = MainActivity.auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FacebookSdk.SdkInitialize(ApplicationContext);
            SetContentView(Resource.Layout.Account_Information);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Account";
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            account_text_email = (TextView)FindViewById(Resource.Id.account_email);

            account_new_password = (EditText)FindViewById(Resource.Id.dasboard_new_password);
            button_change_pass = (Button)FindViewById(Resource.Id.account_button_change_password);
            button_logout = (Button)FindViewById(Resource.Id.account_button_logout);
            button_facebook = (LoginButton)FindViewById(Resource.Id.account_button_facebook);
            activity_account = (LinearLayout)FindViewById(Resource.Id.activity_account);
            button_change_pass.SetOnClickListener(this);
            button_logout.SetOnClickListener(this);

            if (auth.CurrentUser.Providers.ElementAt(0).Equals("facebook.com"))
            {
                button_logout.Visibility = ViewStates.Gone;
                button_facebook.Visibility = ViewStates.Visible;
                account_new_password.Visibility = ViewStates.Gone;
                button_change_pass.Visibility = ViewStates.Gone;

                account_text_email.Text = "Welcome " + auth.CurrentUser.DisplayName;

                RecordFacebookUser();

                mFBCallManager = CallbackManagerFactory.Create();
                button_facebook.RegisterCallback(mFBCallManager, this);
                button_facebook.TextChanged += delegate
                {
                    if (button_facebook.Text.Equals("Continue with Facebook"))
                    {
                        auth.SignOut();
                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                    }
                };
            }
            else
            {
                account_text_email.Text = "Welcome " + auth.CurrentUser.Email;
            }
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.account_button_change_password)
            {
                ChangePassword(account_new_password.Text);
            }
            else if (v.Id == Resource.Id.account_button_logout)
            {
                LogoutUser();
            }
        }

        private async void RecordFacebookUser()
        {
            FirebaseUser currentUser = auth.CurrentUser;
            var users = await MainActivity.firebase
                .Child("Accounts")
                .OrderByKey()
                .StartAt(currentUser.Uid)
                .EndAt(currentUser.Uid)
                .LimitToFirst(1)
                .OnceAsync<Models.Account>();

            if (users.Count == 0)
            {
                Models.Account acc = new Models.Account();
                acc.id = currentUser.Uid;
                acc.fullname = currentUser.DisplayName;
                acc.email = currentUser.Email;
                await MainActivity.firebase.Child("Accounts/" + currentUser.Uid).PutAsync<Models.Account>(acc);
            }
        }

        private void LogoutUser()
        {
            auth.SignOut();
            if (auth.CurrentUser == null)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }

        private void ChangePassword(string input_new_password)
        {
            if (account_new_password.Text == "")
            {
                account_new_password.Visibility = ViewStates.Visible;
            }
            else
            {
                FirebaseUser user = auth.CurrentUser;
                user.UpdatePassword(input_new_password)
                    .AddOnCompleteListener(this);
            }
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Snackbar snackBar = Snackbar.Make(activity_account, "Password has been changed", Snackbar.LengthShort);
                snackBar.Show();
            }
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public void OnCancel()
        {
            throw new NotImplementedException();
        }

        public void OnError(FacebookException error)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}