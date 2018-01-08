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
using static Android.Views.View;

namespace FloweryTea.Activities.Account
{
    [Activity(Label = "ForgotPasswordActivity", Theme = "@style/MyTheme")]
    public class ForgotPasswordActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        private EditText input_email;
        private Button button_reset_pass;
        private TextView link_back;
        private LinearLayout activity_forgot;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Account_ForgotPassword);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Forgot password";
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            input_email = (EditText)FindViewById(Resource.Id.forgot_email);
            button_reset_pass = (Button)FindViewById(Resource.Id.forgot_button_reset);
            link_back = (TextView)FindViewById(Resource.Id.forgot_button_back);
            activity_forgot = (LinearLayout)FindViewById(Resource.Id.activity_forgot);
            button_reset_pass.SetOnClickListener(this);
            link_back.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.forgot_button_back)
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            }
            else if (v.Id == Resource.Id.forgot_button_reset)
            {
                ResetPassword(input_email.Text);
            }
        }

        private void ResetPassword(string email)
        {
            MainActivity.auth.SendPasswordResetEmail(email)
                .AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == false)
            {
                Snackbar snackBar = Snackbar.Make(activity_forgot, "Email address is not valid.", Snackbar.LengthShort);
                snackBar.Show();

                View view1 = snackBar.View;
                TextView txtv = (TextView)view1.FindViewById(Resource.Id.forgot_error_message);
            }
            else
            {
                Snackbar snackBar = Snackbar.Make(activity_forgot, "Reset password link sent to email " + input_email.Text, Snackbar.LengthShort);
                snackBar.Show();

                View view1 = snackBar.View;
                TextView txtv = (TextView)view1.FindViewById(Resource.Id.forgot_error_message);
            }
        }
    }
}