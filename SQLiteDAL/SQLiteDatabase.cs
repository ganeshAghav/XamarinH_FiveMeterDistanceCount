﻿using AppInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDAL
{
    class SQLiteDatabase
    {
        String dbConnection;
        private DBPlatformInfo dbPlatform = null;
        ///     Default Constructor for SQLiteDatabase Class.

        public SQLiteDatabase(DBPlatformInfo dbPlatform)
        {
            //dbConnection = "Data Source=recipes.s3db";
            this.dbPlatform = dbPlatform;
            dbConnection = dbPlatform.SQLDBPath;
        }



        // Single Param Constructor for specifying the DB file.
        /// <param name="inputFile">The File containing the DB</param>

        //public SQLiteDatabase(String inputFile)

        //{

        //    dbConnection = String.Format("Data Source={0}", inputFile);

        //}



        /// <summary>

        ///     Single Param Constructor for specifying advanced connection options.

        /// </summary>

        /// <param name="connectionOpts">A dictionary containing all desired options and their values</param>

        public SQLiteDatabase(Dictionary<String, String> connectionOpts)
        {

            String str = "";

            foreach (KeyValuePair<String, String> row in connectionOpts)
            {

                str += String.Format("{0}={1}; ", row.Key, row.Value);

            }

            str = str.Trim().Substring(0, str.Length - 1);

            dbConnection = str;

        }



        /// <summary>

        ///     Allows the programmer to run a query against the Database.

        /// </summary>

        /// <param name="sql">The SQL to run</param>

        /// <returns>A DataTable containing the result set.</returns>

        //public DataTable GetDataTable(string sql)

        //{

        //    DataTable dt = new DataTable();

        //    try

        //    {

        //        SQLiteConnection cnn = new SQLiteConnection(dbConnection); 
        //        cnn.Open();

        //        SQLiteCommand mycommand = new SQLiteCommand(cnn);

        //        mycommand.CommandText = sql;

        //        SQLiteDataReader reader = mycommand.ExecuteReader();

        //        dt.Load(reader);

        //        reader.Close();

        //        cnn.Close();

        //    }

        //    catch (Exception e)

        //    {

        //        throw new Exception(e.Message);

        //    }

        //    return dt;

        //}



        /// <summary>

        ///     Allows the programmer to interact with the database for purposes other than a query.

        /// </summary>

        /// <param name="sql">The SQL to be run.</param>

        /// <returns>An Integer containing the number of rows updated.</returns>

        public int ExecuteNonQuery(string sql)
        {
            int retuValu = 0;
            try
            {

                using (var commonDAL = new CommonDAL(dbPlatform))
                {
                    using (var sqliteConnection = commonDAL.GetSQLiteConnection())
                    {
                        using (var db = sqliteConnection)
                        {

                            retuValu = db.Execute(sql);


                            db.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return retuValu;
        }



        /// <summary>

        ///     Allows the programmer to retrieve single items from the DB.

        /// </summary>

        /// <param name="sql">The query to run.</param>
        /// <returns>A string.</returns>

        //public string ExecuteScalar(string sql)
        //{ 
        //SQLiteConnection cnn = new SQLiteConnection(dbConnection);
        //cnn.Open();
        //SQLiteCommand mycommand = new SQLiteCommand(cnn);
        //mycommand.CommandText = sql;
        //object value = mycommand.ExecuteScalar();
        //cnn.Close();
        //if (value != null)
        //{
        //    return value.ToString();
        //}
        //return "";
        // }



        /// <summary>

        ///     Allows the programmer to easily update rows in the DB.

        /// </summary>

        /// <param name="tableName">The table to update.</param>

        /// <param name="data">A dictionary containing Column names and their new values.</param>

        /// <param name="where">The where clause for the update statement.</param>

        /// <returns>A boolean true or false to signify success or failure.</returns>

        public bool Update(String tableName, Dictionary<String, String> data, String where, String whereval)
        {
            bool res = false;
            try
            {
                String vals = "";
                int returnCode = 0;
                if (data.Count >= 1)
                {
                    foreach (KeyValuePair<String, String> val in data)
                    {
                        vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value);
                    }
                    vals = vals.Substring(0, vals.Length - 1);
                }
                try
                {
                    returnCode = this.ExecuteNonQuery(String.Format("update {0} set {1} where {2} = {3} ;", tableName, vals, where, whereval));
                }
                catch
                {
                    returnCode = 0;
                }

                if (returnCode == 1)
                {
                    res = true;
                }
                else if (returnCode == 0)
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;

        }

        /// <summary>

        ///     Allows the programmer to easily delete rows from the DB.

        /// </summary>

        /// <param name="tableName">The table from which to delete.</param>

        /// <param name="where">The where clause for the delete.</param>

        /// <returns>A boolean true or false to signify success or failure.</returns>

        public bool Delete(String tableName, String where)
        {

            Boolean returnCode = true;

            try
            {

                this.ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));

            }

            catch (Exception fail)
            {

                //  MessageBox.Show(fail.Message);

                returnCode = false;

            }

            return returnCode;

        }
        public bool Deletescript(String qr)
        {

            Boolean returnCode = true;

            try
            {

                this.ExecuteNonQuery(qr);

            }

            catch (Exception fail)
            {

                //  MessageBox.Show(fail.Message);

                returnCode = false;

            }

            return returnCode;

        }


        /// <summary>

        ///     Allows the programmer to easily insert into the DB

        /// </summary>

        /// <param name="tableName">The table into which we insert the data.</param>

        /// <param name="data">A dictionary containing the column names and data for the insert.</param>

        /// <returns>A boolean true or false to signify success or failure.</returns>

        public bool Insert(String tableName, Dictionary<String, String> data)
        {
            bool res = false;
            try
            {
                String columns = "";
                String values = "";
                int returnCode = 0;

                foreach (KeyValuePair<String, String> val in data)
                {
                    columns += String.Format(" {0},", val.Key.ToString());
                    values += String.Format(" '{0}',", val.Value);
                }
                columns = columns.Substring(0, columns.Length - 1);
                values = values.Substring(0, values.Length - 1);

                try
                {
                    returnCode = this.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
                }

                catch (Exception fail)
                {


                    // MessageBox.Show(fail.Message);

                    returnCode = 0;

                }

                if (returnCode == 1)
                {
                    res = true;
                }
                else if (returnCode == 0)
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;

        }



        /// <summary>

        ///     Allows the programmer to easily delete all data from the DB.

        /// </summary>

        /// <returns>A boolean true or false to signify success or failure.</returns>

        //public bool ClearDB()

        //{

        //    DataTable tables;

        //    try

        //    {

        //        tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");

        //        foreach (DataRow table in tables.Rows)

        //        {

        //            this.ClearTable(table["NAME"].ToString());

        //        }

        //        return true;

        //    }

        //    catch

        //    {

        //        return false;

        //    }

        //}



        /// <summary>

        ///     Allows the user to easily clear all data from a specific table.

        /// </summary>

        /// <param name="table">The name of the table to clear.</param>

        /// <returns>A boolean true or false to signify success or failure.</returns>

        //public bool ClearTable(String table)

        //{

        //    try

        //    {



        //        this.ExecuteNonQuery(String.Format("delete from {0};", table));

        //        return true;

        //    }

        //    catch

        //    {

        //        return false;

        //    }

        //}     
    }
}
