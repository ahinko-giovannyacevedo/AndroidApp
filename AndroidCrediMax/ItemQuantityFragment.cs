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
    public class ItemQuantityFragment : DialogFragment   
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.ItemQuantityFragLayout, container, false);

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
            ProgressBar c_pgbRunning = View.FindViewById<ProgressBar>(Resource.Id.pgbRunning);
            c_pgbRunning.Visibility = ViewStates.Visible;
            await ProcessShoppingCart();
            c_pgbRunning.Visibility = ViewStates.Invisible;
            Dismiss();
        }

        async Task ProcessShoppingCart() {
            await Task.Run(() => {
                System.Threading.Thread.Sleep(5000);
            });
        }
    }
}