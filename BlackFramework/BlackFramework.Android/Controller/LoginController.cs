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
using PortableRazor;
using ModelViewUI.Views;

namespace BlackFramework.Droid.Controller
{
    class LoginController
    {
        IHybridWebView webView;
        Context ctx;
        string txtUserName;
        string txtPassword;
        bool ActivityFlag = false;
        string msg = "";
        string message;
        string version;
        string DeviceID = null;

        public LoginController(IHybridWebView webView, Context ctx)
        {
            this.webView = webView;
            this.ctx = ctx;
        }


        public void OnCreate()
        {
            ChangeLoginCaption();
        }

        public void ChangeLoginCaption()
        {
            var template = new Login { };
            var page = template.GenerateString();
            webView.LoadHtmlString(page);
        }

    }
}