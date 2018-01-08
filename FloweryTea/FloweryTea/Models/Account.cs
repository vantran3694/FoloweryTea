using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FloweryTea.Models
{
    public class Account
    {
        public string id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
    }
}