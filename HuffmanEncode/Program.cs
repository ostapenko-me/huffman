using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
