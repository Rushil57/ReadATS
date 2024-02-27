using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table.Queryable;

namespace ReadATS
{
    public class Program
    {
        static void Main(string[] args)
        {
            var list = ReadCustomerInfo("US");
            Console.ReadLine();
        }

        /// <summary>
        /// Read data from CustomerInfo table based on country name and returns list of countries
        /// </summary>
        /// <param name="countryName">country name to be search for</param>
        /// <returns>list of countries based on country name from CustomerInfo table.</returns>
        public static List<string> ReadCustomerInfo(string countryName)
        {
            var list = new List<string>();
            var table = InitializeConnection();
            var tableQuery = table.CreateQuery<CustomerInfo>().Where(x => x.Country == countryName).AsTableQuery();
            var result = table.ExecuteQuerySegmentedAsync(tableQuery, null).Result;
            if (result != null && result.Results.Count > 0)
            {
                list = result.Results.Select(x => x.Country).ToList();
            }
            return list;
        }

        /// <summary>
        /// Method to initialize the azure connections based on storage account name and key.
        /// </summary>
        /// <returns>CloudTable object</returns>
        private static CloudTable InitializeConnection()
        {
            try
            {
                string accountName = ""; // Azure storage account name
                string accountKey = "";  // Azure storage account key
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                CloudTableClient client = account.CreateCloudTableClient();
                CloudTable table = client.GetTableReference("CustomerInfo");
                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
