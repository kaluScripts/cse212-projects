public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Plan:
        // 1. Create a new double array of size 'length' to hold the results.
        // 2. Loop from index 0 to length - 1.
        // 3. At each index i, the multiple is number * (i + 1).
        //    e.g. index 0 → number * 1, index 1 → number * 2, etc.
        // 4. Store each computed multiple in the array at position i.
        // 5. Return the completed array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Plan:
        // Rotating right by 'amount' means the last 'amount' elements move to the front.
        // Example: {1,2,3,4,5,6,7,8,9} rotated right by 3 → {7,8,9,1,2,3,4,5,6}
        //   - The last 3 elements are {7, 8, 9}
        //   - The remaining first elements are {1, 2, 3, 4, 5, 6}
        //
        // Steps:
        // 1. Calculate the split index: data.Count - amount
        //    This is where the "tail" (elements to move to front) begins.
        // 2. Use GetRange to extract the tail: elements from splitIndex to end.
        // 3. Use GetRange to extract the head: elements from 0 to splitIndex.
        // 4. Clear the original list.
        // 5. AddRange the tail first, then the head.
        //    This produces the rotated result in-place.

        int splitIndex = data.Count - amount;

        List<int> tail = data.GetRange(splitIndex, amount);       // last 'amount' elements
        List<int> head = data.GetRange(0, splitIndex);            // remaining front elements

        data.Clear();
        data.AddRange(tail);   // tail goes first (moved to front)
        data.AddRange(head);   // head follows
    }
}
