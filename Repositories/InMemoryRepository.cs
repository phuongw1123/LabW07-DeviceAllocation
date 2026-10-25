/****************/
// Mã sinh viên: 20206161
// Họ tên: Phạm Thọ Phương
/****************/

using System;
using System.Collections.Generic;
using LabW07_DeviceAllocation.Domain;
using LabW07_DeviceAllocation.Exceptions;

namespace LabW07_DeviceAllocation.Repositories
{
    public interface IRepository<T> where T : IIdentifiable
    {
        void Add(T item);
        bool Remove(string id);
        T? FindById(string id);
        IReadOnlyList<T> GetAll();
        IReadOnlyList<T> FindAll(Predicate<T> condition);
    }

    public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
    {
        private readonly List<T> items = new List<T>();

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (FindById(item.Id) != null)
                throw new DuplicateIdException(item.Id);
            items.Add(item);
        }

        public bool Remove(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            int index = items.FindIndex(x => string.Equals(x.Id, id.Trim(), StringComparison.OrdinalIgnoreCase));
            if (index < 0) return false;
            items.RemoveAt(index);
            return true;
        }

        public T? FindById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return default;
            return items.Find(x => string.Equals(x.Id, id.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public IReadOnlyList<T> GetAll() => items.AsReadOnly();

        public IReadOnlyList<T> FindAll(Predicate<T> condition)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            return items.FindAll(condition).AsReadOnly();
        }
    }
}
