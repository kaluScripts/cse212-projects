using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with distinct priorities (low=1, medium=2, high=3)
    //           and dequeue all three.
    // Expected Result: Items come out highest priority first: "High" (3), "Medium" (2), "Low" (1).
    // Defect(s) Found: 
    //   1. Loop used `index < _queue.Count - 1`, so the last item was never considered for
    //      highest priority.
    //   2. `_queue.RemoveAt(highPriorityIndex)` was missing, so items were never removed
    //      from the internal list (Dequeue returned the correct value but left the queue unchanged).
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 2);
        priorityQueue.Enqueue("High", 3);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items where the highest priority item is at the back of the queue.
    // Expected Result: "Last" (priority 5) is dequeued first, even though it was added last.
    // Defect(s) Found:
    //   1. Same off-by-one bug: `Count - 1` meant the last element was excluded from the
    //      priority search, so "Last" would never be selected even though it has the highest
    //      priority — revealing the loop boundary bug.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Last", 5);

        Assert.AreEqual("Last", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two items with identical priority values. Dequeue both.
    // Expected Result: "Alpha" is returned before "Beta" because FIFO ordering applies
    //                  when priorities are equal (first-in, first-out among ties).
    // Defect(s) Found:
    //   The original loop used `>=` for the comparison, which advanced highPriorityIndex
    //   forward on every tie — effectively returning the *last* enqueued item among equals
    //   instead of the first. Changing to strictly `>` preserves FIFO order on ties.
    public void TestPriorityQueue_3_TieBreakerFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Alpha", 3);
        priorityQueue.Enqueue("Beta", 3);

        Assert.AreEqual("Alpha", priorityQueue.Dequeue());
        Assert.AreEqual("Beta", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException is thrown with message "The queue is empty."
    // Defect(s) Found: None — this guard was already correct in the original code.
    public void TestPriorityQueue_4_EmptyQueueThrows()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception type {e.GetType()}: {e.Message}");
        }
    }

    [TestMethod]
    // Scenario: Enqueue one item and dequeue it; verify the queue is then empty and
    //           a second dequeue throws.
    // Expected Result: First dequeue returns "Only"; second dequeue throws InvalidOperationException.
    // Defect(s) Found:
    //   Without the RemoveAt fix the item was never removed, so the queue would never become
    //   empty and a second dequeue would silently return "Only" again instead of throwing.
    public void TestPriorityQueue_5_ItemActuallyRemoved()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Only", 10);

        Assert.AreEqual("Only", priorityQueue.Dequeue());

        // Queue must now be empty; next dequeue must throw
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown after queue emptied.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}
