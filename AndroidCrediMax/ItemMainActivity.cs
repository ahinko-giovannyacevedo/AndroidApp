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
using Newtonsoft.Json;

namespace ahinko.android.credimax
{
    [Activity(Label = "@string/ApplicationName", Theme = "@style/CustomActionBarTheme")]
    public class ItemMainActivity : Activity
    {
        string json = string.Empty;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ItemMainLayout);
            json = Intent.GetStringExtra("barcode") ?? "No Barcode Available";

            Button c_btnShoppingCar = FindViewById<Button>(Resource.Id.btnShoppingCar);
            c_btnShoppingCar.Click += (object sender, EventArgs e) =>
            {

                //Cargar el fragment que va a agregar al carrito de compra
                var transaction = FragmentManager.BeginTransaction();
                var dialogFragment = new ItemQuantityFragment();
                dialogFragment.Show(transaction, "dialog_fragment");

                Toast.MakeText(this, "Item Agregado al carro", ToastLength.Short).Show();
            };

            /*
             * Configurando el comportamiento de los layouts para simular un panel collapsable
             * TODO: Como hacer un control de usuario para no repetir el mismo codigo varias veces
             */

            LinearLayout c_StockHeaderLayout = FindViewById<LinearLayout>(Resource.Id.StockHeaderLayout);
            LinearLayout c_PriceHeaderLayout = FindViewById<LinearLayout>(Resource.Id.PriceHeaderLayout);
            LinearLayout c_CreditHeaderLayout = FindViewById<LinearLayout>(Resource.Id.CreditHeaderLayout);
            LinearLayout c_OtherHeaderLayout = FindViewById<LinearLayout>(Resource.Id.OtherHeaderLayout);

            c_StockHeaderLayout.Click += GenericHeaderLayout_Click;
            c_PriceHeaderLayout.Click += GenericHeaderLayout_Click;
            c_CreditHeaderLayout.Click += GenericHeaderLayout_Click;
            c_OtherHeaderLayout.Click += GenericHeaderLayout_Click;

            //Cargar la data inicial
            LoadData();
            //Mandar en segundo plano la solicitud de los planes de financiamiento asociados al producto
        }

        private void LoadData()
        {
            List<DataContract.InventoryList> lObj = null;
            DataContract.InventoryList iObj = null;

            try
            {
                lObj = JsonConvert.DeserializeObject<List<DataContract.InventoryList>>(json);

                if (lObj == null)
                {
                    Toast.MakeText(this, "El objecto no pudo ser deserializado", ToastLength.Long).Show();
                    return;
                }

                //Llenar por el momento los campos principales
                iObj = lObj[0];

                TextView c_txtItemCode = FindViewById<TextView>(Resource.Id.txtItemCode);
                TextView c_txtItemName = FindViewById<TextView>(Resource.Id.txtItemName);
                TextView c_txtItemDescription = FindViewById<TextView>(Resource.Id.txtItemDescription);

                c_txtItemCode.Text = iObj.pCodigo;
                c_txtItemName.Text = iObj.nombre;
                c_txtItemDescription.Text = iObj.descripcion;

                //ListView c_lsvStock = FindViewById<ListView>(Resource.Id.lsvStock);
                //BaseAdapter.InventoryStockListViewAdapter adapter = new BaseAdapter.InventoryStockListViewAdapter(this, lObj);
                //c_lsvStock.Adapter = adapter;

                ListView c_lsvPrice = FindViewById<ListView>(Resource.Id.lsvPrice);
                BaseAdapter.PriceListViewAdapter pAdapter = new BaseAdapter.PriceListViewAdapter(this, lObj);
                c_lsvPrice.Adapter = pAdapter;

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                lObj = null; iObj = null;
            }
        }

        private void GenericHeaderLayout_Click(object sender, EventArgs e)
        {
            LinearLayout layout = (LinearLayout)sender;
            LinearLayout mLayout = null;
            TextView mTextView = null;

            switch (layout.Id)
            {
                case Resource.Id.StockHeaderLayout:
                    mLayout = FindViewById<LinearLayout>(Resource.Id.StockMainLayout);
                    mTextView = FindViewById<TextView>(Resource.Id.StockHeaderLabel);

                    ChangeCollapsiblePanelStateLinearLayout(mLayout, mTextView);

                    break;
                case Resource.Id.PriceHeaderLayout:
                    mLayout = FindViewById<LinearLayout>(Resource.Id.PriceMainLayout);
                    mTextView = FindViewById<TextView>(Resource.Id.PriceHeaderLabel);

                    ChangeCollapsiblePanelStateLinearLayout(mLayout, mTextView);

                    break;
                case Resource.Id.CreditHeaderLayout:
                    mLayout = FindViewById<LinearLayout>(Resource.Id.CreditMainLayout);
                    mTextView = FindViewById<TextView>(Resource.Id.CreditHeaderLabel);

                    ChangeCollapsiblePanelStateLinearLayout(mLayout, mTextView);

                    break;
                case Resource.Id.OtherHeaderLayout:
                    mLayout = FindViewById<LinearLayout>(Resource.Id.OtherMainLayout);
                    mTextView = FindViewById<TextView>(Resource.Id.OtherHeaderLabel);

                    ChangeCollapsiblePanelStateLinearLayout(mLayout, mTextView);

                    break;
                default:
                    break;
            }

        }//en void

        private void ChangeCollapsiblePanelStateLinearLayout(LinearLayout mLayout, TextView mTextView)
        {
            try
            {
                if (mLayout.Visibility == ViewStates.Gone || mLayout.Visibility == ViewStates.Invisible)
                {
                    mLayout.Visibility = ViewStates.Visible;

                    if (mTextView != null)
                    {
                        mTextView.SetBackgroundResource(Resource.Drawable.TextViewRedStyle);
                    }
                }
                else
                {
                    mLayout.Visibility = ViewStates.Gone;

                    if (mTextView != null)
                    {
                        mTextView.SetBackgroundResource(Resource.Drawable.TextViewGrayStyle);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }

        }
    }
}