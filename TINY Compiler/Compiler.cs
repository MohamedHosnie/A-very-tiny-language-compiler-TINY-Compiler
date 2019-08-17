using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINY_Compiler
{
    public class Compiler
    {
        public Scanner scanner;
        public Parser parser;
        public Compiler()
        {
            
        }
        public Compiler(string _buffer)
        {
            this.scanner = new Scanner(_buffer);
            this.parser = new Parser(this.getTokens());
        }
        public void Scan(string _buffer)
        {
            this.scanner = new Scanner(_buffer);
            this.scanner.Scan();
        }
        public Parser.treeNode Parse()
        {
            this.parser = new Parser(this.getTokens());
            Parser.treeNode tree = this.parser.Parse();
            return tree;
        }

        public List<Token> getTokens()
        {
            return this.scanner.getTokens();
        }
        public List<Token> getErrors()
        {
            return this.scanner.getErrors();
        }
        public List<Token> getComments()
        {
            return this.scanner.getComments();
        }
        public Hashtable getSymbolTable()
        {
            return this.scanner.getSymbolTable();
        }
        

    }
}
