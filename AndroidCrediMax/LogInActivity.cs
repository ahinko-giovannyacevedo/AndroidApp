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
    [Activity(Label = "@string/ApplicationName", NoHistory = true)]
    public class LogInActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LogInLayout);

            #region Declarar controles del formulario

            Button c_btnNumPad_Cero = FindViewById<Button>(Resource.Id.btnNumPad_Cero);
            c_btnNumPad_Cero.Click += ButtonNumPad_Click;

            Button c_btnNumPad_One = FindViewById<Button>(Resource.Id.btnNumPad_One);
            c_btnNumPad_One.Click += ButtonNumPad_Click;

            Button c_btnNumPad_Two = FindViewById<Button>(Resource.Id.btnNumPad_Two);
            c_btnNumPad_Two.Click += ButtonNumPad_Click;

            Button c_btnNumPad_Three = FindViewById<Button>(Resource.Id.btnNumPad_Three);
            c_btnNumPad_Three.Click += ButtonNumPad_Click;

            Button c_btnNumPad_Four = FindViewById<Button>(Resource.Id.btnNumPad_Four);
            c_btnNumPad_Four.Click += ButtonNumPad_Click;

            Button c_btnNumPad_Five = FindViewById<Button>(Resource.Id.btnNumPad_Five);
            c_btnNumPad_Five.Click += ButtonNumPad_Click;

            Button c_btnNumPad_Six = FindViewById<Button>(Resource.Id.btnNumPad_Six);
            c_btnNumPad_Six.Click += ButtonNumPad_Click;

            Button c_btnNumPad_Seven = FindViewById<Button>(Resource.Id.btnNumPad_Seven);
            c_btnNumPad_Seven.Click += ButtonNumPad_Click;

            Button c_btnNumPad_Eight = FindViewById<Button>(Resource.Id.btnNumPad_Eight);
            c_btnNumPad_Eight.Click += ButtonNumPad_Click;

            Button c_btnNumPad_Nine = FindViewById<Button>(Resource.Id.btnNumPad_Nine);
            c_btnNumPad_Nine.Click += ButtonNumPad_Click;

            Button c_btnConfirm = FindViewById<Button>(Resource.Id.btnConfirm);
            c_btnConfirm.Click += c_btnConfirm_Click;

            #endregion
        }

        private void c_btnConfirm_Click(object sender, EventArgs e)
        {
            EditText c_txtKeyWord = FindViewById<EditText>(Resource.Id.txtKeyWord);

            if (c_txtKeyWord.Text.Trim().Length < 4)
            {
                Toast.MakeText(this, "El codigo esta incompleto. Tiene que contener 4 digitos!", ToastLength.Short).Show();
                return;
            }
            else
            {
                Toast.MakeText(this, "Has Presionado Confirmar!", ToastLength.Short).Show();
            }

            //Llamar al servicio web para validar las credenciales
            //Toast.MakeText(this, string.Format("Mi AndroidID es: {0}", Utility.Utility.GetAndroidID()), ToastLength.Short).Show(); 
            StartActivity(typeof(VendorMainActivity));
        }

        private void ButtonNumPad_Click(object sender, EventArgs e) {
            EditText c_txtKeyWord = FindViewById<EditText>(Resource.Id.txtKeyWord);
            string num = ((Button)sender).Text;
            string code = c_txtKeyWord.Text.ToString().Trim().Replace("*","");

            if (code.Length < 4)
            {
                c_txtKeyWord.Text = string.Concat(code, num).PadLeft(4,'*');
            }
        }
    }
}