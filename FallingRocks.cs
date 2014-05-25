using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

struct Rocks
{
    public int x;
    public int y;
    public char c;
    public string sign;
    public ConsoleColor color;
}


class FallingRocks
{

    static void Main()
    {
        //Declare general signs, dimensions etc.
        char[] rocksSymbols = { '@', '*', '&', '+', '%', '$', '#', '!', '.', ';' };
        ConsoleColor[] colors = { ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.Yellow };
        Console.BufferHeight = Console.WindowHeight = 30;
        Console.BufferWidth = Console.WindowWidth = 50;
        int life = 3;
        int score = 0;
        int speed = 0;
        int level = 1;
        //Declare dwarf
        Rocks dwarf = new Rocks();
        dwarf.x = Console.WindowWidth / 2;
        dwarf.y = Console.WindowHeight - 2;
        dwarf.sign = "(0)";
        dwarf.color = ConsoleColor.White;
        //Random generator
        Random randomGenerator = new Random();
        //Declare list of rocks on line0
        List<Rocks> listOfFallingRocks = new List<Rocks>();



        while (true)
        {
            //Print dwarf on the last row
            Console.SetCursorPosition(dwarf.x, dwarf.y);
            Console.ForegroundColor = dwarf.color;
            Console.Write(dwarf.sign);
            Console.ForegroundColor = ConsoleColor.Gray;

            //Generate rocks and print them on line 0
            for (int i = 0; i < randomGenerator.Next(0, 4); i++)
            {
                Rocks fallingRocks = new Rocks();
                fallingRocks.x = randomGenerator.Next(0, Console.WindowWidth);
                fallingRocks.y = 0;
                fallingRocks.c = rocksSymbols[randomGenerator.Next(0, 10)];
                fallingRocks.color = colors[randomGenerator.Next(0, 7)];
                listOfFallingRocks.Add(fallingRocks);
                Console.SetCursorPosition(fallingRocks.x, fallingRocks.y);
                Console.ForegroundColor = fallingRocks.color;
                Console.Write(fallingRocks.c);
                Console.ForegroundColor = ConsoleColor.Gray;


            }

            //User's imput
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo dwarfMovement = Console.ReadKey();
                //Smooth dwarf movement
                while (Console.KeyAvailable) Console.ReadKey();
                {

                }
                if (dwarfMovement.Key == ConsoleKey.LeftArrow)
                {
                    if (dwarf.x - 1 >= 0)
                    {
                        dwarf.x--;
                    }
                }
                if (dwarfMovement.Key == ConsoleKey.RightArrow)
                {
                    if (dwarf.x + 1 < Console.WindowWidth - 2)
                    {
                        dwarf.x++;
                    }
                }

            }
            Thread.Sleep(150 - speed);
            Console.Clear();

            //Move rocks one line down
            List<Rocks> listOfRocksDown = new List<Rocks>();

            for (int j = 0; j < listOfFallingRocks.Count; j++)
            {
                Rocks oldRock = listOfFallingRocks[j];
                Rocks newRock = new Rocks();
                newRock.x = oldRock.x;
                newRock.y = oldRock.y + 1;
                newRock.c = oldRock.c;
                newRock.color = oldRock.color;

                if ((newRock.x == dwarf.x && newRock.y == dwarf.y) || (newRock.x == dwarf.x + 1 && newRock.y == dwarf.y) || (newRock.x == dwarf.x + 2 && newRock.y == dwarf.y))
                {

                    life--;
                    Console.SetCursorPosition(dwarf.x, dwarf.y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    listOfFallingRocks.Clear();
                    Console.Beep();
                    Console.Write("X");
                    Thread.Sleep(300);
                    Console.ForegroundColor = ConsoleColor.Gray;

                }
                if (life == 0)
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Game over!Score->{0}.Lev->{1} ", score, level);
                    Console.SetCursorPosition(28, Console.WindowHeight - 1);
                    Console.Write("Press [enter] to exit");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                if (newRock.y < Console.WindowHeight - 1)
                {
                    listOfRocksDown.Add(newRock);
                }


            }
            //Print rocks on lines1+
            listOfFallingRocks = listOfRocksDown;
            foreach (Rocks rock1 in listOfFallingRocks)
            {
                Console.SetCursorPosition(rock1.x, rock1.y);
                Console.ForegroundColor = rock1.color;
                Console.Write(rock1.c);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            //Print additional info
            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Life -->{0} Score-->{1} Level-->{2}, Speed-->{3}", life, score, level, 150 + speed);
            Console.ForegroundColor = ConsoleColor.Gray;

            score++;
            if (score / 200 != (score - 1) / 200)
            {
                level = level + 1;
            }

            speed = 10 * (score / 200);


        }
    }
}
