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
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;

namespace FloweryTea.Activities.Account
{
    [Activity(Label = "RegisterActivity", Theme = "@style/MyTheme")]
    public class RegisterActivity : AppCompatActivity, IOnCompleteListener
    {
        private Button button_register;
        private static TextView register_text_name;
        private TextView register_text_email;
        private TextView register_text_password;
        private TextView register_link_agree;
        private LinearLayout activity_register;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Account_Register);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Register";
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            register_text_name = (TextView)FindViewById(Resource.Id.register_text_name);
            register_text_email = (TextView)FindViewById(Resource.Id.register_text_email);
            register_text_password = (TextView)FindViewById(Resource.Id.register_text_password);
            register_link_agree = (TextView)FindViewById(Resource.Id.register_link_agree);
            button_register = (Button)FindViewById(Resource.Id.register_button_register);
            activity_register = (LinearLayout)FindViewById(Resource.Id.activity_register);

            register_link_agree.TextFormatted = Html.FromHtml("<span>By creating an account, you agree to our <a href='condition://'>Conditions of Use</a>. </span>");
            register_link_agree.MovementMethod = LinkMovementMethod.Instance;

            button_register.Click += delegate
            {
                RegisterUser(register_text_email.Text, register_text_password.Text);
            };
        }

        private void RegisterUser(string email, string password)
        {
            MainActivity.auth.CreateUserWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this);
        }

        public async void OnComplete(Task task)
        {
            Console.WriteLine("aaa");
            if (task.IsSuccessful)
            {
                Console.WriteLine("bbb");
                var user = MainActivity.auth.CurrentUser;
                Console.WriteLine("ddd");
                /*user.SendEmailVerification();
                if (user.IsEmailVerified)
                {*/
                Models.Account acc = new Models.Account();
                acc.id = user.Uid;
                acc.fullname = register_text_name.Text;
                acc.email = user.Email;
                Console.WriteLine("eee");
                //Add item
                Console.WriteLine("firebase: " + MainActivity.firebase);
                await MainActivity.firebase.Child("Accounts/" + user.Uid).PutAsync<Models.Account>(acc);
                Console.WriteLine("fff");
                /*}
                else
                {
                }*/
                Intent intent = new Intent(this, typeof(InformationActivity));
                StartActivity(intent);
            }
            else
            {
                Console.WriteLine("ccc");
                Snackbar snackBar = Snackbar.Make(activity_register, "Register failed.", Snackbar.LengthShort);
                snackBar.Show();

                View view1 = snackBar.View;
                TextView txtv = (TextView)view1.FindViewById(Resource.Id.register_error_message);
            }
        }
    }
}