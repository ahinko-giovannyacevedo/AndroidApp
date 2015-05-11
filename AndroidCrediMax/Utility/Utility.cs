using Android.App;
using System;
using System.Data;
using System.IO;
using System.ServiceModel;

namespace ahinko.android.credimax.Utility
{
    public static class AUtility
    {
        public static readonly string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormcredimax.db3");

        public static int RequestID { get; set; }

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

        /// <summary>
        /// Obtiene un objeto del Tipo BasicHttpBinding para configuracion de servicios en WCF
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Verifica si una cadena es un numero decimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDecimal(string value){
            Decimal outResult;
            return decimal.TryParse(value, out outResult);
        }
    }
}