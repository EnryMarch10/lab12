namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> list = new List<TItem>();

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => list.Count();

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => list.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => list[index];
            set {
                var element = list[index];
                list[index] = value;
                ElementChanged?.Invoke(this, list[index], element, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => list.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            list.Add(item);
            ElementInserted?.Invoke(this, item, list.IndexOf(item));
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            var clone = new List<TItem>(list);
            list.Clear();
            for (int i = 0; i < clone.Count; i++)
            {
                this.ElementRemoved?.Invoke(this, clone[i], i);
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) => list.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            int index = list.IndexOf(item);
            if (list.Remove(item))
            {
                ElementRemoved?.Invoke(this, item, index);
                return true;
            }
            return false;
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item) => list.IndexOf(item);

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            list.Insert(index, item);
            ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            var element = list.ElementAt(index);
            list.RemoveAt(index);
            ElementRemoved?.Invoke(this, element, index);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj) =>
            obj is ObservableList<TItem> item && list.Equals(item.list);

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode() =>
            list.GetHashCode();

        /// <inheritdoc cref="object.ToString" />
        public override string ToString() =>
            list.ToString();
    }
}
