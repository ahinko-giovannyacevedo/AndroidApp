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
    //, Theme="@style/CustomActionBarTheme"
    [Activity(Label = "@string/ApplicationName", Theme = "@style/CustomActionBarTheme")]
    public class VendorMainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.VendorMainLayout);

            Button c_btnBuscar = FindViewById<Button>(Resource.Id.btnBuscar);
            c_btnBuscar.Click += c_btnBuscar_Click;

            Button c_btnBorrar = FindViewById<Button>(Resource.Id.btnBorrar);
            c_btnBorrar.Click += (object sender, EventArgs a) => {
                EditText c_txtBarCodeSearch = FindViewById<EditText>(Resource.Id.txtBarCodeSearch);
                c_txtBarCodeSearch.Text = "";
            };
        }

        async void c_btnBuscar_Click(object sender, EventArgs e)
        {
            EditText c_txtBarCodeSearch = FindViewById<EditText>(Resource.Id.txtBarCodeSearch);

            if (c_txtBarCodeSearch.Text.Trim().Length == 0)
            {
                var scanner = new MobileBarcodeScanner(this);
                var result = await scanner.Scan();

                Handleresult(result);
            }
            else
            {
                //Significa que decidio teclear el producto en vez de escanearlo
                StartItemMainActivity(c_txtBarCodeSearch.Text.Trim());
            }

            c_txtBarCodeSearch.Text = "";
        }

        private void StartItemMainActivity(string barcode) {
            var itemActivity = new Intent(this, typeof(ItemMainActivity));
            itemActivity.PutExtra("barcode", barcode);
            StartActivity(itemActivity);
        }

        void Handleresult(ZXing.Result result) {
            var msg = "No BarCode!";

            if (result != null)
            {
                msg = "Barcode: " + result.Text + " [" + result.BarcodeFormat + "]";

                Toast.MakeText(this, msg, ToastLength.Long).Show();

                //Mostrar la pantalla donde se va a ver toda la informacion del Producto
            }
        }

        //Manipulando la devolucion de datos

        #region Menu Derecha
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
        #endregion
    }
}