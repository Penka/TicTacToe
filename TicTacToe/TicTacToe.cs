using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static int InputX(int n)
        {
            string input;
            int x;
            do
            {
                Console.Write("row = ");
                input = Console.ReadLine();

            }
            while (!(int.TryParse(input, out x)) || (x < 0 || x > n - 1));
            return x;
        }
        static int InputY(int n)
        {
            string input;
            int y;
            do
            {
                Console.Write("column = ");
                input = Console.ReadLine();
            }
            while (!(int.TryParse(input, out y)) || (y < 0 || y > n - 1));
            return y;
        }
        static void PrintBoard(Node[] nodes, int n)
        {
            char[,] board = new char[n, n];
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    board[nodes[i].GetX(), nodes[i].GetY()] = nodes[i].Symbol();
                }
            }
            string line = new string('-', 4 * n + 2);
            Console.WriteLine(line);
            for (int i = 0; i < n; i++)
            {

                Console.Write("|");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(" " + board[i, j] + " |");
                }
                Console.WriteLine("\n" + line);
            }
        }

        static List<Node> GenerateAllMoves(Node[] nodes, int n, bool player)
        {
            List<Node> moves = new List<Node>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    bool valid = true;
                    for (int k = 0; k < n * n; k++)
                    {
                        if (nodes[k] == null)
                        {
                            continue;
                        }
                        if (nodes[k].GetX() == i && nodes[k].GetY() == j)
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid)
                    {
                        Node move = new Node(i, j, !player);
                        moves.Add(move);
                    }
                }
            }
            return moves;
        }

        static Node[] MinMax(Node[] nodes, int n, bool player)
        {
            Node bestNode;
            int move = Max(nodes, n, player, out  bestNode);
            if (bestNode == null)
            {
                return nodes;
            }
            Node node = new Node(bestNode.GetX(), bestNode.GetY(), !player);
            nodes = AddNode(nodes, n, node);
            return nodes;
        }

        static Node[] AddNode(Node[] nodes, int n, Node node)
        {
            Node[] newNodes = new Node[n * n];
            bool found = false;
            for (int i = 0; i < n * n; i++)
            {
                if (nodes[i] == null && !found)
                {
                    newNodes[i] = node;
                    found = true;
                    continue;
                }
                newNodes[i] = nodes[i];
            }
            return newNodes;
        }

        static int Max(Node[] nodes, int n, bool player, out Node bestMove)
        {
            if (GameOver(nodes, n, player) != null)
            {
                bestMove = null;
                return Value(nodes, n, player);
            }
            else
            {
                List<Node> moves = GenerateAllMoves(nodes, n, player);
                bestMove = moves[0];
                int length = moves.Count;
                int m = int.MinValue;
                for (int i = 0; i < length; i++)
                {
                    Node move = moves[i]; ;
                    Node[] newNodes = new Node[n * n];
                    newNodes = AddNode(nodes, n, move);
                    int moveValue = -Max(newNodes, n, !player, out bestMove);
                    if (moveValue > m)
                    {
                        m = moveValue;
                        bestMove = move;
                    }
                }
                return m;
            }
        }

        static int Value(Node[] nodes, int n, bool player)
        {
            if (GameOver(nodes, n, player) == null)
            {
                return 0;
            }
            else return (int)GameOver(nodes, n, player);
        }

        static bool CheckVerticals(Node[] nodes, int n, char symbol)
        {
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < n * n; j++)
                {
                    if (nodes[j] == null)
                    {
                        continue;
                    }
                    if (nodes[j].GetX() == i && nodes[j].Symbol() == symbol)
                    {
                        count++;
                    }
                }
                if (count == 3)
                {
                    return true;
                }
            }
            return false;
        }
        static bool CheckHorizontals(Node[] nodes, int n, char symbol)
        {
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < n * n; j++)
                {
                    if (nodes[j] == null)
                    {
                        continue;
                    }
                    if (nodes[j].GetY() == i && nodes[j].Symbol() == symbol)
                    {
                        count++;
                    }
                }
                if (count == 3)
                {
                    return true;
                }
            }
            return false;
        }
        static bool CheckLeftDiagonal(Node[] nodes, int n, char symbol)
        {
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < n * n; j++)
                {
                    if (nodes[j] == null)
                    {
                        continue;
                    }
                    if (nodes[j].GetY() == i && nodes[j].GetX() == i)
                    {
                        if (nodes[j].Symbol() == symbol)
                        {
                            count++;
                        }

                    }

                }
                if (count == 3)
                {
                    return true;
                }
            }
            return false;
        }
        static bool CheckRightDiagonal(Node[] nodes, int n, char symbol)
        {
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < n * n; j++)
                {
                    if (nodes[j] == null)
                    {
                        continue;
                    }
                    if (nodes[j].GetY() == (n - i - 1) && nodes[j].GetX() == i && nodes[j].Symbol() == symbol)
                    {
                        count++;
                    }
                }
                if (count == 3)
                {
                    return true;
                }
            }
            return false;
        }

        static bool ValidMove(Node[] nodes, int n, int x, int y)
        {
            for (int i = 0; i < n * n; i++)
            {
                if (nodes[i] != null)
                {
                    if (nodes[i].GetX() == x && nodes[i].GetY() == y)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool FullBoard(Node[] nodes, int n)
        {
            for (int i = 0; i < n * n; i++)
            {
                if (nodes[i] == null)
                {
                    return false;
                }
            }
            return true;
        }

        static int? GameOver(Node[] nodes, int n, bool player)
        {
            char symbol;
            if (player)
            {
                symbol = 'X';
            }
            else
            {
                symbol = 'O';
            }
            if (CheckHorizontals(nodes, n, symbol) || CheckVerticals(nodes, n, symbol) 
                || CheckLeftDiagonal(nodes, n, symbol) || CheckRightDiagonal(nodes, n, symbol))
            {
                if (player)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            if (FullBoard(nodes, n))
            {
                return 0;
            }

            return null;
        }

        static void Main(string[] args)
        {
            int n = 3;
            bool player = false;
            Node[] nodes = new Node[n * n];
            int i = 0;
            while (true)
            {
                PrintBoard(nodes, n);
                if (GameOver(nodes, n, player) != null)
                {
                    break;
                }
                if (!player)
                {
                    int x;
                    int y;
                    do
                    {
                        x = InputX(n);
                        y = InputY(n);
                    }
                    while (!ValidMove(nodes, n, x, y));
                    nodes[i] = new Node(x, y, player);
                }
                else
                {
                    nodes = MinMax(nodes, n, !player);
                }
                player = !player;
                i++;
                if (GameOver(nodes, n, player) != null)
                {
                    break;
                }
                Console.Clear();

            }
            Console.WriteLine("Game Over!");
        }
    }
}
