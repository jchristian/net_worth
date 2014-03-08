using System.Collections.Generic;

namespace core.importers.persisters
{
    public interface ICollectionPersister<T>
    {
        void Persist(IEnumerable<T> items);
    }
}