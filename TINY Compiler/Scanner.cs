using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINY_Compiler
{
    public enum State
    {
        START, INCOMMENT,
        INNUM, INID, INASSIGN,
        DONEOP, DONENUM,
        DONEID, DONEASSIGN,
        DONECOMMENT, ERROR,
        DONEERROR
    }
    public enum Input
    {
        COMMENT_LEFT,
        COMMENT_RIGHT,
        ASSIGN_COL,
        EQUAL, DIGIT,
        LETTER, SYMBOL,
        DELIMETER, OTHER
    }
    public enum tokenType
    {
        EndFile, Error, Comment, None,
        If, Then, Else, End, Repeat, Until, Read, Write, //Reserved Words
        ID, Num, //multicharacher tokens
        Assign, Equal, LessThan, Plus, Minus, Times, Division, LeftParentheses, RightParentheses, SemiColon //special symbols
    }
    public struct Token
    {
        public string token_Name;
        public tokenType token_Type;

        public int lineNumber;
        public string Text;
        public Token(string name = "", tokenType type = tokenType.Error, int line = 0, string text = "")
        {
            this.token_Name = name;
            this.token_Type = type;
            this.lineNumber = line;
            this.Text = text;
        }

    }
    public class Scanner
    {
        private byte[,] transition_table;
        private string buffer;
        private Hashtable special_symbols, delimeters;
        private bool[] accept;
        private List<Token> tokens;
        private Hashtable symbolTable;
        private List<Token> comments, errors;

        private void initialize()
        {
            accept = new bool[12];
            accept[(byte)State.START] = false;
            accept[(byte)State.INCOMMENT] = false;
            accept[(byte)State.INNUM] = false;
            accept[(byte)State.INID] = false;
            accept[(byte)State.INASSIGN] = false;
            accept[(byte)State.DONEOP] = true;
            accept[(byte)State.DONENUM] = true;
            accept[(byte)State.DONEID] = true;
            accept[(byte)State.DONEASSIGN] = true;
            accept[(byte)State.DONECOMMENT] = true;
            accept[(byte)State.ERROR] = false;
            accept[(byte)State.DONEERROR] = true;

            transition_table = new byte[12, 9];

            transition_table[(byte)State.START, (byte)Input.COMMENT_LEFT] = (byte)State.INCOMMENT;
            transition_table[(byte)State.START, (byte)Input.COMMENT_RIGHT] = (byte)State.ERROR;
            transition_table[(byte)State.START, (byte)Input.ASSIGN_COL] = (byte)State.INASSIGN;
            transition_table[(byte)State.START, (byte)Input.EQUAL] = (byte)State.DONEOP;
            transition_table[(byte)State.START, (byte)Input.DIGIT] = (byte)State.INNUM;
            transition_table[(byte)State.START, (byte)Input.LETTER] = (byte)State.INID;
            transition_table[(byte)State.START, (byte)Input.SYMBOL] = (byte)State.DONEOP;
            transition_table[(byte)State.START, (byte)Input.DELIMETER] = (byte)State.START;
            transition_table[(byte)State.START, (byte)Input.OTHER] = (byte)State.ERROR;

            transition_table[(byte)State.INCOMMENT, (byte)Input.COMMENT_LEFT] = (byte)State.INCOMMENT;
            transition_table[(byte)State.INCOMMENT, (byte)Input.COMMENT_RIGHT] = (byte)State.DONECOMMENT;
            transition_table[(byte)State.INCOMMENT, (byte)Input.ASSIGN_COL] = (byte)State.INCOMMENT;
            transition_table[(byte)State.INCOMMENT, (byte)Input.EQUAL] = (byte)State.INCOMMENT;
            transition_table[(byte)State.INCOMMENT, (byte)Input.DIGIT] = (byte)State.INCOMMENT;
            transition_table[(byte)State.INCOMMENT, (byte)Input.LETTER] = (byte)State.INCOMMENT;
            transition_table[(byte)State.INCOMMENT, (byte)Input.SYMBOL] = (byte)State.INCOMMENT;
            transition_table[(byte)State.INCOMMENT, (byte)Input.DELIMETER] = (byte)State.INCOMMENT;
            transition_table[(byte)State.INCOMMENT, (byte)Input.OTHER] = (byte)State.INCOMMENT;

            transition_table[(byte)State.INNUM, (byte)Input.COMMENT_LEFT] = (byte)State.DONENUM;
            transition_table[(byte)State.INNUM, (byte)Input.COMMENT_RIGHT] = (byte)State.ERROR;
            transition_table[(byte)State.INNUM, (byte)Input.ASSIGN_COL] = (byte)State.DONENUM;
            transition_table[(byte)State.INNUM, (byte)Input.EQUAL] = (byte)State.DONENUM;
            transition_table[(byte)State.INNUM, (byte)Input.DIGIT] = (byte)State.INNUM;
            transition_table[(byte)State.INNUM, (byte)Input.LETTER] = (byte)State.ERROR;
            transition_table[(byte)State.INNUM, (byte)Input.SYMBOL] = (byte)State.DONENUM;
            transition_table[(byte)State.INNUM, (byte)Input.DELIMETER] = (byte)State.DONENUM;
            transition_table[(byte)State.INNUM, (byte)Input.OTHER] = (byte)State.ERROR;

            transition_table[(byte)State.INID, (byte)Input.COMMENT_LEFT] = (byte)State.DONEID;
            transition_table[(byte)State.INID, (byte)Input.COMMENT_RIGHT] = (byte)State.ERROR;
            transition_table[(byte)State.INID, (byte)Input.ASSIGN_COL] = (byte)State.DONEID;
            transition_table[(byte)State.INID, (byte)Input.EQUAL] = (byte)State.DONEID;
            transition_table[(byte)State.INID, (byte)Input.DIGIT] = (byte)State.INID;
            transition_table[(byte)State.INID, (byte)Input.LETTER] = (byte)State.INID;
            transition_table[(byte)State.INID, (byte)Input.SYMBOL] = (byte)State.DONEID;
            transition_table[(byte)State.INID, (byte)Input.DELIMETER] = (byte)State.DONEID;
            transition_table[(byte)State.INID, (byte)Input.OTHER] = (byte)State.ERROR;

            transition_table[(byte)State.INASSIGN, (byte)Input.COMMENT_LEFT] = (byte)State.ERROR;
            transition_table[(byte)State.INASSIGN, (byte)Input.COMMENT_RIGHT] = (byte)State.ERROR;
            transition_table[(byte)State.INASSIGN, (byte)Input.ASSIGN_COL] = (byte)State.ERROR;
            transition_table[(byte)State.INASSIGN, (byte)Input.EQUAL] = (byte)State.DONEASSIGN;
            transition_table[(byte)State.INASSIGN, (byte)Input.DIGIT] = (byte)State.ERROR;
            transition_table[(byte)State.INASSIGN, (byte)Input.LETTER] = (byte)State.ERROR;
            transition_table[(byte)State.INASSIGN, (byte)Input.SYMBOL] = (byte)State.ERROR;
            transition_table[(byte)State.INASSIGN, (byte)Input.DELIMETER] = (byte)State.ERROR;
            transition_table[(byte)State.INASSIGN, (byte)Input.OTHER] = (byte)State.ERROR;

            transition_table[(byte)State.ERROR, (byte)Input.COMMENT_LEFT] = (byte)State.DONEERROR;
            transition_table[(byte)State.ERROR, (byte)Input.COMMENT_RIGHT] = (byte)State.ERROR;
            transition_table[(byte)State.ERROR, (byte)Input.ASSIGN_COL] = (byte)State.DONEERROR;
            transition_table[(byte)State.ERROR, (byte)Input.EQUAL] = (byte)State.DONEERROR;
            transition_table[(byte)State.ERROR, (byte)Input.DIGIT] = (byte)State.ERROR;
            transition_table[(byte)State.ERROR, (byte)Input.LETTER] = (byte)State.ERROR;
            transition_table[(byte)State.ERROR, (byte)Input.SYMBOL] = (byte)State.DONEERROR;
            transition_table[(byte)State.ERROR, (byte)Input.DELIMETER] = (byte)State.DONEERROR;
            transition_table[(byte)State.ERROR, (byte)Input.OTHER] = (byte)State.ERROR;

            special_symbols = new Hashtable();
            special_symbols.Add(264, '/');
            special_symbols.Add(265, '-');
            special_symbols.Add(266, '+');
            special_symbols.Add(267, '*');
            special_symbols.Add(268, '<');
            special_symbols.Add(269, '(');
            special_symbols.Add(270, ')');
            special_symbols.Add(271, ';');

            delimeters = new Hashtable();
            delimeters.Add(273, ' ');
            delimeters.Add(274, '\t');
            delimeters.Add(275, '\n');
        }
        private byte checkType(char _input)
        {
            byte input;
            if ((_input <= 'Z' && _input >= 'A') || (_input <= 'z' && _input >= 'a'))
            {
                input = (byte)Input.LETTER;
            }
            else if (_input <= '9' && _input >= '0')
            {
                input = (byte)Input.DIGIT;
            }
            else if (_input == '}')
            {
                input = (byte)Input.COMMENT_RIGHT;
            }
            else if (_input == '{')
            {
                input = (byte)Input.COMMENT_LEFT;
            }
            else if (_input == ':')
            {
                input = (byte)Input.ASSIGN_COL;
            }
            else if (_input == '=')
            {
                input = (byte)Input.EQUAL;
            }
            else if (special_symbols.ContainsValue(_input))
            {
                input = (byte)Input.SYMBOL;
            }
            else if (delimeters.ContainsValue(_input))
            {
                input = (byte)Input.DELIMETER;
            }
            else
            {
                input = (byte)Input.OTHER;
            }

            return input;
        }
        private bool backState(byte state)
        {
            if (state == (byte)State.DONEID || state == (byte)State.DONENUM || state == (byte)State.DONEERROR)
                return true;
            else
                return false;
        }
        private tokenType getCategory(byte state, string token)
        {
            if (state == (byte)State.DONEASSIGN)
            {
                return tokenType.Assign;

            }
            else if (state == (byte)State.DONECOMMENT)
            {
                return tokenType.Comment;
            }
            else if (state == (byte)State.DONEID)
            {
                switch (token)
                {
                    case "if":
                        return tokenType.If;
                    case "then":
                        return tokenType.Then;
                    case "else":
                        return tokenType.Else;
                    case "end":
                        return tokenType.End;
                    case "repeat":
                        return tokenType.Repeat;
                    case "until":
                        return tokenType.Until;
                    case "read":
                        return tokenType.Read;
                    case "write":
                        return tokenType.Write;
                    default:
                        return tokenType.ID;

                }

            }
            else if (state == (byte)State.DONENUM)
            {
                return tokenType.Num;
            }
            else if (state == (byte)State.DONEOP)
            {
                switch (token)
                {
                    case "+":
                        return tokenType.Plus;
                    case "-":
                        return tokenType.Minus;
                    case "*":
                        return tokenType.Times;
                    case "/":
                        return tokenType.Division;
                    case "=":
                        return tokenType.Equal;
                    case "<":
                        return tokenType.LessThan;
                    case "(":
                        return tokenType.LeftParentheses;
                    case ")":
                        return tokenType.RightParentheses;
                    case ";":
                        return tokenType.SemiColon;
                    case ":=":
                        return tokenType.Assign;
                    default:
                        return tokenType.None;
                }


            }
            else if (state == (byte)State.DONEERROR)
            {
                return tokenType.Error;
            }
            else
                return tokenType.None;

        } 
        public Scanner()
        {
            initialize();
        }
        public Scanner(string _buffer) : this()
        {
            _buffer = _buffer.ToLower();
            this.buffer = _buffer;
            this.symbolTable = new Hashtable();
            this.tokens = new List<Token>();
            this.comments = new List<Token>();
            this.errors = new List<Token>();
        }
        public List<Token> getTokens()
        {
            return this.tokens;
        }
        public List<Token> getErrors()
        {
            return this.errors;
        }
        public List<Token> getComments()
        {
            return this.comments;
        }
        public Hashtable getSymbolTable()
        {
            return this.symbolTable;
        }
        public void Scan()
        {
            buffer = buffer.Replace("\r\n", "\n") + " ";
            byte state = (byte)State.START;
            string current_token = "";
            int line_num = 1;
            for (int i = 0; i < buffer.Length; i++)
            {
                
                byte input = checkType(buffer[i]);
                state = (byte)transition_table[state, input];

                if (accept[(byte)state]) //Accept
                {
                    object newToken;
                    tokenType current_category = tokenType.None;
                    if (!backState(state)) current_token += buffer[i];
                    else i--;

                    current_category = getCategory(state, current_token);

                    /// Add each Token to it's specified list
                    if(current_category == tokenType.Comment)
                    {
                        newToken = new Token("", tokenType.Comment, line_num, current_token);
                        comments.Add((Token)newToken);

                    }
                    else if (current_category == tokenType.Error)
                    {
                        newToken = new Token("", tokenType.Error, line_num, current_token);
                        errors.Add((Token)newToken);
                        tokens.Add((Token)newToken);

                    }
                    else if (current_category == tokenType.ID)
                    {
                        newToken = new Token(current_token, current_category);
                        if (!symbolTable.ContainsKey(((Token)newToken).token_Name))
                            symbolTable.Add(((Token)newToken).token_Name, 0);
                        tokens.Add((Token)newToken);

                    } 
                    else
                    {
                        if (current_category != tokenType.Num) current_token = "";
                        newToken = new Token(current_token, current_category);
                        tokens.Add((Token)newToken);
                    }

                    /// start over
                    current_token = "";
                    state = (byte)State.START;
                
                } //End if Accept
                else //Not Accept
                {
                    if (state == (byte)State.INCOMMENT)
                    {
                        current_token += buffer[i];
                    }
                    else
                    {
                        if (!delimeters.ContainsValue(buffer[i]))
                        {
                            current_token += buffer[i];
                        }
                    }    
                }


                if (buffer[i] == '\n') line_num++;
            } //End for loop

            return;
        } //End function start

        




    } //End class Scanner
}
