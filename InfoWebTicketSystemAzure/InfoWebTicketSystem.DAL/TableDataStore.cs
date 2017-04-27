// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleDataStore.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   Class implementation for RoleDataStore.cs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

using MediaValet.Data.Core;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace InfoWebTicketSystem.DAL
{
    public class TableDataStore : IDataStore
    {
        private readonly string _tableStorageEndpoint;
        private readonly string _accountName;
        private readonly string _accountSharedKey;

        public TableDataStore(
            string tableStorageEndpoint,
            string accountName,
            string accountSharedKey)
        {
            //Ensure.Argument.NotNullOrEmpty(tableStorageEndpoint, "tableStorageEndpoint");
            //Ensure.Argument.NotNullOrEmpty(accountName, "accountName");
            //Ensure.Argument.NotNullOrEmpty(accountSharedKey, "accountSharedKey");

            this._accountName = accountName;
            this._accountSharedKey = accountSharedKey;
            this._tableStorageEndpoint = tableStorageEndpoint;

        }

        public TableDataStore()
            : this(
                ConfigurationManager.AppSettings["TableStorageEndpoint"],
                ConfigurationManager.AppSettings["AccountName"],
                ConfigurationManager.AppSettings["AccountSharedKey"])
        {

        }

        public T Insert<T>(T item) where T : IEntity
        {
            var entityType = typeof(T);

            var tableName = entityType.Name;

            this.CreateTableIfNotExists(tableName);

            var tableServiceContext = this.CreateContext();

            tableServiceContext.AddObject(tableName, item);
            tableServiceContext.SaveChangesWithRetries();

            return item;
        }

        public void Delete<T>(T item) where T : IEntity
        {
            var entityType = typeof(T);

            var tableName = entityType.Name;

            this.CreateTableIfNotExists(tableName);

            var tableServiceContext = this.CreateContext();

            var queryTable = tableServiceContext.CreateQuery<T>(tableName);

            var results = queryTable.Where(p => p.PartitionKey == item.PartitionKey && p.RowKey == item.RowKey).AsTableServiceQuery().Execute();

            var obj = results.FirstOrDefault();

            if (obj == null)
            {
                return;
            }

            tableServiceContext.DeleteObject(obj);
            tableServiceContext.SaveChangesWithRetries();
        }

        public T Update<T>(T item) where T : IEntity
        {
            var entityType = typeof(T);

            var tableName = entityType.Name;

            this.CreateTableIfNotExists(tableName);

            var tableServiceContext = this.CreateContext();

            var queryTable = tableServiceContext.CreateQuery<T>(tableName);

            var results = queryTable.Where(p => p.PartitionKey == item.PartitionKey && p.RowKey == item.RowKey).AsTableServiceQuery().Execute();

            var obj = results.FirstOrDefault();

            if (obj == null)
            {
                return default(T);
            }

            var properties = entityType.GetProperties();

            foreach (var propertyInfo in properties)
            {
                propertyInfo.SetValue(obj, propertyInfo.GetValue(item));
            }

            tableServiceContext.UpdateObject(obj);
            tableServiceContext.SaveChangesWithRetries();

            return item;
        }

        public IEnumerable<T> Read<T>(Expression<Func<T, bool>> query) where T : IEntity
        {
            var entityType = typeof(T);

            var tableName = entityType.Name;

            this.CreateTableIfNotExists(tableName);

            var tableServiceContext = this.CreateContext();

            var queryTable = tableServiceContext.CreateQuery<T>(tableName);

            return queryTable.Where(query).AsTableServiceQuery().Execute();
        }

        private void CreateTableIfNotExists(string tableName)
        {
            var tableClient = new CloudTableClient(
                new Uri(this._tableStorageEndpoint),
                new StorageCredentialsAccountAndKey(this._accountName, this._accountSharedKey));

            tableClient.CreateTableIfNotExist(tableName);
        }

        private TableServiceContext CreateContext()
        {
            return new TableServiceContext(
                this._tableStorageEndpoint,
                new StorageCredentialsAccountAndKey(this._accountName, this._accountSharedKey)
                );
        }
    }
}
