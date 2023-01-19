using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMIP
{
    public class Connection
    {

        private string serverName;
        private string databaseName;
        private string connectionString = null;
        private SqlConnection con;

        public Connection(String ServerName, String DatabaseName)
        {
            this.serverName = ServerName;
            //this.password = Password;
            this.databaseName = DatabaseName;
            //this.userName = UserName;

            //SqlConnection cnn;

            //connetionString = @"Data Source=" +serverName + ";Initial Catalog="+databaseName+";User ID="+ userName +";Password="+password+ @"";
            connectionString = "Server=" + serverName + ";Integrated Security=true;Database=" + databaseName + ";multipleactiveresultsets=True";
            //OpenConection();

            con = new SqlConnection(connectionString);

          
        }

        public void OpenConection()
        {
            try
            {
                con.Open();
                //System.Windows.Forms.MessageBox.Show("Connection Open ! ");
                //cnn.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Can not open connection ! ");
            }
        }



        public void CloseConnection()
        {
            con.Close();
        }


        public DataTable SelectQuery(string tableName, List<string> searchParamList, List<string> filterParamList)
        {
            if (tableName == null || tableName == "")
                return null;

            string sql = "select ";
            string searchParamString = "";
            string filterParamString = "";

            if (searchParamList != null && searchParamList.Count > 0)
            {
                for (int i = 0; i < searchParamList.Count; i++)
                {
                    searchParamString += searchParamList[i];
                    if (i < searchParamList.Count - 1)
                        searchParamString += ",";
                }
            }
            else
            {
                searchParamString = "*";
            }

            sql += searchParamString + " from " + tableName;

            if (filterParamList != null && filterParamList.Count > 0)
            {
                for (int i = 0; i < filterParamList.Count; i++)
                {
                    filterParamString += filterParamList[i];
                    if (i < filterParamList.Count - 1)
                        filterParamString += ",";
                }
            }

            if (filterParamString != "")
                sql += " where " + filterParamString;

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Connection.Open();
            new SqlDataAdapter(cmd).Fill(ds);
            con.Close();
            return ds.Tables[0];

        }
        public void InsertBlobQuery(string tableName, List<string> tableFieldsList, string blobPath)
        {
            if (tableName == null || tableName == "" || blobPath == null || blobPath == "" || tableFieldsList == null || tableFieldsList.Count == 0)
                return;

            string tableFieldsString = "";

            for (int i = 0; i < tableFieldsList.Count; i++)
            {
                tableFieldsString += tableFieldsList[i];
                if (i < tableFieldsList.Count - 1)
                    tableFieldsString += ",";
            }

            try
            {

                string fileName = blobPath.Substring(blobPath.LastIndexOf(@"\") + 1, blobPath.LastIndexOf(@".") - blobPath.LastIndexOf(@"\") - 1);

                SqlCommand cmd = new SqlCommand("INSERT INTO " + tableName + " (" + tableFieldsString + ") VALUES (@fileName,@blobData)", con);

                //Read jpg into file stream, and from there into Byte array.
                FileStream fsBLOBFile = new FileStream(blobPath, FileMode.Open, FileAccess.Read);
                Byte[] bytBLOBData = new Byte[fsBLOBFile.Length];
                fsBLOBFile.Read(bytBLOBData, 0, bytBLOBData.Length);
                fsBLOBFile.Close();

                //Create parameter for insert command and add to SqlCommand object.
                SqlParameter prm1 = new SqlParameter("@fileName", fileName);
                SqlParameter prm2 = new SqlParameter("@blobData", SqlDbType.VarBinary, bytBLOBData.Length, ParameterDirection.Input, false,
                0, 0, null, DataRowVersion.Current, bytBLOBData);
                cmd.Parameters.Add(prm1);
                cmd.Parameters.Add(prm2);

                //Open connection, execute query, and close connection.
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Failed to insert Blob! " + ex.Message);
            }
        }

        public void DeleteQuery(string tableName, List<string> filterParamList)
        {
            if (tableName == null || tableName == "")
                return;

            string filterParamString = "";
            string sql = "delete from " + tableName;

            if (filterParamList != null && filterParamList.Count > 0)
            {
                for (int i = 0; i < filterParamList.Count; i++)
                {
                    filterParamString += filterParamList[i];
                    if (i < filterParamList.Count - 1)
                        filterParamString += ",";
                }
            }

            if (filterParamString != "")
                sql += " where " + filterParamString;


            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
