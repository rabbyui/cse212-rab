public static class TakingTurns
{
    private class Person
    {
        public string Name { get; set; }
        public int Turns { get; set; }

        public Person(string name, int turns)
        {
            Name = name;
            Turns = turns;
        }
    }

    private static Queue<Person> players = new Queue<Person>();
    public static void AddPerson(string name, int turns)
    {
        players.Enqueue(new Person(name, turns));
    }

    public static void GetNextPerson()
    {
        if (players.Count == 0)
        {
            // Queue is empty, so display an error message.
            Console.WriteLine("The queue is empty.");
            return;
        }

        var person = players.Dequeue();
        Console.WriteLine(person.Name);

        // Decrement turns only if the person doesn't have infinite turns
        if (person.Turns > 0)
        {
            person.Turns--;
        }

        // Re-enqueue the person if they have turns left
        if (person.Turns > 0)
        {
            players.Enqueue(person);
        }
    }

    public static void Test()
    {
        // Test 1
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3) and
        //           run until the queue is empty
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
        Console.WriteLine("Test 1");
        var players = new TakingTurnsQueue();
        players.AddPerson("Bob", 2);
        players.AddPerson("Tim", 5);
        players.AddPerson("Sue", 3);
        while (players.Length > 0)
            players.GetNextPerson();

        // Defect(s) Found: Turns are not decremented in the GetNextPerson method.

        Console.WriteLine("---------");

        // Test 2
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3)
        //           After running 5 times, add George with 3 turns.  Run until the queue is empty.
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
        Console.WriteLine("Test 2");
        players = new TakingTurnsQueue();
        players.AddPerson("Bob", 2);
        players.AddPerson("Tim", 5);
        players.AddPerson("Sue", 3);
        for (int i = 0; i < 5; i++)
        {
            players.GetNextPerson();
        }

        players.AddPerson("George", 3);
        while (players.Length > 0)
            players.GetNextPerson();

        // Defect(s) Found: Turns are not decremented in the GetNextPerson method.

        Console.WriteLine("---------");

        // Test 3
        // Scenario: Create a queue with the following people and turns: Bob (2), Tim (Forever), Sue (3)
        //           Run 10 times.
        // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
        Console.WriteLine("Test 3");
        players = new TakingTurnsQueue();
        players.AddPerson("Bob", 2);
        players.AddPerson("Tim", 0);
        players.AddPerson("Sue", 3);
        for (int i = 0; i < 10; i++)
        {
            players.GetNextPerson();
        }
        // Defect(s) Found: The case of a person with infinite turns is not handled correctly.

        Console.WriteLine("---------");
        
        // Test 4
        // Scenario: Try to get the next person from an empty queue
        // Expected Result: Error message should be displayed
        Console.WriteLine("Test 4");
        players = new TakingTurnsQueue();
        players.GetNextPerson();
        // Defect(s) Found: Error message for an empty queue is not checked.
    }
}
