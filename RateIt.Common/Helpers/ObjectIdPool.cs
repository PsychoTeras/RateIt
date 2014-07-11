using System.Collections;
using MongoDB.Bson;

namespace RateIt.Common.Helpers
{
    internal sealed class ObjectIdPool
    {

#region Constants

        private const float GROW_FACTOR = 1.5f;

#endregion

#region Private static fields

        private static readonly ObjectIdPool _instance = new ObjectIdPool();

#endregion

#region Static properties

        public static ObjectIdPool Instance
        {
            get { return _instance; }
        }

#endregion

#region Private fields

        private readonly Queue _queue;
        private int _queueCapacity = 512;

#endregion

#region Class methods

        private ObjectIdPool()
        {
            _queue = Queue.Synchronized(new Queue(_queueCapacity, GROW_FACTOR));
        }

        private void CheckAndExpandQueueCapacity()
        {
            if (_instance._queue.Count == 0)
            {
                lock (_instance._queue)
                {
                    if (_instance._queue.Count == 0)
                    {
                        int count = (int) ((_queueCapacity*GROW_FACTOR) - _queueCapacity);
                        for (int i = 0; i < count; i++)
                        {
                            _queue.Enqueue(new ObjectId());
                        }
                        _queueCapacity += count;
                    }
                }
            }
        }

        public static ObjectId Pop()
        {
            _instance.CheckAndExpandQueueCapacity();
            return (ObjectId)_instance._queue.Dequeue();
        }

        public static ObjectId Pop(string id)
        {
            _instance.CheckAndExpandQueueCapacity();
            ObjectId objectId = (ObjectId) _instance._queue.Dequeue();
            objectId.ParseString(id);
            return objectId;
        }

        public static ObjectId Pop(byte[] bytes)
        {
            _instance.CheckAndExpandQueueCapacity();
            ObjectId objectId = (ObjectId) _instance._queue.Dequeue();
            objectId.ParseBytes(bytes);
            return objectId;
        }

        public static void Push(ObjectId obj)
        {
            _instance._queue.Enqueue(obj);
        }

#endregion

    }
}
