public class UpdatedPriorityQueue
{
    private class PriorityItem
    {
        public string Data { get; }
        public int Priority { get; }

        public PriorityItem(string data, int priority)
        {
            Data = data;
            Priority = priority;
        }
    }
    private List<PriorityItem> queue = new List<PriorityItem>();

    public void Enqueue(string data, int priority)
    {
        PriorityItem newItem = new PriorityItem(data, priority);
        queue.Add(newItem);
    }

    public string Dequeue()
    {
        if (queue.Count == 0)
        {
            Console.WriteLine("Error: Queue is empty.");
            return null;
        }

        PriorityItem highestPriorityItem = queue.OrderBy(item => item.Priority).First();
        queue.Remove(highestPriorityItem);

        return highestPriorityItem.Data;
    }
    public override string ToString()
    {
        return "[" + string.Join(", ", queue.Select(item => $"{item.Data}({item.Priority})")) + "]";
    }
}
public static class Priority
{
    public static void Test()
    {
        var priorityQueue = new UpdatedPriorityQueue();
        Console.WriteLine(priorityQueue);
        // Test Cases

        // Test 1
        // Scenario: Enqueue items with different priorities and dequeue them.
        // Expected Result: Items should be dequeued in descending order of priority.
        Console.WriteLine("Test 1");
        priorityQueue.Enqueue("Item1", 3);
        priorityQueue.Enqueue("Item2", 1);
        priorityQueue.Enqueue("Item3", 2);
        Console.WriteLine("Dequeued: " + priorityQueue.Dequeue()); // Expecting Item2
        Console.WriteLine("Dequeued: " + priorityQueue.Dequeue()); // Expecting Item3
        Console.WriteLine("Dequeued: " + priorityQueue.Dequeue()); // Expecting Item1
        Console.WriteLine("---------");

        // Test 2
        // Scenario: Enqueue items with the same priority and dequeue them.
        // Expected Result: Items should be dequeued in the order they were enqueued.
        Console.WriteLine("Test 2");
        priorityQueue.Enqueue("ItemA", 2);
        priorityQueue.Enqueue("ItemB", 2);
        priorityQueue.Enqueue("ItemC", 2);
        Console.WriteLine("Dequeued: " + priorityQueue.Dequeue()); // Expecting ItemA
        Console.WriteLine("Dequeued: " + priorityQueue.Dequeue()); // Expecting ItemB
        Console.WriteLine("Dequeued: " + priorityQueue.Dequeue()); // Expecting ItemC
        Console.WriteLine("---------");

        // Test 3
        // Scenario: Try to dequeue from an empty queue.
        // Expected Result: Error message should be displayed.
        Console.WriteLine("Test 3");
        Console.WriteLine("Dequeued: " + priorityQueue.Dequeue()); // Expecting an error message
        Console.WriteLine("---------");

    }
}
