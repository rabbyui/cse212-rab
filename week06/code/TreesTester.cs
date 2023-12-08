public static class TreesTester {
    /// <summary>
    /// Entry point for the Prove 9 tests
    /// </summary>
    public static void Run() {
        // Sample Test Cases (may not be comprehensive)
        Console.WriteLine("\n=========== PROBLEM 1 TESTS ===========");
        BinarySearchTree tree = new BinarySearchTree();
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(7);
        
        // TODO Problem 1
        tree.Insert(7);
        tree.Insert(4);
        tree.Insert(10);
        tree.Insert(1);
        tree.Insert(6);
        Console.WriteLine(tree.ToString()); // 1, 3, 4, 5, 6, 7, 10

        Console.WriteLine("\n=========== PROBLEM 2 TESTS ===========");
        Console.WriteLine(tree.Contains(3)); // True
        Console.WriteLine(tree.Contains(2)); // False
        Console.WriteLine(tree.Contains(7)); // True
        Console.WriteLine(tree.Contains(6)); // True
        Console.WriteLine(tree.Contains(9)); // False

        Console.WriteLine("\n=========== PROBLEM 3 TESTS ===========");
        foreach (var value in tree.Reverse()) {
            Console.WriteLine(value); // 10, 7, 6, 5, 4, 3, 1
        }

        Console.WriteLine("\n=========== PROBLEM 4 TESTS ===========");
        Console.WriteLine(tree.GetHeight()); // 3
        tree.Insert(6);
        Console.WriteLine(tree.GetHeight()); // 3
        tree.Insert(12);
        Console.WriteLine(tree.GetHeight()); // 4

        Console.WriteLine("\n=========== PROBLEM 5 TESTS ===========");
        var tree1 = CreateTreeFromSortedList(new[] { 10, 20, 30, 40, 50, 60 });
        var tree2 = CreateTreeFromSortedList(Enumerable.Range(0, 127).ToArray()); // 2^7 - 1 nodes
        var tree3 = CreateTreeFromSortedList(Enumerable.Range(0, 128).ToArray()); // 2^7 nodes
        var tree4 = CreateTreeFromSortedList(new[] { 42 });
        var tree5 = CreateTreeFromSortedList(Array.Empty<int>());
        Console.WriteLine(tree1.GetHeight()); // 3
        Console.WriteLine(tree2.GetHeight()); // 7 .. any higher and its not balanced
        Console.WriteLine(tree3.GetHeight()); // 8 .. any higher and its not balanced
        Console.WriteLine(tree4.GetHeight()); // 1
        Console.WriteLine(tree5.GetHeight()); // 0
    }

    private static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers) {
        var bst = new BinarySearchTree(); // Create an empty BST to start with 
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst) {
        // TODO Start Problem 5
        if (first <= last) {
            int middleIndex = (first + last) / 2;
            int middleValue = sortedNumbers[middleIndex];
            bst.Insert(middleValue);

            InsertMiddle(sortedNumbers, first, middleIndex - 1, bst);
            InsertMiddle(sortedNumbers, middleIndex + 1, last, bst);
        }
        
    }
}