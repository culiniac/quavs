using System;
using System.Collections.Generic;
using System.Text;

namespace QUAVS.Base
{
    class ByteAppend<T> : IEnumerable<T>
    {
        LinkedList<T[]> m_lstItems = new LinkedList<T[]>();
        int m_nCount;

        public int Count
        {
            get
            {
                return m_nCount;
            }
        }

        public T[] ToArray()
        {
            T[] aTemp = new T[Count];
            for (int nIndex = 0; nIndex < Count; nIndex++)
            {
                aTemp[nIndex] = this[nIndex];
            }
            return aTemp;
        }

        public void Add(T[] aItems)
        {
            if (aItems == null)
                return;
            if (aItems.Length == 0)
                return;

            m_lstItems.AddLast(aItems);
            m_nCount += aItems.Length;
        }

        private T[] GetItemIndex(int nRealIndex, out int nOffset)
        {
            nOffset = 0;
            int nCurrentStart = 0;

            foreach (T[] items in m_lstItems)
            {
                nCurrentStart += items.Length;
                if (nCurrentStart > nRealIndex)
                    return items;
                nOffset = nCurrentStart;
            }
            return null;
        }

        public T this[int nIndex]
        {
            get
            {
                int nOffset;
                T[] i = GetItemIndex(nIndex, out nOffset);
                return i[nIndex - nOffset];
            }
            set
            {
                int nOffset;
                T[] i = GetItemIndex(nIndex, out nOffset);
                i[nIndex - nOffset] = value;
            }
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T[] items in m_lstItems)
                foreach (T item in items)
                    yield return item;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

}
