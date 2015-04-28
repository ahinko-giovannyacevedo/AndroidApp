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

//To BarCode
using ZXing;
using ZXing.Mobile;

namespace ahinko.android.credimax
{
    [Activity(Label = "@string/ApplicationName", Theme="@style/CustomActionBarTheme")]
    public class VendorMainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.VendorMainLayout);

            Button c_btnBuscar = FindViewById<Button>(Resource.Id.btnBuscar);
            c_btnBuscar.Click += c_btnBuscar_Click;
        }

        async void c_btnBuscar_Click(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner(this);
            var result = await scanner.Scan();

            Handleresult(result);
        }

        void Handleresult(ZXing.Result result) {
            var msg = "No BarCode!";

            if (result != null)
            {
                msg = "Barcode: " + result.Text + " [" + result.BarcodeFormat + "]";

                Toast.MakeText(this, msg, ToastLength.Long).Show();
            }
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