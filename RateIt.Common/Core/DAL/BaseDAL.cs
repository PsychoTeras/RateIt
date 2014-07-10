using System;
using MongoDB.Driver;
using RateIt.Common.Core.System;

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
        private readonly MongoDatabaseSettings _dbSettings = new MongoDatabaseSettings();

#endregion

#region Properties

        internal abstract string CollectionName { get; }

        internal MongoDatabase Database { get; private set; }
        internal MongoCollection<T> DataCollection { get; private set; }

        protected virtual MongoDatabaseSettings DBSettings { get { return _dbSettings; } }
        protected virtual MongoInsertOptions InsertOptions { get { return _dbInsertOptions; } }

#endregion

#region Class methods

        protected virtual void CreateCollectionStructure() { }

        protected virtual void InitializeDB() { }

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
            Database = !Server.DatabaseExists(DATABASE_NAME)
                ? new MongoDatabase(Server, DATABASE_NAME, DBSettings)
                : Server.GetDatabase(DATABASE_NAME);

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
            InitializeDB();
        }

        public void ReIndex()
        {
            DataCollection.ReIndex();
        }

#endregion

    }
}
