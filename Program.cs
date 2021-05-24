using System;

namespace GenericStack
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CstmStack<int> MyStack = new CstmStack<int>();
            // Stack myStack = new Stack();

            MyStack.Push(1);
            MyStack.Push(2);
            MyStack.Push(3);

            MyStack.ShowItems();

            //------------------------------------------

            // Проверяем работу перечислителя
            Console.WriteLine("----foreach-1:");

            foreach (int Item in MyStack)
            {
                Console.WriteLine($"Item: {Item}");
            }

            //------------------------------------------
            CstmStack<char> MyCharStack = new CstmStack<char>();

            MyCharStack.Push('A');
            MyCharStack.Push('B');
            MyCharStack.Push('C');

            Console.WriteLine("----foreach-Char:");
            foreach (Char ItemChar in MyCharStack)
            {
                Console.WriteLine($"Item: {ItemChar}");
            }
            //------------------------------------------

            Console.ReadLine();
        }
    }
}
