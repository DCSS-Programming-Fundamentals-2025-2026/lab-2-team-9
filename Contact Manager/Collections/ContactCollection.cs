using System;
using System.Collections;
using Contact_Manager.Models;
namespace Contact_Manager.Collections
{
    public class ContactCollection : IEnumerable
    {
        private Contact[] items;
        private int count;
        public ContactCollection(int capacity = 10)
        {
            if (capacity <= 0)
            {
                capacity = 10;
            }
            items = new Contact[capacity];
            count = 0;
        }
        public int Count
        {
            get
            {
                return count;
            }
        }
        public void Add(Contact contact)
        {
            if (count == items.Length)
            {
                Array.Resize(ref items, items.Length * 2);
            }

            items[count] = contact;
            count++;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Неправильний індекс");
            }

            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            count--;
            items[count] = null;
        }
        public Contact GetAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Неправильний індекс");
            }
            return items[index];
        }

        public void SetAt(int index, Contact contact)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Неправильний індекс");
            }
            items[index] = contact;
        }

        public IEnumerator GetEnumerator()
        {
            return new ContactEnumerator(items, count);
        }

        private class ContactEnumerator : IEnumerator
        {
            private Contact[] _items;
            private int _count;
            private int _position;

            public ContactEnumerator(Contact[] items, int count)
            {
                _items = items;
                _count = count;
                _position = -1;
            }

            public bool MoveNext()
            {
                _position++;
                if (_position < _count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                _position = -1;
            }

            public object Current
            {
                get
                {
                    return _items[_position];
                }
            }
        }
    }
}

