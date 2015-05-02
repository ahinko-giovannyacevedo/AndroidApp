using System;
using System.Data;
using System.IO;

using SQLite;

namespace ahinko.android.credimax.Db
{
    public class SQLiteHelper
    {
        private string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormcredimax.db3");

        private string DbPath
        {
            get { return _dbPath; }
            //set { SQLiteHelper._dbPath = value; }
        }

        private SQLiteConnection _db;

        public SQLiteConnection Db
        {
            get { return _db; }
            //set { SQLiteHelper._db = value; }
        }

        public void InitializeDataBase() {
            _db = new SQLiteConnection(DbPath);
            //Crear las tablas
        }

    }
}