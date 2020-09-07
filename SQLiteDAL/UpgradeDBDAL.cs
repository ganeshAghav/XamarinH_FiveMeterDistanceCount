using AppInfo;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDAL
{
    public class UpgradeDBDAL
    {
        private DBPlatformInfo dbPlatform = null;

        public UpgradeDBDAL(DBPlatformInfo dbPlatform)
        {
            this.dbPlatform = dbPlatform;
        }

        //public int GetDBVersion()
        //{
        //    int DbVer = 0;
        //    CommonDAL commonDAL = null;
        //    DBVersion dbversion = null;
        //    SQLiteConnection sqliteConnection = null;
        //    try
        //    {
        //        commonDAL = new CommonDAL(dbPlatform);
        //        sqliteConnection = commonDAL.GetSQLiteConnection();
        //        using (var db = sqliteConnection)
        //        {
        //            dbversion = db.Query<DBVersion>("Select * from DBVersion", "").FirstOrDefault();
        //            if (dbversion.VersionNo > 0)
        //            {
        //                DbVer = dbversion.VersionNo;
        //            }

        //            db.Commit();
        //            db.Dispose();
        //            db.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //  ;
        //    }
        //    finally
        //    {
        //        sqliteConnection = null;
        //    }
        //    return DbVer;
        //}
        public bool CleanTableData()
        {
            SQLiteDatabase db1 = new SQLiteDatabase(dbPlatform);
            bool isSaved = db1.Deletescript("delete from CaptMast");
            isSaved = db1.Deletescript("delete from CaptLangMapp");
            //if (DbVersion == "100")
            //{.
            isSaved = db1.Deletescript("delete from LangMast");
            isSaved = db1.Deletescript("delete from FieldMaster");
            isSaved = db1.Deletescript("delete from ProdGrouMast");
            isSaved = db1.Deletescript("delete from AudioPiceMast");
            isSaved = db1.Deletescript("delete from ScriptMast");
            isSaved = db1.Deletescript("delete from ScriptDeta");
            isSaved = db1.Deletescript("delete from ScriptMapping");
            isSaved = db1.Deletescript("delete from ProdGrouScreMappMast");
            isSaved = db1.Deletescript("update UserMast set IsDownloaded = 0,LastDownloadDate=''");
            isSaved = db1.Deletescript("update LangMast set IsDownloaded = 0,LastDownloadDate=''");
            //}
            //else if(DbVersion == "110")
            //{
            //    isSaved = db1.Deletescript("delete from ScriptMast");
            //    isSaved = db1.Deletescript("delete from ScriptDeta");
            //    isSaved = db1.Deletescript("delete from ScriptMapping");
            //    isSaved = db1.Deletescript("update UserMast set IsDownloaded = 0,LastDownloadDate=''");
            //    isSaved = db1.Deletescript("update LangMast set IsDownloaded = 0,LastDownloadDate=''");
            //}
            return isSaved;
        }
        public bool UpdateDBVersion(int version)
        {
            bool isSuccess = false;
            CommonDAL commonDAL = null;
            SQLiteConnection sqliteConnection = null;
            try
            {
                commonDAL = new CommonDAL(dbPlatform);
                sqliteConnection = commonDAL.GetSQLiteConnection();
                using (var db = sqliteConnection)
                {
                    int i = db.Execute("Update DBVersion Set VersionNo=" + version + " ", DateTime.Now.ToString());

                    if (i > 0)
                    {
                        isSuccess = true;
                    }

                    db.Commit();
                    db.Dispose();
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                //;
            }
            finally
            {
                sqliteConnection = null;
            }
            return isSuccess;
        }
        public bool UpdateDBToV2()
        {

            bool isSuccess = false;
            CommonDAL commonDAL = null;
            SQLiteConnection sqliteConnection = null;
            List<string> lstInsertTable = new List<string>();
            lstInsertTable.Add("ALTER table OfflineData add column LANG VARCHAR(50) DEFAULT 'ENGLISH' not null;");
            lstInsertTable.Add("ALTER table OfflineData add column Combo VARCHAR(50) DEFAULT '0' not null;");
            lstInsertTable.Add("ALTER table OfflineData add column SRApplicationNo VARCHAR(50) DEFAULT '0' not null;");
            //InsertRowInTable(lstInsertTable);



            try
            {
                InsertRowInTable(lstInsertTable);
            }
            catch (Exception ex)
            {
                //;
            }
            finally
            {
                sqliteConnection = null;
            }
            return isSuccess;
        }
        public bool UpdateDBToV3()
        {

            bool isSuccess = false;
            CommonDAL commonDAL = null;
            SQLiteConnection sqliteConnection = null;
            List<string> lstInsertTable = new List<string>();
            lstInsertTable.Add("ALTER table OfflineData add column Combo VARCHAR(50) DEFAULT '0' not null;");
            lstInsertTable.Add("ALTER table OfflineData add column SRApplicationNo VARCHAR(50) DEFAULT '0' not null;");
            try
            {
                InsertRowInTable(lstInsertTable);
            }
            catch (Exception ex)
            {
                //;
            }
            finally
            {
                sqliteConnection = null;
            }
            return isSuccess;
        }
        public bool UpdateDBToV1()
        {
            bool isSuccess = false;
            CommonDAL commonDAL = null;
            SQLiteConnection sqliteConnection = null;
            List<string> lstCreateTable = new List<string>();
            lstCreateTable.Add("CREATE TABLE [DBVersion] ([VersionNo] BIGINT,[LastUpdate] DATETIME);");
            lstCreateTable.Add("ALTER table OfflineData add column Status VARCHAR(3);");
            lstCreateTable.Add("ALTER table OfflineData add column EDU VARCHAR(50);");
            lstCreateTable.Add("ALTER table OfflineData add column ADDR VARCHAR(250);");
            lstCreateTable.Add("ALTER table OfflineData add column INC VARCHAR(50);");
            lstCreateTable.Add("ALTER table OfflineData add column HEALTH_DTLS VARCHAR(5);");
            lstCreateTable.Add("ALTER table OfflineData add column HEALTH_OTHER VARCHAR(100);");
            lstCreateTable.Add("ALTER table OfflineData add column OCC_DTLS VARCHAR(50);");




            try
            {
                CreateTables(lstCreateTable);
            }
            catch (Exception ex)
            {
                //;
            }
            finally
            {
                sqliteConnection = null;
            }
            return isSuccess;
        }

        private void InsertRowInTable(List<string> lstInsertTable)
        {
            CommonDAL commonDAL = null;
            SQLiteConnection sqliteConnection = null;
            int i = 0;
            try
            {
                commonDAL = new CommonDAL(dbPlatform);
                sqliteConnection = commonDAL.GetSQLiteConnection();
                using (var db = sqliteConnection)
                {
                    foreach (string item in lstInsertTable)
                    {
                        try
                        {

                            i = db.Execute(item, "");

                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.ToString().Contains("duplicate column name") || ex.Message.ToString().Contains("already exists"))
                            {

                            }
                            else
                            {
                                //throw;
                            }
                        }

                    }
                    db.Commit();
                    db.Dispose();
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                //;
            }
            finally
            {
                commonDAL = null;
                sqliteConnection = null;
            }

        }

        private void CreateTables(List<string> lstCreatTable)
        {
            CommonDAL commonDAL = null;
            SQLiteConnection sqliteConnection = null;
            int i = 0;
            try
            {

                commonDAL = new CommonDAL(dbPlatform);
                sqliteConnection = commonDAL.GetSQLiteConnection();
                using (var db = sqliteConnection)
                {

                    foreach (string item in lstCreatTable)
                    {
                        try
                        {
                            i = db.Execute(item, "");
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.ToString().Contains("duplicate column name") || ex.Message.ToString().Contains("already exists"))
                            {

                            }
                            else
                            {
                                //throw;
                            }
                        }

                    }
                    db.Commit();
                    db.Dispose();
                    db.Close();
                }





            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                commonDAL = null;
                sqliteConnection = null;
            }

        }

        public bool CheckExistingRec(string table, string column, string val, bool isInt)
        {
            bool isExist = false;
            CommonDAL commonDAL = null;
            SQLiteConnection sqliteConnection = null;
            string sqlQuery = string.Empty;
            double count = 0;
            if (isInt)
            {
                sqlQuery = "Select count(*) from " + table + " where " + column + "=" + val + "";
            }
            else
            {
                sqlQuery = "Select count(*) from " + table + " where " + column + "='" + val + "'";
            }

            try
            {
                commonDAL = new CommonDAL(dbPlatform);
                sqliteConnection = commonDAL.GetSQLiteConnection();
                using (var db = sqliteConnection)
                {
                    count = db.ExecuteScalar<double>(sqlQuery, "");
                    if (count > 0)
                    {
                        isExist = true;
                    }

                    db.Commit();
                    db.Dispose();
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                //  ;
            }
            finally
            {
                sqliteConnection = null;
            }
            return isExist;
        }

        public int GetDBVersion()
        {
            int DbVer = 0;
            CommonDAL commonDAL = null;
            DBVersion dbversion = null;
            SQLiteConnection sqliteConnection = null;
            try
            {
                commonDAL = new CommonDAL(dbPlatform);
                sqliteConnection = commonDAL.GetSQLiteConnection();
                using (var db = sqliteConnection)
                {
                    dbversion = db.Query<DBVersion>("Select * from DBVersion", "").FirstOrDefault();
                    if (dbversion.VersionNo > 0)
                    {
                        DbVer = dbversion.VersionNo;
                    }

                    db.Commit();
                    db.Dispose();
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                //  ;
            }
            finally
            {
                sqliteConnection = null;
            }
            return DbVer;
        }




    }
}
