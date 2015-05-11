using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Widget;
using Android.Content;
using Android.Views;

namespace ahinko.android.credimax.BaseAdapter
{
    public class PriceListViewAdapter : BaseAdapter<DataContract.InventoryList>
    {
        public List<DataContract.InventoryList> _data;
        private Context _context;

        public PriceListViewAdapter(Context context, List<DataContract.InventoryList> data)
        {
            this._data = data;
            this._context = context;
        }

        public override DataContract.InventoryList this[int position]
        {
            get { return _data[position]; }
        }

        public override int Count
        {
            get { return _data.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row != null)
            {
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.PriceRow_Layout, null, false);
            }

            DataContract.InventoryList iObj = this._data[position];

            if (iObj != null)
            {
                TextView c_txtSucursal = row.FindViewById<TextView>(Resource.Id.txtSucursal);
                c_txtSucursal.Text = iObj.tienda.ToString();

                TextView c_txtPrecio = row.FindViewById<TextView>(Resource.Id.txtPrecio);
                c_txtPrecio.Text = iObj.precioLista.ToString();

                TextView c_txtPrecioEspecial = row.FindViewById<TextView>(Resource.Id.txtPrecioEspecial);
                c_txtPrecioEspecial.Text = iObj.precioOferta.ToString();

                TextView c_txtDescuento = row.FindViewById<TextView>(Resource.Id.txtDescuento);
                c_txtDescuento.Text = iObj.descuentoMaximo.ToString();
            }

            return row;
        }
    }
}