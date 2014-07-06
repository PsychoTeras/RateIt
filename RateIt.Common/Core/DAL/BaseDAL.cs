﻿using System;
using MongoDB.Driver;
using RateIt.Common.Core.System;
using RateIt.Common.SupplyClasses;

namespace RateIt.Common.Core.DAL
{
    internal abstract class BaseDAL<T> : MongoDBConnection
        where T : class
    {

#region Constants

        public const string DATABASE_NAME = "RateItDB";

#endregion

#region Private members

        private readonly MongoInsertOptions _dbInsertOptions = new MongoInsertOptions
                                                                   {
                                                                       CheckElementNames = true,
                                                                       Flags = InsertFlags.ContinueOnError
                                                                   };

#endregion

#region Properties

        internal abstract string CollectionName { get; }
        internal MongoDatabase Database { get; private set; }
        internal MongoCollection<T> DataCollection { get; private set; }
        protected virtual MongoInsertOptions InsertOptions { get { return _dbInsertOptions; } }

#endregion

#region Class methods

        protected virtual void CreateCollectionStructure() { }

        protected void AssertErrorMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                throw new Exception(message);
            }
        }

        protected BaseDAL() : base(Configuration.DBHost, Configuration.DBPort)
        {
            //Open or create database
            if (!Server.DatabaseExists(DATABASE_NAME))
            {
                Server.CreateDatabaseSettings(DATABASE_NAME);
            }
            Database = Server.GetDatabase(DATABASE_NAME);

            //Open or create collection
            bool collectionExists = Database.CollectionExists(CollectionName);
            if (!collectionExists)
            {
                Database.CreateCollection(CollectionName);
            }
            DataCollection = Database.GetCollection<T>(CollectionName);
            if (!collectionExists)
            {
                CreateCollectionStructure();
            }
        }

#endregion

    }
}
