namespace CircularQueue
{
    public interface IQueue<T>
    {
        public void Enqueue(T value);

        public T Peek();

        public T Dequeue();
    }
}