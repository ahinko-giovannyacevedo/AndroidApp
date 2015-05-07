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

namespace ahinko.android.credimax.BaseAdapter
{
    public class InventoryStockListViewAdapter : BaseAdapter<DataContract.InventoryList>
    {
        public List<DataContract.InventoryList> _data;
        private Context _context;

        public InventoryStockListViewAdapter(Context context, List<DataContract.InventoryList> data) {
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

            if (row == null)
            {
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.StockRow_Layout,null, false);
            }

            DataContract.InventoryList iObj = this._data[position];

            if (iObj != null)
            {
                //Controles
                TextView c_txtWareHouse = row.FindViewById<TextView>(Resource.Id.txtWareHouse);
                c_txtWareHouse.Text = iObj.bodega.ToString();

                TextView c_txtStock = row.FindViewById<TextView>(Resource.Id.txtStock);
                c_txtStock.Text = "50";//TODO: Todavia no viene en el servicio
            }

            return row;
        }
    }
}