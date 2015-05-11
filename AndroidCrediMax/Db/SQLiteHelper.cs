using System;
using System.Data;
using System.IO;

using SQLite;

namespace ahinko.android.credimax.Db
{
    public class SQLiteHelper
    {
        private SQLiteConnection db { get; set; }
        public SQLiteHelper() {
            db = new SQLiteConnection(Utility.AUtility.DbPath);
        }

        public void InitializeDataBase() {
            //Crear las tablas
            db.CreateTable<Request>();
            db.CreateTable<RequestDetail>();
        }

        #region Tabla Request
        public Request GetRequestByID(int requestID) {
            Request req = null;
            try
            {
                req = db.Get<Request>(requestID);
            }
            catch (Exception)
            {
                throw;
            }
            return req;
        }

        public int SaveRequest(Request rq) {
            int autoID = 0;
            try
            {
                autoID = db.Insert(rq);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return autoID;
        }

        #endregion

        #region Tabla RequestDetail

        public RequestDetail GetRequestDetailByPK(int pk) {
            RequestDetail det = null;

            try
            {
                det = db.Get<RequestDetail>(pk);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return det;
        }

        public RequestDetail GetRequestDetailByFK(int requestID, string itemnumber) {
            RequestDetail det = null;

            try
            {
                det = db.Find<RequestDetail>(x=> x.RequestID == requestID && x.ItemNumber == itemnumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return det;
        }

        public int InsertRequestDetail(RequestDetail det) {
            int result = 0;
            try
            {
                result = db.Insert(det);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int UpdateRequestDetail(RequestDetail det) {
            int result = 0;
            try
            {
                result = db.Update(det);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }
}