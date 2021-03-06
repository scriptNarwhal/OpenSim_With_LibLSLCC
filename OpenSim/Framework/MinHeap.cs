/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OpenSim.Framework
{
    public interface IHandle { }

    [Serializable, ComVisible(false)]
    public class MinHeap<T> : ICollection<T>, ICollection
    {
        private class Handle : IHandle
        {
            internal int index = -1;
            internal MinHeap<T> heap = null;

            internal void Clear()
            {
                index = -1;
                heap = null;
            }
        }

        private struct HeapItem
        {
            internal T value;
            internal Handle handle;

            internal HeapItem(T _value, Handle _handle)
            {
                value = _value;
                handle = _handle;
            }

            internal void Clear()
            {
                if (handle != null)
                {
                    handle.Clear();
                    handle = null;
                }               
                value = default(T);
            }

            internal void ClearRef()
            {
                value = default(T);
                handle = null;
            }
        }

        public const int DEFAULT_CAPACITY = 4;

        private HeapItem[] items;
        private int size;
        private object sync_root;
        private int version;
        private int capacity;

        private Comparison<T> comparison;

        public MinHeap() : this(DEFAULT_CAPACITY, Comparer<T>.Default) { }
        public MinHeap(int capacity) : this(capacity, Comparer<T>.Default) { }
        public MinHeap(IComparer<T> comparer) : this(DEFAULT_CAPACITY, comparer) { }
        public MinHeap(int capacity, IComparer<T> comparer) :
            this(capacity, new Comparison<T>(comparer.Compare)) { }
        public MinHeap(Comparison<T> comparison) : this(DEFAULT_CAPACITY, comparison) { }
        public MinHeap(int _capacity, Comparison<T> _comparison)
        {
            capacity = _capacity;
            items = new HeapItem[capacity];
            comparison = _comparison;
            size = version = 0;
        }

        public int Count { get { return size; } }

        public bool IsReadOnly { get { return false; } }

        public bool IsSynchronized { get { return false; } }

        public T this[IHandle key]
        {
            get
            {
                Handle handle = ValidateThisHandle(key);
                return items[handle.index].value;
            }

            set
            {
                Handle handle = ValidateThisHandle(key);
                int indx = handle.index;
                items[indx].value = value;
                if (!BubbleUp(indx))
                    BubbleDown(indx);
            }
        }

        public object SyncRoot
        {
            get
            {
                if (sync_root == null)
                    Interlocked.CompareExchange<object>(ref sync_root, new object(), null);
                return sync_root;
            }
        }

        private Handle ValidateHandle(IHandle ihandle)
        {
            if (ihandle == null)
                throw new ArgumentNullException("handle");
            Handle handle = ihandle as Handle;
            if (handle == null)
                throw new InvalidOperationException("handle is not valid");
            return handle;
        }

        private Handle ValidateThisHandle(IHandle ihandle)
        {
            Handle handle = ValidateHandle(ihandle);
            if (!object.ReferenceEquals(handle.heap, this))
                throw new InvalidOperationException("handle is not valid for this heap");
            if (handle.index < 0)
                throw new InvalidOperationException("handle is not associated to a value");
            return handle;
        }

        private void Set(HeapItem item, int index)
        {
            items[index] = item;
            if (item.handle != null)
                item.handle.index = index;
        }

        private bool BubbleUp(int index)
        {
            HeapItem item = items[index];
            int current, parent;

            for (current = index, parent = (current - 1) / 2;
                (current > 0) && (comparison(items[parent].value, item.value)) > 0;
                current = parent, parent = (current - 1) / 2)
            {
                Set(items[parent], current);
            }

            if (current != index)
            {
                Set(item, current);
                ++version;
                return true;
            }
            return false;
        }

        private void BubbleDown(int index)
        {
            HeapItem item = items[index];
            int current, child;

            for (current = index, child = (2 * current) + 1;
                current < size / 2;
                current = child, child = (2 * current) + 1)
            {
                if ((child < size - 1) && comparison(items[child].value, items[child + 1].value) > 0)
                    ++child;
                if (comparison(items[child].value, item.value) >= 0)
                    break;
                Set(items[child], current);
            }

            if (current != index)
            {
                Set(item, current);
                ++version;
            }
        }

        public bool TryGetValue(IHandle key, out T value)
        {
            Handle handle = ValidateHandle(key);
            if (handle.index > -1)
            {
                value = items[handle.index].value;
                return true;
            }
            value = default(T);
            return false;
        }

        public bool ContainsHandle(IHandle ihandle)
        {
            Handle handle = ValidateHandle(ihandle);
            return object.ReferenceEquals(handle.heap, this) && handle.index > -1;
        }

        public void Add(T value, ref IHandle handle)
        {
            if (handle == null)
                handle = new Handle();
            Add(value, handle);
        }

        public void Add(T value, IHandle ihandle)
        {
            if (size == items.Length)
            {
                int capacity = (int)((items.Length * 200L) / 100L);
                if (capacity < (items.Length + DEFAULT_CAPACITY))
                    capacity = items.Length + DEFAULT_CAPACITY;
                Array.Resize<HeapItem>(ref items, capacity);
            }

            Handle handle = null;
            if (ihandle != null)
            {
                handle = ValidateHandle(ihandle);
                handle.heap = this;
            }

            HeapItem item = new MinHeap<T>.HeapItem(value, handle);

            Set(item, size);
            BubbleUp(size++);
        }

        public void Add(T value)
        {
            Add(value, null);
        }

        public T Min()
        {
            if (size == 0)
                throw new InvalidOperationException("Heap is empty");

            return items[0].value;
        }

        public void Clear()
        {
            for (int index = 0; index < size; ++index)
                items[index].Clear();
            size = 0;
            if(items.Length > capacity)
                items = new HeapItem[capacity];
            ++version;
        }

        public void TrimExcess()
        {
            int length = (int)(items.Length * 0.9);
            if (size < length)
                Array.Resize<HeapItem>(ref items, Math.Min(size, capacity));
        }

        private void RemoveAt(int index)
        {
            if (size == 0)
                throw new InvalidOperationException("Heap is empty");
            if (index >= size)
                throw new ArgumentOutOfRangeException("index");

            items[index].Clear();
            if (--size > 0 && index != size)
            {
                Set(items[size], index);
                items[size].ClearRef();
                if (!BubbleUp(index))
                    BubbleDown(index);
            }
            if(size == 0 && items.Length > 4 * capacity)
                items = new HeapItem[capacity];
        }

        public T RemoveMin()
        {
            if (size == 0)
                throw new InvalidOperationException("Heap is empty");

            HeapItem item = items[0];
            RemoveAt(0);
            return item.value;
        }

        public T Remove(IHandle ihandle)
        {
            Handle handle = ValidateThisHandle(ihandle);
            HeapItem item = items[handle.index];
            RemoveAt(handle.index);
            return item.value;
        }

        private int GetIndex(T value)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            int index;

            for (index = 0; index < size; ++index)
            {
                if (comparer.Equals(items[index].value, value))
                    return index;
            }
            return -1;
        }

        public bool Contains(T value)
        {
            return GetIndex(value) != -1;
        }

        public bool Remove(T value)
        {
            int index = GetIndex(value);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Rank != 1)
                throw new ArgumentException("Multidimensional array not supported");
            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException("Non-zero lower bound array not supported");

            int length = array.Length;
            if ((index < 0) || (index > length))
                throw new ArgumentOutOfRangeException("index");
            if ((length - index) < this.size)
                throw new ArgumentException("Not enough space available in array starting at index");

            for (int i = 0; i < size; ++i)
                array[index + i] = items[i].value;
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (array.Rank != 1)
                throw new ArgumentException("Multidimensional array not supported");
            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException("Non-zero lower bound array not supported");

            int length = array.Length;
            if ((index < 0) || (index > length))
                throw new ArgumentOutOfRangeException("index");
            if ((length - index) < size)
                throw new ArgumentException("Not enough space available in array starting at index");

            try
            {
                for (int i = 0; i < size; ++i)
                    array.SetValue(items[i].value, index + i);
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException("Invalid array type");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            int cversion = version;

            for (int index = 0; index < size; ++index)
            {
                if (cversion != version)
                    throw new InvalidOperationException("Heap was modified while enumerating");
                yield return items[index].value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
