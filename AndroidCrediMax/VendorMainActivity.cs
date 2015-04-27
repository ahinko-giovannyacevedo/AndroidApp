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

namespace ahinko.android.credimax
{
    [Activity(Label = "@string/ApplicationName")]
    public class VendorMainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.VendorMainLayout);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            MenuInflater inflater = this.MenuInflater;

            inflater.Inflate(Resource.Menu.vendorMenu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);

            switch (item.ItemId)
            {
                case Resource.Id.add:
                    break;
                case Resource.Id.call:
                    Intent intent = new Intent(Intent.ActionDial);
                    StartActivity(intent);
                    break;
                default:
                    break;
            }

            return true;
        }
    }
}