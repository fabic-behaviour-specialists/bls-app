using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;

namespace BLS.Cloud.Database
{
    public class ExternalDatabase
    {
        public static ExternalDatabase InternalDatabase;
        #region Variables
        #region " Access Related "
        public static OleDbConnection DBCon;
        public static OleDbDataAdapter DBCmd;
        public static OleDbCommandBuilder DBCmdBuilder;
        public static OleDbDataReader Reader;
        public static OleDbCommand DBCmdExecuter = new OleDbCommand();
        #endregion
        #region " SQL Server Related "
        public SqlConnection SQLCon;
        public SqlDataAdapter SQLCmd;
        public SqlCommandBuilder SQLCmdBuilder;
        public SqlDataReader SQLReader;
        public SqlCommand SQLCmdExecuter = new SqlCommand();
        #endregion
        private bool bAllowClose = true;
        private static string sPassword;
        private bool _Connected = false;
        private string _Err;
        #endregion
        #region Properties
        public DataSet DS
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string DBConnectionString
        {
            get;
            set;
        }

        #region SQL Properties
        public SqlConnection SQLConnection
        {
            get { return SQLCon; }
        }

        public string SQLServer
        {
            get;
            set;
        }

        public string SQLCatalog
        {
            get;
            set;
        }

        public string SQLUsername
        {
            get;
            set;
        }

        public string SQLPassword
        {
            get;
            set;
        }

        public bool ForWeb
        {
            get;
            set;
        }


        public string ErrorDescription
        {
            get { return _Err; }
        }

        #endregion

        public ExternalDatabase(OleDbConnection DBConnection = null, string Password = "__BLANK__")
        {
            DBCon = DBConnection;
            if ((DBConnection != null))
                bAllowClose = false;
            if (Password != "__BLANK__")
                sPassword = Password;
        }

        #region Database Connection

        private bool initiateDatabaseConnection()
        {
            try
            {
                SQLCon = new SqlConnection(DBConnectionString);
                if (SQLCon.State != System.Data.ConnectionState.Open)
                    SQLCon.Open();
                if (SQLCon == null || SQLCon.State != System.Data.ConnectionState.Open)
                {
                    Exception ex = new Exception("Database connection failed! (" + Name + ")" + "Connection is Empty");

                    _Err = ex.Message;
                    return false;
                }
                else
                {
                    _Connected = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _Err = ex.Message;
                return false;
            }
            return true;
        }

        public bool OpenDatabase(string dbConnectionString)
        {
            bool functionReturnValue = false;

            DBConnectionString = dbConnectionString;

            string DBConStr = DBConnectionString;

            return initiateDatabaseConnection();

            return functionReturnValue;
        }


        public bool OpenDatabase(string sqlserver, string sqldatabase, string sqluser, string sqlpassword)
        {
            bool functionReturnValue = false;
            SQLServer = sqlserver;
            SQLCatalog = sqldatabase;
            SQLUsername = sqluser;
            SQLPassword = sqlpassword;

            var sqlBuilder = new SqlConnectionStringBuilder();

            if (string.IsNullOrEmpty(SQLUsername))
            {
                // Set the properties for the data source.
                sqlBuilder.DataSource = SQLServer;
                sqlBuilder.InitialCatalog = SQLCatalog;
                sqlBuilder.IntegratedSecurity = true;
                sqlBuilder.MultipleActiveResultSets = true;
            }
            else
            {
                sqlBuilder.DataSource = SQLServer;
                sqlBuilder.InitialCatalog = SQLCatalog;
                sqlBuilder.IntegratedSecurity = false;
                sqlBuilder.UserID = SQLUsername;
                sqlBuilder.Password = SQLPassword;
                sqlBuilder.MultipleActiveResultSets = true;
            }

            DBConnectionString = sqlBuilder.ToString();
            return initiateDatabaseConnection();

            return functionReturnValue;
        }
        #endregion

        public void SaveChanges(string tblName, DataSet DS = null)
        {
            //if (DS == null)
            //{
            //    if (Table == null)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        DS = Table.DataSet;
            //    }
            //}
            //DataSet nDS = DS.GetChanges();
            //if ((nDS != null))
            //{
            //    if (DBType == DatabaseType.Access)
            //    {
            //        DBCmd.Update(nDS, tblName);
            //    }
            //    else if (DBType == DatabaseType.SQLServer)
            //    {
            //        SQLCmd.Update(nDS, tblName);
            //    }
            //}
        }

        public void SelectLastRow(string tblName, string PrimKey = "fldID")
        {
            ExecuteSQL("SELECT * FROM " + tblName + " WHERE " + PrimKey + " = (SELECT MAX(" + PrimKey + ") FROM " + tblName + ")", tblName);
        }

        public void GetTable(string sTable, bool bSilent = false, bool bForceReload = true)
        {
            //if (!bForceReload)
            //{
            //    if ((Table != null))
            //    {
            //        if (Table.TableName.ToLower() == sTable.ToLower())
            //        {
            //            // If Not MainForm Is Nothing Then MainForm.DebugMsg("DB(" & _Name & ") Get Table Skipped, Table Already Loaded (" & sTable & ")")
            //            return;
            //        }
            //    }
            //}

            //if (DBType == DatabaseType.Access)
            //{
            //    if ((Reader != null)) { Reader.Close(); Reader = null; }
            //    if ((DBCmd != null)) { DBCmd.Dispose(); DBCmd = null; }
            //    if (bSilent)
            //    {
            //        DBCmd = new OleDbDataAdapter("SELECT * FROM " + sTable, DBCon);
            //        DBCmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //        DBCmdBuilder = new OleDbCommandBuilder(DBCmd);
            //        DS = new DataSet();
            //        //DBCmd.AcceptChangesDuringFill = True
            //        DBCmd.Fill(DS, sTable);
            //        Table = DS.Tables[0];
            //        Table.TableName = sTable;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            DBCmd = new OleDbDataAdapter("SELECT * FROM " + sTable, DBCon);
            //            DBCmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //            DBCmdBuilder = new OleDbCommandBuilder(DBCmd);
            //            DS = new DataSet();
            //            DBCmd.Fill(DS, sTable);
            //            Table = DS.Tables[0];
            //            Table.TableName = sTable;
            //        }
            //        catch (Exception ex)
            //        {
            //            ex.HandleMonitorException("Getting Table in external DB");
            //            CloseDatabase();
            //            initiateDatabaseConnection();
            //        }
            //    }

            //}
            //else if (DBType == DatabaseType.SQLServer)
            //{
            //    if ((SQLReader != null)) { SQLReader.Close(); SQLReader = null; }
            //    if ((SQLCmd != null)) { SQLCmd.Dispose(); SQLCmd = null; }
            //    if (bSilent)
            //    {
            //        SQLCmd = new SqlDataAdapter("SELECT * FROM " + sTable, SQLCon);
            //        SQLCmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //        SQLCmdBuilder = new SqlCommandBuilder(SQLCmd);
            //        DS = new DataSet();
            //        //DBCmd.AcceptChangesDuringFill = True
            //        SQLCmd.Fill(DS, sTable);
            //        Table = DS.Tables[0];
            //        Table.TableName = sTable;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            SQLCmd = new SqlDataAdapter("SELECT * FROM " + sTable, SQLCon);
            //            SQLCmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //            SQLCmdBuilder = new SqlCommandBuilder(SQLCmd);
            //            DS = new DataSet();
            //            SQLCmd.Fill(DS, sTable);
            //            Table = DS.Tables[0];
            //            Table.TableName = sTable;
            //        }
            //        catch (Exception ex)
            //        {
            //            ex.HandleMonitorException("Getting Table from external db");

            //            CloseDatabase();
            //            initiateDatabaseConnection();
            //        }
            //    }
            //}
        }
        /// <summary>
        /// Executes an SQL query on an external database
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="tblName"></param>
        /// <param name="MaxRows"></param>
        /// <returns></returns>
        public DataTable ExecuteSQL(string sSQL, string tblName = "", int MaxRows = -1)
        {
            try
            {
                if ((SQLReader != null)) { SQLReader.Close(); SQLReader = null; }
                if ((SQLCmd != null)) { SQLCmd.Dispose(); SQLCmd = null; }
                SQLCmd = new SqlDataAdapter(sSQL, SQLCon);
                SQLCmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                SQLCmdBuilder = new SqlCommandBuilder(SQLCmd);
                DS = new DataSet();
                if (!string.IsNullOrEmpty(tblName))
                {
                    if (MaxRows != -1)
                    {
                        SQLCmd.Fill(DS, 0, MaxRows, tblName);
                    }
                    else
                    {
                        SQLCmd.Fill(DS, tblName);
                    }
                }
                else
                {
                    if (MaxRows != -1)
                    {
                        SQLCmd.Fill(DS, 0, MaxRows, tblName);
                    }
                    else
                    {
                        SQLCmd.Fill(DS);
                    }
                }

                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0] != null)
                        return DS.Tables[0];
                }
            }
            catch (Exception ex)
            {
                CloseDatabase();
                initiateDatabaseConnection();
            }
            return null;
        }

        public bool ExecuteReader(string sSQL)
        {
            bool functionReturnValue = false;
            try
            {
                if ((SQLReader != null)) { SQLReader.Close(); SQLReader = null; }
                if ((SQLCmd != null)) { SQLCmd.Dispose(); SQLCmd = null; }
                int iEnd = 0;
                iEnd = sSQL.IndexOf(" ");
                if (iEnd > 0)
                {
                    string sType = null;
                    sType = sSQL.Substring(0, iEnd).ToUpper();
                    switch (sType)
                    {
                        case "INSERT":
                        case "UPDATE":
                        case "CREATE":
                        case "DELETE":
                        case "ALTER":
                            SqlCommand SQLDBCmd = new SqlCommand(sSQL, SQLCon);
                            SQLDBCmd.ExecuteNonQuery();
                            SQLDBCmd.Dispose();
                            SQLDBCmd = null;

                            break;
                        default:
                            SQLDBCmd = new SqlCommand(sSQL, SQLCon);
                            SQLReader = SQLDBCmd.ExecuteReader();
                            break;
                    }
                }
                functionReturnValue = true;
            }
            catch (Exception ex)
            {
                CloseDatabase();
                initiateDatabaseConnection();
            }
            return functionReturnValue;
        }

        public int FillModelDS(ref System.Data.DataSet DDS, System.Data.OleDb.OleDbParameter[] Parameters, string sSQL, System.Data.CommandType CommandType, string IDField = "fldID", System.Data.Common.DataColumnMapping[] Mappings = null)
        {
            return 0;
            //try
            //{
            //    Retry:
            //    string sParams = string.Empty;
            //    if (DBType == DatabaseType.Access)
            //    {
            //        if ((Reader != null))
            //            Reader.Close();
            //        if ((DBCmd != null)) { DBCmd.Dispose(); DBCmd = null; }
            //        DDS.EnforceConstraints = false;

            //        DBCmd = new OleDbDataAdapter(sSQL, DBCon);
            //        DBCmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //        DBCmdBuilder = new OleDbCommandBuilder(DBCmd);
            //        Table = null;
            //        DS = null;

            //        //- Set the Table Mappings For The Specified Model Type
            //        DBCmd.TableMappings.Clear();
            //        DBCmd.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] { new System.Data.Common.DataTableMapping(nameof(Table), "Activities", new System.Data.Common.DataColumnMapping[] { new System.Data.Common.DataColumnMapping(IDField, "ID") }) });
            //        if ((Mappings != null))
            //            DBCmd.TableMappings[0].ColumnMappings.AddRange(Mappings);

            //        DBCmd.AcceptChangesDuringFill = false;
            //        DBCmd.SelectCommand.CommandType = CommandType;
            //        DBCmd.SelectCommand.CommandText = sSQL;
            //        DBCmd.SelectCommand.Parameters.Clear();
            //        if ((Parameters != null))
            //        {
            //            for (int i = 0; i <= Information.UBound(Parameters); i++)
            //            {
            //                DBCmd.SelectCommand.Parameters.Add(Parameters[i]);
            //                sParams += ", " + Parameters[i].ParameterName + " = " + Parameters[i].Value;
            //            }
            //        }
            //        DBCmd.Fill(DDS);

            //    }
            //    else if (DBType == DatabaseType.SQLServer)
            //    {
            //        if ((SQLReader != null))
            //            SQLReader.Close();
            //        if ((SQLCmd != null)) { SQLCmd.Dispose(); SQLCmd = null; }
            //        DDS.EnforceConstraints = false;

            //        SQLCmd = new SqlDataAdapter(sSQL, SQLCon);
            //        SQLCmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            //        SQLCmdBuilder = new SqlCommandBuilder(SQLCmd);
            //        Table = null;
            //        DS = null;

            //        //- Set the Table Mappings For The Specified Model Type
            //        SQLCmd.TableMappings.Clear();
            //        SQLCmd.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] { new System.Data.Common.DataTableMapping(nameof(Table), "Activities", new System.Data.Common.DataColumnMapping[] { new System.Data.Common.DataColumnMapping(IDField, "ID") }) });
            //        if ((Mappings != null))
            //            DBCmd.TableMappings[0].ColumnMappings.AddRange(Mappings);

            //        SQLCmd.AcceptChangesDuringFill = false;
            //        SQLCmd.SelectCommand.CommandType = CommandType;
            //        SQLCmd.SelectCommand.CommandText = sSQL;
            //        SQLCmd.SelectCommand.Parameters.Clear();
            //        if ((Parameters != null))
            //        {
            //            for (int i = 0; i <= Information.UBound(Parameters); i++)
            //            {
            //                SQLCmd.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(Parameters[i].ParameterName, Parameters[i].Value));
            //                sParams += ", " + Parameters[i].ParameterName + " = " + Parameters[i].Value;
            //            }
            //        }
            //        SQLCmd.Fill(DDS);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.HandleMonitorException("Filling DataSet in external db");
            //    CloseDatabase();
            //    initiateDatabaseConnection();
            //}
            //return 0;
        }

        public DataTable ExecuteQuery(string QueryName, System.Data.OleDb.OleDbParameter[] Parameters = null)
        {
            try
            {
                string sParams = string.Empty;
                if ((SQLReader != null))
                    SQLReader.Close();
                if ((SQLCmd != null)) { SQLCmd.Dispose(); SQLCmd = null; }

                SQLCmd = new SqlDataAdapter(QueryName, SQLCon);
                SQLCmd.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                SQLCmdBuilder = new SqlCommandBuilder(SQLCmd);
                DS = null;

                SQLCmd.AcceptChangesDuringFill = false;
                SQLCmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                SQLCmd.SelectCommand.CommandText = QueryName;
                SQLCmd.SelectCommand.Parameters.Clear();
                if ((Parameters != null))
                {
                    for (int i = 0; i <= Parameters.Count(); i++)
                    {
                        SQLCmd.SelectCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter(Parameters[i].ParameterName, Parameters[i].Value));
                        if (object.ReferenceEquals(Parameters[i].Value.GetType(), typeof(string)))
                        {
                            sParams += ", " + Parameters[i].ParameterName + " = " + Parameters[i].Value;
                        }
                        else
                        {
                            sParams += ", " + Parameters[i].ParameterName + " = " + Parameters[i].Value.GetType().ToString();
                        }
                    }
                }
                DS = new DataSet();
                SQLCmd.Fill(DS);

                if (DS.Tables.Count > 0)
                {
                    return DS.Tables[0];
                }
            }
            catch (Exception ex)
            {
                CloseDatabase();
                initiateDatabaseConnection();
            }
            return null;
        }

        public int ExecuteUpdateQuery(string QueryName, System.Data.OleDb.OleDbParameter[] Parameters = null)
        {
            try
            {
                Retry:
                string sParams = string.Empty;
                if ((SQLReader != null))
                    SQLReader.Close();
                if ((SQLCmd != null)) { SQLCmd.Dispose(); SQLCmd = null; }

                SQLCmdExecuter.CommandType = CommandType.StoredProcedure;
                SQLCmdExecuter.CommandText = QueryName;
                SQLCmdExecuter.Connection = SQLCon;
                SQLCmdExecuter.Parameters.Clear();
                if ((Parameters != null))
                {
                    for (int i = 0; i <= Parameters.Count(); i++)
                    {
                        if (Parameters[i].Value.GetType().ToString() == "System.Byte[]")
                        {
                            System.Data.SqlClient.SqlParameter Param = SQLCmdExecuter.Parameters.Add(new System.Data.SqlClient.SqlParameter(Parameters[i].ParameterName, SqlDbType.Image, ((Byte[])Parameters[i].Value).Length));
                            Param.Value = Parameters[i].Value;

                            sParams += ", " + Parameters[i].ParameterName + " = " + Parameters[i].Value.GetType().ToString();
                        }
                        else
                        {
                            SQLCmdExecuter.Parameters.Add(new System.Data.SqlClient.SqlParameter(Parameters[i].ParameterName, Parameters[i].Value));
                            sParams += ", " + Parameters[i].ParameterName + " = " + Parameters[i].Value;
                        }
                    }
                }
                return SQLCmdExecuter.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CloseDatabase();
                initiateDatabaseConnection();
            }
            return 0;
        }

        public object ExecuteScalarQuery(string QueryName, System.Data.OleDb.OleDbParameter[] Parameters = null)
        {
            try
            {
                Retry:
                string sParams = string.Empty;
                if ((SQLReader != null))
                    SQLReader.Close();
                if ((SQLCmd != null)) { SQLCmd.Dispose(); SQLCmd = null; }

                SQLCmdExecuter.CommandType = CommandType.StoredProcedure;
                SQLCmdExecuter.CommandText = QueryName;
                SQLCmdExecuter.Connection = SQLCon;
                SQLCmdExecuter.Parameters.Clear();
                if ((Parameters != null))
                {
                    for (int i = 0; i <= Parameters.Length; i++)
                    {
                        SQLCmdExecuter.Parameters.Add(new System.Data.SqlClient.SqlParameter(Parameters[i].ParameterName, Parameters[i].Value));
                        if (object.ReferenceEquals(Parameters[i].Value.GetType(), typeof(string)))
                        {
                            sParams += ", " + Parameters[i].ParameterName + " = " + Parameters[i].Value;
                        }
                        else
                        {
                            sParams += ", " + Parameters[i].ParameterName + " = " + Parameters[i].Value.GetType().ToString();
                        }
                    }
                }
                return SQLCmdExecuter.ExecuteScalar();
            }
            catch (Exception ex)
            {
                CloseDatabase();
                initiateDatabaseConnection();
            }
            return null;
        }

        public void CloseDatabase()
        {
            if ((DS != null))
            {
                DS.Tables.Clear();
                DS.Dispose();
                DS = null;
            }
            if ((DBCon != null))
            {
                if ((Reader != null))
                {
                    Reader.Close();
                    Reader = null;
                }
                if ((DBCmdBuilder != null))
                {
                    DBCmdBuilder.Dispose();
                    DBCmdBuilder = null;
                }
                if ((DBCmd != null))
                {
                    DBCmd.Dispose();
                    DBCmd = null;
                }
                if (bAllowClose)
                {
                    if ((DBCon != null))
                        DBCon.Close();
                }
                _Connected = false;
            }
            if ((SQLCon != null))
            {
                if ((SQLReader != null))
                {
                    SQLReader.Close();
                    SQLReader = null;
                }
                if ((SQLCmdBuilder != null))
                {
                    SQLCmdBuilder.Dispose();
                    SQLCmdBuilder = null;
                }
                if ((SQLCmd != null))
                {
                    SQLCmd.Dispose();
                    SQLCmd = null;
                }
                if (bAllowClose)
                {
                    if ((SQLCon != null))
                        SQLCon.Close();
                }
                _Connected = false;
            }
        }

        public int RowCount
        {
            get
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0] != null)
                        return DS.Tables[0].Rows.Count;
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
        }
        #endregion
    }
}
