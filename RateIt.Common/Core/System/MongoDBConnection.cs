using System;
using MongoDB.Driver;

namespace RateIt.Common.Core.System
{
    internal abstract class MongoDBConnection : IDisposable
    {

#region Constants

        private const int MONGODB_DEFAULT_PORT = 27017;

#endregion

#region Properties

        protected MongoServer Server { get; private set; }

#endregion

#region Class methods

        protected MongoDBConnection(string host) 
            : this(host, MONGODB_DEFAULT_PORT) {}

        protected MongoDBConnection(string host, int port)
        {
            string connectionString = string.Format(@"mongodb://{0}:{1}", host, port);
            Server = new MongoClient(connectionString).GetServer();
        }

        public void Dispose()
        {
            if (Server != null)
            {
                Server.Disconnect();
                Server = null;
            }
        }

#endregion

    }
}
