using System;
using System.Collections.Generic;
using System.Linq;

namespace CircularQueue
{
    public class CircularQueue<T> : IQueue<T>
    {
        private T[] _items;
        private int _head;
        private int _tail;
        
        public int Capacity { get; private set; } = 4;

        public int Count => IsTailAfterHead() ? _tail - _head : Capacity - _head + _tail;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularQueue{T}"/> class that is empty and has the default initial capacity.
        /// </summary>
        public CircularQueue()
        {
            _items = new T[Capacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularQueue{T}"/> class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="CircularQueue{T}"/> can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException">capacity is less than zero.</exception>
        public CircularQueue(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Non-negative number required.");
            }
            
            Capacity = capacity;
            _items = new T[Capacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularQueue{T}"/> class that contains elements copied from the specified
        /// collection and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="CircularQueue{T}"/>.</param>
        /// <exception cref="ArgumentNullException">collection is null.</exception>
        public CircularQueue(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var array = collection.ToArray();
            var arrayLength = array.Length;
            
            Capacity = arrayLength + 1;
            _items = new T[Capacity];
            _tail = arrayLength;
            Array.Copy(array, _items, arrayLength);
        }
        
        /// <summary>
        /// Adds an object to the end of the <see cref="CircularQueue{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="CircularQueue{T}"/>. The value can be null for reference types.</param>
        public void Enqueue(T item)
        {
            if (NeedsReallocateMemory())
            {
                ReallocateMemory();
            }

            _items[_tail++] = item;

            if (_tail == Capacity)
            {
                _tail = 0;
            }
        }

        /// <summary>
        /// Returns the object at the beginning of the <see cref="CircularQueue{T}"/> without removing it.
        /// </summary>
        /// <returns>The object at the beginning of the <see cref="CircularQueue{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="CircularQueue{T}"/> is empty.</exception>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return _items[_head];
        }
        
        /// <summary>
        /// Removes and returns the object at the beginning of the <see cref="CircularQueue{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the <see cref="CircularQueue{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="CircularQueue{T}"/> is empty.</exception>
        public T Dequeue()
        {
            var value = Peek();
            _head++;
            
            if (_head == Capacity)
            {
                _head = 0;
            }
            
            return value;
        }

        private bool NeedsReallocateMemory() => Count == Capacity - 1;

        private void ReallocateMemory()
        {
            if (IsTailAfterHead())
            {
                ReallocateHeadFirstCase();
            }
            else
            {
                ReallocateTailFirstCase();
            }
        }

        private bool IsTailAfterHead() => _tail >= _head;
        
        private void ReallocateHeadFirstCase()
        {
            Capacity *= 2;
            var items = new T[Capacity];

            for (var i = 0; i < _tail; i++) 
            {
                items[i] = _items[_head + i];
            }
            
            _items = items;
            _head = 0;
        }

        private void ReallocateTailFirstCase()
        {
            var oldCapacity = Capacity;
            Capacity *= 2;
            var items = new T[Capacity];
            var currentIndex = _head;
            var itemsCount = 0;

            while (currentIndex != _tail)
            {
                items[itemsCount++] = _items[currentIndex];
                currentIndex = currentIndex == oldCapacity - 1 ? 0 : ++currentIndex;
            }

            _items = items;
            _head = 0;
            _tail = itemsCount;
        }
    }
}