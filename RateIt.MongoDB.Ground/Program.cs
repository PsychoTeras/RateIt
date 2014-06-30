using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.SupplyClasses;
using RateIt.MongoDB.Ground.Helpers;
using ProductInfo = RateIt.MongoDB.Ground.Entities.ProductInfo;

namespace RateIt.MongoDB.Ground
{
    class Program
    {

#region Constants

        private const int MONGODB_DEFAULT_PORT = 27017;
        private const string MONGODB_DATABASE_NAME = "RateItDB";
        private const string MONGODB_PRODUCTS_COLLECTION_NAME = "Products";

#endregion

#region MongoDB methods

        private static MongoServer GetMongoServer(string host, int port)
        {
            string connectionString = string.Format(@"mongodb://{0}:{1}", host, port);
            return new MongoClient(connectionString).GetServer();
        }

        private static MongoDatabase OpenMongoDatabase(MongoServer server, 
            string databaseName)
        {
            if (!server.DatabaseExists(databaseName))
            {
                server.CreateDatabaseSettings(databaseName);
            }
            return server.GetDatabase(databaseName);
        }

        private static MongoCollection<ProductInfo> GetMongoDatabaseCollection(
            MongoDatabase database, string collectioName)
        {
            return database.GetCollection<ProductInfo>(collectioName);
        }

        private static MongoCollection<ProductInfo> RecreateMongoDatabaseCollection(
            MongoDatabase database, string collectioName)
        {
            if (database.CollectionExists(collectioName))
            {
                database.DropCollection(collectioName);
            }
            database.CreateCollection(collectioName);
            return database.GetCollection<ProductInfo>(collectioName);
        }

        private static void DisconnectMongoServer(MongoServer server)
        {
            server.Disconnect();
        }

#endregion

#region Demo products methods

        private static List<ProductInfo> GenerateDemoProducts(int productsCount)
        {
            List<ProductInfo> productsList = new List<ProductInfo>();
            NameGenerator nameGenerator = new NameGenerator();
            HashSet<string> uniqueNamesSet = new HashSet<string>();

            for (int i = 0; i < productsCount; i++)
            {
                string productName;
                do
                {
                    productName = nameGenerator.Generate();
                } 
                while (uniqueNamesSet.Contains(productName));
                uniqueNamesSet.Add(productName);

                ProductInfo product = new ProductInfo();
                product.Name = productName;
                productsList.Add(product);
            }

            return productsList;
        }

#endregion

#region Class methods

        private static void WriteLog(string logMessage)
        {
            WriteLog(logMessage, true);
        }

        private static void WriteLog(string logMessage, bool writeLine)
        {
            logMessage = string.Format("{0}: {1}", DateTime.Now, logMessage ?? string.Empty);
            if (writeLine)
            {
                Console.WriteLine(logMessage);
            }
            else
            {
                Console.Write(logMessage);
            }
        }

        private static void CreateAndFillProductsDatabase(string host)
        {
            WriteLog("PRC: CreateAndFillProductsDatabase\n\n");

            try
            {
                //Connect to the MongoDB server
                WriteLog(string.Format("Establish connection to the MongoDB server, {0}:{1}... ",
                                       host, MONGODB_DEFAULT_PORT), false);
                MongoServer server = GetMongoServer(host, MONGODB_DEFAULT_PORT);
                Console.WriteLine("done");

                //Create or connect RateIt database
                WriteLog(string.Format("Create or connect database '{0}'... ",
                                       MONGODB_DATABASE_NAME), false);
                MongoDatabase db = OpenMongoDatabase(server, MONGODB_DATABASE_NAME);
                Console.WriteLine("done");

                //Recreate products collection
                WriteLog(string.Format("Recreate products collection '{0}'... ",
                                       MONGODB_PRODUCTS_COLLECTION_NAME), false);
                MongoCollection<ProductInfo> collection = RecreateMongoDatabaseCollection(
                    db, MONGODB_PRODUCTS_COLLECTION_NAME);
                Console.WriteLine("done");

                //Generate demo products
                const int productsCount = 100000;
                WriteLog(string.Format("Generate {0} demo products... ",
                                       productsCount), false);
                List<ProductInfo> productList = GenerateDemoProducts(productsCount);
                Console.WriteLine("done");

                //Store the product list into the collection
                WriteLog(string.Format("Store product list into collection '{0}'... ",
                                       MONGODB_PRODUCTS_COLLECTION_NAME), false);
                MongoInsertOptions insOpt = new MongoInsertOptions
                {
                    CheckElementNames = true,
                    Flags = InsertFlags.ContinueOnError
                };
                collection.InsertBatch<ProductInfo>(productList.ToArray(), insOpt);
                Console.WriteLine("done");

                //Disconnect from the MongoDB server
                WriteLog("Disconnect from the MongoDB server... ", false);
                DisconnectMongoServer(server);
                Console.WriteLine("done");

                Console.WriteLine("\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("\n\nEXCEPTION: {0}", ex));
            }
        }

        private static void QueryProductsDatabase(string host)
        {
            WriteLog("PRC: QueryProductsDatabase\n\n");

            try
            {
                //Connect to the MongoDB server
                WriteLog(string.Format("Establish connection to the MongoDB server, {0}:{1}... ",
                                       host, MONGODB_DEFAULT_PORT), false);
                MongoServer server = GetMongoServer(host, MONGODB_DEFAULT_PORT);
                Console.WriteLine("done");

                //Create or connect RateIt database
                WriteLog(string.Format("Create or connect database '{0}'... ",
                                       MONGODB_DATABASE_NAME), false);
                MongoDatabase db = OpenMongoDatabase(server, MONGODB_DATABASE_NAME);
                Console.WriteLine("done");

                //Get products collection
                WriteLog(string.Format("Get products collection '{0}'... ",
                                       MONGODB_PRODUCTS_COLLECTION_NAME), false);
                MongoCollection<ProductInfo> collection = GetMongoDatabaseCollection(
                    db, MONGODB_PRODUCTS_COLLECTION_NAME);
                Console.WriteLine("done");

                //Get the element <Product> from the database
                string elementNameLike = string.Format("/{0}/", new NameGenerator().Generate(4, 4, 1)).ToLower();
                WriteLog(string.Format("Get all elements with the name like '{0}' from the database (CS)... ",
                                       elementNameLike.Replace("/", "%")), false);
                IMongoQuery queryGetElementByName = Query.Matches("UserName", elementNameLike);
                MongoCursor<ProductInfo> products = collection.FindAs<ProductInfo>(queryGetElementByName);

                //Print found products name
                StringBuilder foundItems = new StringBuilder();
                foreach (ProductInfo product in products)
                {
                    foundItems.AppendFormat("{0}{1}", foundItems.Length > 0 ? ", " : "",
                                            product.Name);
                }
                Console.WriteLine(string.Format("done.\nFound items ({0}): {1}\n", products.Count(),
                    foundItems));
            }
            catch (Exception ex)
            {
                WriteLog(string.Format("\n\n{0}", ex));
            }
            Console.WriteLine("\n\n");
        }

        private static void QueryProductsDatabasePerf(string host, string userName)
        {
            WriteLog("PRC: QueryProductsDatabasePerf\n\n");

            try
            {
                //Connect to the MongoDB server
                WriteLog(string.Format("Establish connection to the MongoDB server, {0}:{1}... ",
                                       host, MONGODB_DEFAULT_PORT), false);
                MongoServer server = GetMongoServer(host, MONGODB_DEFAULT_PORT);
                Console.WriteLine("done");

                //Create or connect RateIt database
                WriteLog(string.Format("Create or connect database '{0}'... ",
                                       MONGODB_DATABASE_NAME), false);
                MongoDatabase db = OpenMongoDatabase(server, MONGODB_DATABASE_NAME);
                Console.WriteLine("done");

                //Get products collection
                WriteLog(string.Format("Get collection '{0}'... ",
                                       MONGODB_PRODUCTS_COLLECTION_NAME), false);
                MongoCollection<User> collection = db.GetCollection<User>("T_USERS");
                Console.WriteLine("done");

                Console.WriteLine();
                Console.WriteLine("Select GROUP, EQ:");

                HRTimer timer;
                const int elementsCount = 1;
                const int take = 100;

                //Begin test 1
                string elementNameLike = string.Format("/^{0}$/si", userName);
                IMongoQuery qUserByName = Query.Matches("UserName", elementNameLike);
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = collection.Find(qUserByName).Take(take).ToArray();
                }
                WriteLog(string.Format("#1 [Query]. Items count: {0}. Processed time: {1} msec",
                    elementsCount, timer.StopWatch()));

                //Begin test 2
                timer = HRTimer.CreateAndStart();
                IQueryable<User> result = collection.AsQueryable();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = result.Where(e => e.UserName.ToLower() == userName).Take(take).ToArray();
                }
                WriteLog(string.Format("#2 [Linq]. Items count: {0}. Processed time: {1} msec",
                    elementsCount, timer.StopWatch()));

                //Begin test 3
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = collection.Find(qUserByName).Take(take).ToArray();
                }
                WriteLog(string.Format("#3 [Query]. Items count: {0}. Processed time: {1} msec",
                    elementsCount, timer.StopWatch()));

                //Begin test 4
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = result.Where(e => e.UserName.ToLower() == userName).Take(take).ToArray();
                }
                WriteLog(string.Format("#4 [Linq]. Items count: {0}. Processed time: {1} msec",
                    elementsCount, timer.StopWatch()));

                Console.WriteLine();
                Console.WriteLine("Select SINGLE, EQ:");

                //Begin test 5
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = collection.FindOne(qUserByName);
                }
                WriteLog(string.Format("#5 [Query]. Items count: {0}. Processed time: {1} msec",
                    1, timer.StopWatch()));

                //Begin test 6
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = result.FirstOrDefault(e => e.UserName.ToLower() == userName);
                }
                WriteLog(string.Format("#6 [Linq]. Items count: {0}. Processed time: {1} msec",
                    1, timer.StopWatch()));

                //Begin test 7
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = collection.FindOne(qUserByName);
                }
                WriteLog(string.Format("#7 [Query]. Items count: {0}. Processed time: {1} msec",
                    1, timer.StopWatch()));

                //Begin test 8
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = result.FirstOrDefault(e => e.UserName.ToLower() == userName);
                }
                WriteLog(string.Format("#8 [Linq]. Items count: {0}. Processed time: {1} msec",
                    1, timer.StopWatch()));

                Console.WriteLine();
                Console.WriteLine("Select GROUP, IN:");

                userName = userName.Substring(userName.Length/2);

                //Begin test 9
                elementNameLike = string.Format("/{0}/si", userName);
                qUserByName = Query.Matches("UserName", elementNameLike);
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = collection.Find(qUserByName).Take(take).ToArray();
                }
                WriteLog(string.Format("#9 [Query]. Items count: {0}. Processed time: {1} msec",
                    elementsCount, timer.StopWatch()));

                //Begin test 10
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = result.Where(e => e.UserName.ToLower().Contains(userName)).Take(take).ToArray();
                }
                WriteLog(string.Format("#10 [Linq]. Items count: {0}. Processed time: {1} msec",
                    elementsCount, timer.StopWatch()));

                //Begin test 11
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = collection.Find(qUserByName).Take(take).ToArray();
                }
                WriteLog(string.Format("#11 [Query]. Items count: {0}. Processed time: {1} msec",
                    elementsCount, timer.StopWatch()));

                //Begin test 10
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = result.Where(e => e.UserName.ToLower().Contains(userName)).Take(take).ToArray();
                }
                WriteLog(string.Format("#12 [Linq]. Items count: {0}. Processed time: {1} msec",
                    elementsCount, timer.StopWatch()));

                Console.WriteLine();
                Console.WriteLine("Select SINGLE, IN:");

                //Begin test 11
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = collection.FindOne(qUserByName);
                }
                WriteLog(string.Format("#11 [Query]. Items count: {0}. Processed time: {1} msec",
                    1, timer.StopWatch()));

                //Begin test 12
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = result.FirstOrDefault(e => e.UserName.ToLower().Contains(userName));
                }
                WriteLog(string.Format("#12 [Linq]. Items count: {0}. Processed time: {1} msec",
                    1, timer.StopWatch()));

                //Begin test 13
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = collection.FindOne(qUserByName);
                }
                WriteLog(string.Format("#13 [Query]. Items count: {0}. Processed time: {1} msec",
                    1, timer.StopWatch()));

                //Begin test 14
                timer = HRTimer.CreateAndStart();
                for (int i = 0; i < elementsCount; i++)
                {
                    var x = result.FirstOrDefault(e => e.UserName.ToLower().Contains(userName));
                }
                WriteLog(string.Format("#14 [Linq]. Items count: {0}. Processed time: {1} msec",
                    1, timer.StopWatch()));

                //End test
            }
            catch (Exception ex)
            {
                WriteLog(string.Format("\n\n{0}", ex));
            }
            Console.WriteLine("\n\n");
        }

        static void Main(string[] args)
        {
            string host = args.Length == 1 ? args[0] : "localhost";

            //CreateAndFillProductsDatabase(host);
            //QueryProductsDatabase(host);
            QueryProductsDatabasePerf(host, "");

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }

#endregion

    }
}
