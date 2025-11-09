namespace PROG7312_POE.Models.DataStructures
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private List<T> _elements = new();

        public int Count => _elements.Count;
        private int GetParent(int i) => (i - 1) / 2;
        private int LeftChild(int i) => (2 * i) + 1;
        private int RightChild(int i) => (2 * i) + 2;

        public void Add(T value)
        {
            _elements.Add(value);
            HeapifyUp(_elements.Count - 1);
        }

        public T Peek() => _elements.First();
        public T Pop()
        {
            var result = _elements.First();
            _elements[0] = _elements.Last();
            _elements.RemoveAt(_elements.Count - 1);
            HeapifyDown(0);
            return result;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0 && _elements[index].CompareTo(_elements[GetParent(index)]) < 0)
            {
                (_elements[index], _elements[GetParent(index)]) = (_elements[GetParent(index)], _elements[index]);
                index = GetParent(index);
            }
        }

        private void HeapifyDown(int index)
        {
            int smallest = index;
            int left = LeftChild(index);
            int right = RightChild(index);

            if (left < Count && _elements[left].CompareTo(_elements[smallest]) < 0) smallest = left;
            if (right < Count && _elements[right].CompareTo(_elements[smallest]) < 0) smallest = right;

            if (smallest != index)
            {
                (_elements[index], _elements[smallest]) = (_elements[smallest], _elements[index]);
                HeapifyDown(smallest);
            }
        }

        public List<T> ToList() => new List<T>(_elements);
    }
}
