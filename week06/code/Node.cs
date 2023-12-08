public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value) {
        // TODO Start Problem 2
        if (value == this.Data)
        {
            return true;
        }
        else if (value < this.Data && Left != null)
        {
            return Left.Contains(value);
        }
        else if (value > this.Data && Right != null)
        {
            return Right.Contains(value);
        }
        return false;
    }

    public int GetHeight() {
        // TODO Start Problem 4
        int leftHeight = (Left == null) ? 0 : Left.GetHeight();
        int rightHeight = (Right == null) ? 0 : Right.GetHeight();
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}