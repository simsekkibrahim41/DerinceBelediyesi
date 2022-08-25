using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;//ekle
using System.Data;//ekle

namespace DerinceBelediyesi.DAL
{
    class accessconnection
    {//bağlantimi ayarladığım class
        private readonly string _connecLionString;
        public accessconnection()
        {
            _connecLionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Staj.accdb";
            // access bağlantı yolu 
        }
        private OleDbConnection GetOleDbConnection()
        {
            OleDbConnection cnn = new OleDbConnection(_connecLionString);
            if (cnn.State == ConnectionState.Open)
            {
                cnn.Close();
                cnn.Open();
            }
            else
            {
                cnn.Open();
            }
            return cnn;
        }
        public OleDbCommand GetOleDbCommand()
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = GetOleDbConnection();
            return cmd;
        }

    }
}
