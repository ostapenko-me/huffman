using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HuffmanEncode
{
    public class Node
    {
        public char Symbol;
        public long Count = 0;
        public Node Left = null;
        public Node Right = null;
        public bool Terminal = false;

        public List<bool> BuildCode(char symbol, List<bool> code)
        {
            if (Terminal)
            {
                if (Symbol == symbol)
                {
                    if (code.Count == 0)
                    {
                        code.Add(false);
                    }
                    return code;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> LeftCode = new List<bool>(code);
                List<bool> RightCode = new List<bool>(code);
                LeftCode.Add(true);
                RightCode.Add(false);
                LeftCode = Left?.BuildCode(symbol, LeftCode);
                RightCode = Right?.BuildCode(symbol, RightCode);

                if (LeftCode != null)
                {
                    return LeftCode;
                }
                else
                {
                    return RightCode;
                }
            }
        }
    }

    public class HuffmanEncoding
    {
        public Dictionary<char, List<bool>> CodeTable = new Dictionary<char, List<bool>>();

        public void SetEncoding(string input)
        {
            List<Node> nodes = new List<Node>();
            Dictionary<char, long> Quantities = new Dictionary<char, long>();
            Node Root;
            foreach (char ch in input)
            {
                if (!Quantities.ContainsKey(ch))
                {
                    Quantities.Add(ch, 0);
                }
                Quantities[ch]++;
            }
            foreach (KeyValuePair<char, long> element in Quantities)
            {
                nodes.Add(new Node() { Symbol = element.Key, Count = element.Value, Terminal = true });
            }
            while (nodes.Count != 1)
            {
                nodes = nodes.OrderBy(node => node.Count).ToList();
                Node node1 = nodes[0];
                Node node2 = nodes[1];
                Node newNode = new Node();
                newNode.Count = node1.Count + node2.Count;
                newNode.Left = node1;
                newNode.Right = node2;
                nodes.RemoveRange(0, 2);
                nodes.Add(newNode);
            }
            Root = nodes[0];

            foreach (char ch in Quantities.Keys)
            {
                CodeTable.Add(ch, Root.BuildCode(ch, new List<bool>()));
            }
        }

        public BitArray Encode(string input)
        {
            List<bool> code = new List<bool>();
            for (int i = 0; i < input.Length; i++)
            {
                code.AddRange(CodeTable[input[i]]);
            }
            return new BitArray(code.ToArray());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку, которую вы хотите закодировать: ");
            string input = Console.ReadLine();

            HuffmanEncoding huffmanEncoding = new HuffmanEncoding();
            huffmanEncoding.SetEncoding(input);

            BitArray result = huffmanEncoding.Encode(input);
            Console.WriteLine("\nДлина исходной строки в битах - {0} бит", input.Length*8);
            Console.WriteLine("Длина задодированной строки в битах - {0} бит", result.Count);            
            Console.WriteLine("Закодированная строка: \n");
            for (int i=0; i<result.Count; i++)
            {
                Console.Write(result[i]?1:0);
                if ((i + 1) % 4 == 0)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

}
