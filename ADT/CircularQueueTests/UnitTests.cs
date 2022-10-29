using System;
using System.Collections.Generic;
using CircularQueue;
using Xunit;

namespace CircularQueueTests
{
    public class UnitTests
    {
        [Fact]
        public void Initialize_NegativeCapacity_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            CircularQueue<object> Initialization() => new(-1);
        
            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(Initialization);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        public void Initialize_NonNegativeCapacity_SetsCapacity(int capacity)
        {
            // Arrange, Act
            var queue = new CircularQueue<object>(capacity);
            var actual = queue.Capacity;
            
            // Assert
            Assert.Equal(capacity, actual);
        }
        
        [Fact]
        public void Initialize_NullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            CircularQueue<object> Initialization() => new(null);
        
            // Act, Assert
            Assert.Throws<ArgumentNullException>(Initialization);
        }
        
        [Theory]
        [InlineData(new int[] { }, 0)]
        [InlineData(new[] {0, 1}, 2)]
        public void Initialize_IntArray_SetsItems(IEnumerable<int> collection, int expected)
        {
            // Arrange, Act
            var queue = new CircularQueue<int>(collection);
            var actual = queue.Count;
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        public void Enqueue_MultipleTimes_CountChanged(int itemsNumber)
        {
            // Arrange
            var queue = new CircularQueue<int>();
        
            // Act
            for (var i = 0; i < itemsNumber; i++)
            {
                queue.Enqueue(i);
            }
            
            var actual = queue.Count;
        
            // Assert
            Assert.Equal(itemsNumber, actual);
        }
        
        [Theory]
        [InlineData(8, 16)]
        [InlineData(16, 32)]
        public void Enqueue_MultipleTimes_CapacityChanged(int itemsNumber, int expected)
        {
            // Arrange
            var queue = new CircularQueue<int>();
            
            // Act
            for (var i = 0; i < itemsNumber; i++)
            {
                queue.Enqueue(i);
            }
        
            var actual = queue.Capacity;
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Peek_EmptyQueue_ThrowsInvalidOperationException()
        {
            // Arrange
            var queue = new CircularQueue<object>();
            
            // Act, Assert
            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }
        
        [Fact]
        public void Peek_MultipleTimes_ReturnsSameItem()
        {
            // Arrange
            var queue = new CircularQueue<string>();
            const string expected = "Item";
            queue.Enqueue(expected);
        
            // Act, Assert
            for (var i = 0; i < 2; i++)
            {
                var actual = queue.Peek();
                Assert.Same(expected, actual);
            }
        }
        
        [Fact]
        public void Dequeue_EmptyQueue_ThrowsInvalidOperationException()
        {
            // Arrange
            var queue = new CircularQueue<object>();
            
            // Act, Assert
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }
        
        [Fact]
        public void Dequeue_MultipleTimes_PreservesOrder()
        {
            // Arrange
            var random = new Random();
            var queue = new CircularQueue<int>();
            
            const int itemsNumber = 10000;
            const int maxNumber = 10000;
            var expected = new int[itemsNumber];
            for (var i = 0; i < itemsNumber; i++)
            {
                var randomValue = random.Next(maxNumber);
                queue.Enqueue(randomValue);
                expected[i] = randomValue;
            }
            
            // Act
            var actual = new int[itemsNumber];
            for (var i = 0; i < itemsNumber; i++)
            {
                actual[i] = queue.Dequeue();
            }
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(4)]
        [InlineData(7)]
        public void EnqueueDequeue_DifferentOperationsOrder_PreservesOrder(int itemsNumber)
        {
            // Arrange
            var headFirstQueue = new CircularQueue<int>();
            var tailFirstQueue = new CircularQueue<int>();
            var headFirstResult = new int[itemsNumber];
            var tailFirstResult = new int[itemsNumber];
            
            // Act
            for (var i = 1; i <= itemsNumber; i++)
            {
                headFirstQueue.Enqueue(i);
            }
        
            for (var i = 0; i < itemsNumber; i++)
            {
                headFirstResult[i] = headFirstQueue.Dequeue();
            }
            
            for (var i = itemsNumber; i > 1; i--)
            {
                tailFirstQueue.Enqueue(i);
            }
            
            for (var i = 0; i < itemsNumber - 1; i++)
            {
                tailFirstQueue.Dequeue();
            }
            
            for (var i = 1; i <= itemsNumber; i++)
            {
                tailFirstQueue.Enqueue(i);
            }
            
            for (var i = 0; i < itemsNumber; i++)
            {
                tailFirstResult[i] = tailFirstQueue.Dequeue();
            }
            
            // Assert
            Assert.Equal(headFirstResult, tailFirstResult);
        }
        
    }
}