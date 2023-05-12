using System.Collections.Generic;
using System.Linq;

namespace HideAndSeek
{
    public class BasePool<TObject, TId>
    {
        protected readonly List<TObject> ReturnedObjects;
        protected readonly Dictionary<TObject, Identifier<TObject, TId>> Identifires;

        private readonly IFactory<TObject, TId> _factory;

        public BasePool(IFactory<TObject, TId> factory)
        {
            _factory = factory;
            ReturnedObjects = new List<TObject>();
            Identifires = new Dictionary<TObject, Identifier<TObject, TId>>();
        }

        public TObject Get(TId id)
        {
            var validIdentifier = Identifires.Values.Where(x => x.Id.Equals(id));

            if (validIdentifier.Count() == 0)
            {
                return CreateNewObject(id);
            }

            var validObject = ReturnedObjects
                .Intersect(validIdentifier.Select(x => x.Object))
                .FirstOrDefault();

            if (validObject != null)
            {
                ReturnedObjects.Remove(validObject);

                OnGetFromPool(validObject);

                return validObject;
            }

            return CreateNewObject(id);
        }

        public void ReturnToPool(TObject obj)
        {
            if (Identifires.ContainsKey(obj) && !ReturnedObjects.Contains(obj))
            {
                ReturnedObjects.Add(obj);

                OnReturnToPool(obj);
            }
        }

        public void ReturnAll()
        {
            foreach (var identifier in Identifires.Values)
            {
                ReturnToPool(identifier.Object);
            }
        }

        public void ReturnToDefault()
        {
            ReturnedObjects.Clear();
            Identifires.Clear();
        }

        protected virtual void OnGetFromPool(TObject obj) { }
        protected virtual void OnReturnToPool(TObject obj) { }

        private TObject CreateNewObject(TId id)
        {
            var obj = _factory.Create(id);

            Identifires.Add(obj, new Identifier<TObject, TId>(obj, id));

            return obj;
        }
    }
}
