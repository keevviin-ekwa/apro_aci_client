using Sign_Up_Form.DTO;
using Sign_Up_Form.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Sign_Up_Form.Utils
{
    public static class Helpers
    {
        public static string DEFAULT_RSV_ACCES_RIGHT = "RSV";
        public static string DEFAULT_ADMIN_ACCES_RIGHT = "ADMIN";
        public static string DEFAULT_COMPUTER_SCIENCE_ACCES_RIGHT = "COMPUTER_SCIENCE";
        public static string DEFAULT_CONTROL_AGENCY_ACCES_RIGHT = "CONTROL_AGENCY";
        public static string DEFAULT_CONTROL_PRINCIPAL_ACCES_RIGHT = "CONTROL_PRINCIPAL";
        public static string DEFAULT_HEAD_AGENCY_ACCES_RIGHT = "HEAD_OF_AGENCY";
        public static string PASSWORD_FOR_ENCRYPTION_AND_DECRYPTION = "pa@ddgterssé$ùfjword#]*";
        public static bool IS_SEND_MAIL_SCEDULED = false;
        public static string DEFAULT_USER_PASSWORD = "aci_lgcca";



        public static string RacineFolderForRecettes = "DossierAppRecettes";
        public static string FolderNameForLogs = "Logs";

        public static ConnectedUserModel connectedUserModel = null;
        public static List<Produit> GlobaProduit = null;
        public static List<Objectif> GlobalObjectif = null;

        public static void WriteLog(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + RacineFolderForRecettes + "\\" + FolderNameForLogs + "\\";
            logFilePath = logFilePath + "Log-" + DateTime.Now.ToString("dd-MM-yyyy HH'h'mm'min'ss'sec'") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog);
            log.Close();
        }


        public static string removeWhiteSpacesInString(string inputString)
        {
            if (inputString == "")
            {
                return "";
            }
            else
            {
                string outputString = Regex.Replace(inputString, @"\s", "");
                return outputString;
            }
        }


        // Generates a random number within a range.      
        public static int RandomNumber(int min, int max)
        {
            Random _random = new Random();
            int generatedNumber = _random.Next(min, max);
            return generatedNumber;
        }

        // Converts a string to a list.      
        public static List<string> convertStringToList(string toBeConverted)
        {
            List<string> result = toBeConverted.Split(';').ToList();
            return result;
        }

        // Converts a list to a string.      
        public static string convertListToString(List<string> toBeConverted)
        {
            var result = String.Join(";", toBeConverted);
            return result;
        }

        public static bool haveRight(List<string> accesRightList, string accessRight)
        {
            if (accesRightList.Contains(accessRight))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static PropertyInfo[] getPropertiesInfosFromList<T>(List<T> models)
        {
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return Props;
        }

        // T : Generic Class
        public static DataTable ConvertToDataTable<T>(List<T> models)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = getPropertiesInfosFromList<T>(models);
            // Loop through all the properties            
            // Adding Column to our datatable
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names 
                dataTable.Columns.Add(prop.Name);
            }

            // Adding Row
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows  
                    values[i] = Props[i].GetValue(item, null);
                }
                // Finally add value to datatable  
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }




    }
}
