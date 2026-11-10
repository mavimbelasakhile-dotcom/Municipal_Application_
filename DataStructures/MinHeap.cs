namespace AdvancedProgrammingAss1.DataStructures
{
    /// <summary>
    /// A binary min-heap of service requests ordered by priority (1 = Critical is
    /// smallest, therefore surfaces first). Used to always pull the most urgent
    /// outstanding request in O(log n), and to produce a priority-ordered list.
    /// </summary>
    public class MinHeap
    {
        private readonly List<ServiceRequest> heap = new List<ServiceRequest>();

        public int Count => heap.Count;

        /// <summary>Lower key = higher urgency. Ties broken by earliest date.</summary>
        private bool Less(ServiceRequest a, ServiceRequest b)
        {
            if (a.Priority != b.Priority) return a.Priority < b.Priority;
            return a.DateSubmitted < b.DateSubmitted;
        }

        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            SiftUp(heap.Count - 1);
        }

        /// <summary>Returns (without removing) the most urgent request.</summary>
        public ServiceRequest? Peek() => heap.Count > 0 ? heap[0] : null;

        /// <summary>Removes and returns the most urgent request.</summary>
        public ServiceRequest? ExtractMin()
        {
            if (heap.Count == 0) return null;

            ServiceRequest min = heap[0];
            ServiceRequest last = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
            {
                heap[0] = last;
                SiftDown(0);
            }
            return min;
        }

        /// <summary>
        /// Returns every request in priority order without mutating the heap,
        /// by draining a disposable copy.
        /// </summary>
        public List<ServiceRequest> ToSortedList()
        {
            var copy = new MinHeap();
            foreach (var r in heap) copy.Insert(r);

            var result = new List<ServiceRequest>();
            while (copy.Count > 0)
                result.Add(copy.ExtractMin()!);
            return result;
        }

        private void SiftUp(int i)
        {
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (Less(heap[i], heap[parent]))
                {
                    Swap(i, parent);
                    i = parent;
                }
                else break;
            }
        }

        private void SiftDown(int i)
        {
            int n = heap.Count;
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int smallest = i;

                if (left < n && Less(heap[left], heap[smallest])) smallest = left;
                if (right < n && Less(heap[right], heap[smallest])) smallest = right;

                if (smallest == i) break;
                Swap(i, smallest);
                i = smallest;
            }
        }

        private void Swap(int a, int b)
        {
            (heap[a], heap[b]) = (heap[b], heap[a]);
        }
    }
}
