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

namespace ahinko.android.credimax.Utility
{
    public static class Utility
    {
        /// <summary>
        /// Retorna el Identificador unico del dispositivo.
        /// </summary>
        /// <returns></returns>
        public static string GetAndroidID() {
            string m_androidID = string.Empty;
            try
            {
                m_androidID = Android.Provider.Settings.Secure.GetString(Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            }
            catch (Exception)
            {
                throw;
            }

            return m_androidID;
        }
    }
}