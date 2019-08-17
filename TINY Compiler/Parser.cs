using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINY_Compiler
{ 
    public class Parser
    {
        private List<Token> tokens;
        private Hashtable symbolTable;

        private treeNode currentNode;
        private Token current_token;
        private static int counter = 0;


        public const int maxChilds = 3;
        public enum nodeKind
        {
            Stmt, Exp 
        }
        public enum stmtKind
        {
            If, Repeat, Assign, Read, Write
        }
        public enum expKind
        {
            Op, Const, Id
        }
        public enum expType
        {
            Void, Integer, Boolean
        }
        public class treeNode
        {
            public treeNode[] child;
            public treeNode sibling;
            public int? lineno;
            public nodeKind? nodekind;

            public stmtKind? stmtkind;
            public expKind? expkind;

            public Token? tokentype;
            public Int32? val;
            public string name;

            public expType? exptype;

            public treeNode()
            {
                child = new treeNode[maxChilds];

                sibling = null;
                lineno = null;
                nodekind = null;

                stmtkind = null;
                expkind = null;

                tokentype = null;
                val = null;
                name = "";

                exptype = null;

            }
        }
        public Parser()
        {       }
        public Parser(List<Token> _tokens)
        {
            this.tokens = _tokens;
        }

        private bool match(tokenType expected)
        {
            if(expected == current_token.token_Type)
            {
                current_token = getNextToken();
                return true;
            }
            else
            {
                error("Expected: " + expected.ToString() + ", found: " + current_token.token_Name.ToString());
                return false;
            }
        }
        private void error(string _error)
        {
            //throw new ContextMarshalException();
            Program.mainForm.error(_error);
        }
        private Token getNextToken()
        {
            if (counter < tokens.Count - 1)
            {
                return tokens[++counter];
            }
                
            else return new Token();
        }
        private Token previousToken()
        {
            return tokens[counter - 1];
        }

        public treeNode stmt_sequence()
        {
            treeNode temp_stmt_node = new treeNode(), new_temp_stmt_node = new treeNode();
            treeNode start = new treeNode();


            temp_stmt_node = statement();
            start = temp_stmt_node;
            while(current_token.token_Type == tokenType.SemiColon)
            {
                match(tokenType.SemiColon);
                new_temp_stmt_node = statement();
                temp_stmt_node.sibling = new_temp_stmt_node;
                temp_stmt_node = new_temp_stmt_node;
            }
            return start;
        }
        private treeNode statement()
        {
            treeNode temp_statement_node = new treeNode();

            switch(current_token.token_Type)
            {
                case tokenType.If:
                    temp_statement_node = if_stmt();
                    break;
                case tokenType.Repeat:
                    temp_statement_node = repeat_stmt();
                    break;
                case tokenType.ID:
                    temp_statement_node = assign_stmt();
                    break;
                case tokenType.Read:
                    temp_statement_node = read_stmt();
                    break;
                case tokenType.Write:
                    temp_statement_node = write_stmt();
                    break;
                default:
                    //error("");
                    break;
            }

            return temp_statement_node;


        }

        private treeNode write_stmt()
        {
            treeNode temp_write_stmt = new treeNode();
            temp_write_stmt.nodekind = nodeKind.Stmt;
            temp_write_stmt.stmtkind = stmtKind.Write;

            match(tokenType.Write);
            temp_write_stmt.child[0] = exp();

            return temp_write_stmt;
        }
        private treeNode read_stmt()
        {
            treeNode temp_read_stmt = new treeNode();
            temp_read_stmt.nodekind = nodeKind.Stmt;
            temp_read_stmt.stmtkind = stmtKind.Read;

            match(tokenType.Read);
            match(tokenType.ID);
            temp_read_stmt.name = previousToken().token_Name;

            return temp_read_stmt;
        }
        private treeNode assign_stmt()
        {
            treeNode temp_assign_stmt = new treeNode();
            temp_assign_stmt.nodekind = nodeKind.Stmt;
            temp_assign_stmt.stmtkind = stmtKind.Assign;

            match(tokenType.ID);
            temp_assign_stmt.name = previousToken().token_Name;
            match(tokenType.Assign);
            temp_assign_stmt.child[0] = exp();

            return temp_assign_stmt;
        }
        private treeNode repeat_stmt()
        {
            treeNode temp_repeat_stmt = new treeNode();
            temp_repeat_stmt.nodekind = nodeKind.Stmt;
            temp_repeat_stmt.stmtkind = stmtKind.Repeat;

            match(tokenType.Repeat);
            temp_repeat_stmt.child[0] = stmt_sequence();
            match(tokenType.Until);
            temp_repeat_stmt.child[1] = exp();

            return temp_repeat_stmt;
        }
        private treeNode if_stmt()
        {
            treeNode temp_if_stmt = new treeNode();
            temp_if_stmt.nodekind = nodeKind.Stmt;
            temp_if_stmt.stmtkind = stmtKind.If;

            match(tokenType.If);
            temp_if_stmt.child[0] = exp();
            match(tokenType.Then);
            temp_if_stmt.child[1] = stmt_sequence();
            if(current_token.token_Type == tokenType.Else)
            {
                match(tokenType.Else);
                temp_if_stmt.child[2] = stmt_sequence();
            }
            match(tokenType.End);

            return temp_if_stmt;
        }

        private treeNode exp()
        {
            treeNode temp_exp = new treeNode(), new_temp_exp = new treeNode();

            temp_exp = simple_exp();
            if(current_token.token_Type == tokenType.LessThan || current_token.token_Type == tokenType.Equal)
            {
                new_temp_exp.nodekind = nodeKind.Exp;
                new_temp_exp.expkind = expKind.Op;
                new_temp_exp.exptype = expType.Boolean;
                new_temp_exp.tokentype = current_token;

                match(current_token.token_Type);
                new_temp_exp.child[0] = temp_exp;
                new_temp_exp.child[1] = simple_exp();

                temp_exp = new_temp_exp;
            }

            return temp_exp;
        }
        private treeNode simple_exp()
        {
            treeNode temp_simple_exp = new treeNode(), new_temp_simple_exp = new treeNode();

            temp_simple_exp = term();
            while(current_token.token_Type == tokenType.Plus || current_token.token_Type == tokenType.Minus)
            {
                new_temp_simple_exp.nodekind = nodeKind.Exp;
                new_temp_simple_exp.expkind = expKind.Op;
                new_temp_simple_exp.exptype = expType.Integer;
                new_temp_simple_exp.tokentype = current_token;

                match(current_token.token_Type);
                new_temp_simple_exp.child[0] = temp_simple_exp;
                new_temp_simple_exp.child[1] = term();

                temp_simple_exp = new_temp_simple_exp;
            }

            return temp_simple_exp;
        }
        private treeNode term()
        {
            treeNode temp_term = new treeNode(), new_temp_term = new treeNode();

            temp_term = factor();
            while (current_token.token_Type == tokenType.Times)
            {
                new_temp_term.nodekind = nodeKind.Exp;
                new_temp_term.expkind = expKind.Op;
                new_temp_term.exptype = expType.Integer;
                new_temp_term.tokentype = current_token;

                match(current_token.token_Type);
                new_temp_term.child[0] = temp_term;
                new_temp_term.child[1] = factor();

                temp_term = new_temp_term;
            }

            return temp_term;

        }
        private treeNode factor()
        {
            treeNode temp_factor = new treeNode();
            
            switch(current_token.token_Type)
            {
                case tokenType.LeftParentheses:
                    match(tokenType.LeftParentheses);
                    temp_factor = exp();
                    match(tokenType.RightParentheses);
                    break;
                case tokenType.Num:
                    temp_factor.nodekind = nodeKind.Exp;
                    temp_factor.expkind = expKind.Const;
                    temp_factor.exptype = expType.Integer;
                    temp_factor.tokentype = current_token;
                    temp_factor.val = Int32.Parse(current_token.token_Name);
                    match(tokenType.Num);
                    break;
                case tokenType.ID:
                    temp_factor.nodekind = nodeKind.Exp;
                    temp_factor.expkind = expKind.Id;
                    temp_factor.exptype = expType.Integer;
                    temp_factor.tokentype = current_token;
                    temp_factor.name = current_token.token_Name;
                    match(tokenType.ID);
                    break;
                default:
                    //error("");
                    break;

            }

            return temp_factor;
        }

        public treeNode Parse()
        {
            treeNode TreeHead = new treeNode();
            current_token = tokens[0];
            counter = 0;

            TreeHead = stmt_sequence();
            return TreeHead;

            
        }


    }
}
