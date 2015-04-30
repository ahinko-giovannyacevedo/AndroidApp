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
    [Activity(Label = "@string/ApplicationName", Theme = "@style/CustomActionBarTheme")]
    public class ItemMainActivity : Activity
    {
        string barcode = string.Empty;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ItemMainLayout);
            barcode = Intent.GetStringExtra("barcode") ?? "No Barcode Available";
            // Create your application here
            Toast.MakeText(this, barcode, ToastLength.Short).Show();

            Button c_btnShoppingCar = FindViewById<Button>(Resource.Id.btnShoppingCar);
            c_btnShoppingCar.Click += (object sender, EventArgs e) => {
                Toast.MakeText(this, "Item Agregado al carro", ToastLength.Short).Show();
            };
        }
    }
}