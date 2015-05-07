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
using System.ServiceModel;
using Newtonsoft.Json;

namespace ahinko.android.credimax
{
    //, Theme="@style/CustomActionBarTheme"
    [Activity(Label = "@string/ApplicationName", Theme = "@style/CustomActionBarTheme")]
    public class VendorMainActivity : Activity
    {

        public static EndpointAddress endpoint = new EndpointAddress("http://192.168.100.68:10250/InventarioCreditMax/Servicios/ConsultaInventariosService.svc");
        ConsultaInventariosServiceClient client = null;

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

            /*Llamar al wcf*/
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            TimeSpan timeout = new TimeSpan(0, 0, 30);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;

            //Llamando al servicio de manera asincrona
            client = new ConsultaInventariosServiceClient(binding, endpoint);
            client.ObtenerDatosCompleted += client_ObtenerDatosCompleted;
            //client.ObtenerDatosCompleted += new EventHandler<ObtenerDatosCompletedEventArgs>(client_ObtenerDatosCompleted);
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
                CallWCF_Inventory();
            }
        }

        private void StartItemMainActivity(string barcode) {
            var itemActivity = new Intent(this, typeof(ItemMainActivity));
            itemActivity.PutExtra("barcode", barcode);
            StartActivity(itemActivity);
        }

        void Handleresult(ZXing.Result result) {

            if (result != null)
            {
                //msg = "Barcode: " + result.Text + " [" + result.BarcodeFormat + "]";
                //Toast.MakeText(this, msg, ToastLength.Long).Show();
                CallWCF_Inventory();
            }
        }

        //Manipulando la devolucion de datos
        private void CallWCF_Inventory() {
            ProgressBar c_prgCallingWCF = FindViewById<ProgressBar>(Resource.Id.prgCallingWCF);

            try
            {
                c_prgCallingWCF.Visibility = ViewStates.Visible;
                client.ObtenerDatosAsync("0215003000", "credimax");
            }
            catch (Exception)
            {
                Toast.MakeText(this, "El servicio no ha podido ser consultado", ToastLength.Long).Show();
                c_prgCallingWCF.Visibility = ViewStates.Invisible;
            }
        }

        private void client_ObtenerDatosCompleted(object sender, ObtenerDatosCompletedEventArgs e)
        {
            string json = "", msg = "";
            List<DataContract.InventoryList> lObj = null;

            try
            {
                if (e.Error != null)
                {
                    msg = e.Error.Message.ToString();
                }
                else if (e.Cancelled)
                {
                    msg = "La solicitud fue cancelada";
                }
                else
                {
                    json = e.Result;

                    lObj = JsonConvert.DeserializeObject<List<DataContract.InventoryList>>(json);

                    if (lObj == null)
                    {
                        msg = "El objecto no pudo ser deserializado";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
            }
            finally {
                RunOnUiThread(() =>
                {
                    ProgressBar c_prgCallingWCF = FindViewById<ProgressBar>(Resource.Id.prgCallingWCF);
                    EditText c_txtBarCodeSearch = FindViewById<EditText>(Resource.Id.txtBarCodeSearch);

                    c_prgCallingWCF.Visibility = ViewStates.Invisible;
                    c_txtBarCodeSearch.Text = "";

                    c_prgCallingWCF = null;
                    c_txtBarCodeSearch = null;

                    if (msg.Length > 0)
                    {
                        Toast.MakeText(this, msg, ToastLength.Long).Show();
                    }
                    else
                    {
                        //Llamar a la UI siguiente
                        StartItemMainActivity(json);
                    }
                });
            }
        }

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