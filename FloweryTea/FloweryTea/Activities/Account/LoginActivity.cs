using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

namespace FloweryTea.Activities.Account
{
    [Activity(Label = "LoginActivity", Theme = "@style/MyTheme")]
    public class LoginActivity : AppCompatActivity, IOnCompleteListener, IFacebookCallback
    {
        private Button button_login;
        private LoginButton button_facebook;
        private TextView button_register;
        private TextView login_text_email;
        private TextView login_text_password;
        private TextView login_link_forgot;
        private LinearLayout activity_login;

        private FirebaseAuth auth = MainActivity.auth;

        private ICallbackManager mFBCallManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            FacebookSdk.SdkInitialize(ApplicationContext);

            SetContentView(Resource.Layout.Account_Login);           

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Login";
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            login_link_forgot = (TextView)FindViewById(Resource.Id.login_link_forgot);
            login_text_email = (TextView)FindViewById(Resource.Id.login_text_email);
            login_text_password = (TextView)FindViewById(Resource.Id.login_text_password);
            button_login = (Button)FindViewById(Resource.Id.login_button_login);
            button_facebook = (LoginButton)FindViewById(Resource.Id.login_button_facebook);
            button_register = (TextView)FindViewById(Resource.Id.login_button_register);
            activity_login = (LinearLayout)FindViewById(Resource.Id.activity_login);

            button_facebook.SetReadPermissions(new List<string> {
                    "email",
                    "public_profile"
                });

            mFBCallManager = CallbackManagerFactory.Create();
            button_facebook.RegisterCallback(mFBCallManager, this);

            login_link_forgot.Click += delegate
            {
                Intent intent = new Intent(this, typeof(ForgotPasswordActivity));
                StartActivity(intent);
            };

            button_login.Click += delegate
            {
                LoginUser(login_text_email.Text, login_text_password.Text);
            };

            button_register.Click += delegate
            {
                Intent intent = new Intent(this, typeof(RegisterActivity));
                StartActivity(intent);
            };
        }

        private void LoginUser(string email, string password)
        {
            auth.SignInWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Intent intent = new Intent(this, typeof(InformationActivity));
                StartActivity(intent);
            }
            else
            {
                TextView txtv = (TextView)FindViewById(Resource.Id.login_error_message);
                Toast snackBar = Toast.MakeText(activity_login.Context, "Email or password is invalid", ToastLength.Short);
                snackBar.Show();
            }
        }

        void IFacebookCallback.OnCancel()
        {
            throw new NotImplementedException();
        }

        void IFacebookCallback.OnError(FacebookException error)
        {
            throw new NotImplementedException();
        }

        private void handleFacebookAccessToken(AccessToken accessToken)
        {
            AuthCredential credential = FacebookAuthProvider.GetCredential(accessToken.Token);
            auth.SignInWithCredential(credential).AddOnCompleteListener(this, this);
        }

        void IFacebookCallback.OnSuccess(Java.Lang.Object result)
        {
            // set facebook token
            LoginResult loginResult = result as LoginResult;
            handleFacebookAccessToken(loginResult.AccessToken);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}