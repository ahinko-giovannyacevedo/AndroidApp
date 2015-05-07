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
using System.ServiceModel;

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

        public static BasicHttpBinding GetBasicHttpBinding()
        {
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

            return binding;
        }
    }
}