using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace ahinko.android.credimax
{
    using Utility;
    using Newtonsoft.Json;

    public class ItemQuantityFragment : DialogFragment   
    {
        private DataContract.InventoryList iObj = null;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.ItemQuantityFragLayout, container, false);

            //if (savedInstanceState != null)
            //{
            //    string sObj = savedInstanceState.GetString("item_obj");
            //    if (!string.IsNullOrEmpty(sObj))
            //    {
            //        iObj = JsonConvert.DeserializeObject<DataContract.InventoryList>(sObj);
            //    }
            //}

            string sObj = Arguments.GetString("item_obj");
            if (!string.IsNullOrEmpty(sObj))
            {
                iObj = JsonConvert.DeserializeObject<DataContract.InventoryList>(sObj);
            }

            //bind all the controls
            Button c_btnCancelar = view.FindViewById<Button>(Resource.Id.btnCancelar);
            c_btnCancelar.Click += (object sender, EventArgs e) => Dismiss();

            Button c_btnAceptar = view.FindViewById<Button>(Resource.Id.btnAceptar);
            c_btnAceptar.Click += c_btnAceptar_Click;

            return view;
        }

        async void c_btnAceptar_Click(object sender, EventArgs e)
        {
            //Agregar a SQLite.
            TextView c_txtQuantity = View.FindViewById<TextView>(Resource.Id.txtQuantity);

            //Validar la cantidad
            if ((string.IsNullOrEmpty(c_txtQuantity.Text)) || (!Utility.AUtility.IsDecimal(c_txtQuantity.Text)))
            {
                Toast.MakeText(View.Context, "Digite un valor numerico", ToastLength.Short).Show();
                return;
            }

            ProgressBar c_pgbRunning = View.FindViewById<ProgressBar>(Resource.Id.pgbRunning);
            c_pgbRunning.Visibility = ViewStates.Visible;

            await ProcessShoppingCart();
            
            c_pgbRunning.Visibility = ViewStates.Invisible;
            Dismiss();
        }

        async Task ProcessShoppingCart() {
            await Task.Run(() => {
                //System.Threading.Thread.Sleep(5000);
                Db.SQLiteHelper db = new Db.SQLiteHelper();
                Db.Request req = null; Db.RequestDetail det = null;

                if (AUtility.RequestID != 0)
                {
                    req = db.GetRequestByID(AUtility.RequestID);
                }

                if (req == null)
                {
                    req = new Db.Request();
                    req.CreatedDate = DateTime.Now;
                    req.UserID = 0;
                    req.UserName = "credimax";

                    AUtility.RequestID = db.SaveRequest(req);
                }
                
                TextView c_txtQuantity = View.FindViewById<TextView>(Resource.Id.txtQuantity);

                //Insertar el detalle
                det = db.GetRequestDetailByFK(AUtility.RequestID, iObj.pCodigo);
                if (det != null)
                {
                    det.Quantity = Convert.ToDecimal(c_txtQuantity.Text.Trim());

                    db.UpdateRequestDetail(det);
                }
                else
                {
                    det = new Db.RequestDetail();
                    det.ItemNumber = iObj.pCodigo;
                    det.BarCode = iObj.pCodigo;
                    det.Name = iObj.nombre;
                    det.Description = iObj.descripcion;
                    det.Quantity = Convert.ToDecimal(c_txtQuantity.Text.Trim());
                    det.Price = Convert.ToDecimal(iObj.precioLista);

                    db.InsertRequestDetail(det);
                }
            });
        }
    }
}