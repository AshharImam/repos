using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using LAna.Semantic_Solutuons;

namespace LAna
{
    class analyzer
    {
        internal Regex _regId=new Regex("^[a-zA-Z]{1,20}[a-zA-Z0-9]{0,20}$");
        internal Regex regId= new Regex("^[_a-zA-Z]{1,20}[a-zA-Z0-9]{1,20}$");
        internal Regex regInt = new Regex(("^[+-]{0,1}[0-9]{1,20}$"));
        internal Regex regFloat = new Regex("^[+-]{0,1}[0-9]{0,20}(.)[0-9]{1,20}$");
        internal string[] keyWords = { "class", "void", "int", "float", "Main", "return", "while", "for", "this","if","else","char","bool","break","continue"
                                        ,"array","string","base", "sealed", "virtual", "override", "inheritance", "public", "private", "protected", "new","static"};
        public List<string> Tokens = new List<string>();
        public List<string> tokensPass = new List<string>();
        public List<string> tokensPass2 = new List<string>();



        //SemanticAnalyser sem =new SemanticAnalyser();// object created
        public bool _chk(string temp)
        {
            for (int z = 0; z < keyWords.Length; z++)
            {
                if (temp == keyWords[z])
                {
                    _write("Keyword",temp);
                    return true;
                }
            }
            if (_regId.IsMatch(temp))
            {
                _write("identifier", temp);
                return true;
            }
            //identifier with _
            else if (regId.IsMatch(temp))
            {
                _write("identifier", temp);
                return true;
            }
            //integer constant
            else if (regInt.IsMatch(temp))
            {
                _write("integer constant", temp);
                return true;
            }
            else if (regFloat.IsMatch(temp))
            {
                _write("float constant", temp);
                
                return true;
            }
            else
            {
                return false;
            }
        }
        public int count = 0;
        public void _write(string type, string word)
        {
            //Console.WriteLine(type + "  "+word);
            string listOBJ = (type +"  "+ word);
            string listObj2 = word;
            string listObj3 = type;
            Tokens.Add(listOBJ);
            tokensPass.Add(listObj2);
            tokensPass2.Add(listObj3);
        }

        public bool _chkInt(string temp)
        {
            if (regInt.IsMatch(temp))
            {
                //_write("integer constant", temp);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool _chkFloat(string temp)
        {
            if (regInt.IsMatch(temp))
            {
                //_write("Float constant", temp);
                return true;
            }
            else
            {
                return false;
            }
        }
        public string _writeFile()
        {
            string text = null;
            foreach (var item in Tokens)
            {
                text += item + Environment.NewLine;
            }
            tokensGet.Func(tokensPass,tokensPass2);
            return text;
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            analyzer an = new analyzer();
            string text = File.ReadAllText(@"C:\Users\Syed Ashhar Imam\source\repos\LAna\LAna\input.txt"), word = null;
            //string text = "class construct abc 78978", word = null;
            int lineNum = 1, ittr = 0;
            //Console.WriteLine("start of program");
            while (ittr < text.Length)
            {
                
                if (text[ittr] == ' ')//space
                {
                    if (word != null)
                    {
                        //Console.WriteLine(word);
                        if (an._chk(word))
                        {
                            word = null;
                        }
                    }
                    ittr++;
                }
                else if (text[ittr] == '\r' || text[ittr] == '\n')//line change or carriage return
                {
                    if (word != null)
                    {
                        if (an._chk(word))
                        {
                            word = null;
                        }
                    }
                    if (text[ittr] == '\r')
                    {
                        lineNum++;
                    }
                    ittr++;
                }
                else if (text[ittr] == (char)9)//tabspace
                {
                    if (word != null)
                    {
                        if (an._chk(word))
                        {
                            word = null;
                        }
                    }
                    ittr++;
                }
                else if (text[ittr] == '}' || text[ittr] == '{' || text[ittr] == '(' || text[ittr] == ')' || text[ittr] == '[' || text[ittr] == ']' || text[ittr] == ':' || text[ittr] == ';')
                {
                    if (word != null)
                    {
                        if (an._chk(word))
                        {
                            word = null;
                        }
                    }
                    if (text[ittr] == ':' && text[ittr + 1] == ':')
                    {
                        word = text[ittr].ToString() + text[ittr + 1].ToString();
                        an._write("refrence operator", word);
                        word = null;
                        ittr = ittr + 2;
                    }
                    else if (text[ittr] == '(')
                    {
                        int j = ittr;
                        string temp = null;
                        while (text[ittr + 1] != ')' && text[ittr + 1] != '\r')
                        {
                            temp += text[ittr + 1];
                            ittr++;
                        }
                        if (temp != null)
                        {
                            if (an._chkInt(temp))
                            {
                                an._write("parantheses", "(");
                                an._write("integer constant ", temp);
                                temp = null;
                                ittr++;
                            }
                            else if (an._chkFloat(temp))
                            {
                                an._write("parantheses", "(");
                                an._write("float constant ", temp);
                                temp = null;
                                ittr++;
                            }
                        }
                            else
                            {
                                ittr = j;
                                an._write("parantheses", "(");
                                temp = null;
                                ittr++;
                            }
                    }
                    else
                    {
                        an._write("unknown", text[ittr].ToString());
                        ittr++;
                    }
                }
                else if (text[ittr] == '+' || text[ittr] == '-' || text[ittr] == '*' || text[ittr] == '/' || text[ittr] == '%' || text[ittr] == '<' || text[ittr] == '>' || text[ittr] == '=' || text[ittr] == '!' || text[ittr] == '&' || text[ittr] == '!')
                {
                    if (word != null)
                    {
                        if (an._chk(word))
                        {
                            word = null;
                        }
                    }
                    Regex regex = new Regex(@"^[+-]{0,1}[0-9]{1,20}$");
                    switch (text[ittr])
                    {
                        case '+':
                            //inc op
                            if (text[ittr + 1] == '+')
                            {
                                an._write("INC", "++");
                                word = null;
                                ittr = ittr + 2;
                            }
                            //signed
                            else if (ittr - 1 >= 0 && text[ittr - 1] == '=' && regex.IsMatch(text[ittr + 1].ToString()))
                            {
                                string temp = null;
                                int j = ittr;
                                while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                {
                                    temp += text[ittr + 1];
                                    ittr++;

                                }
                                an._write("signed integer", temp);
                                ittr++;
                            }
                            //for signed with \n
                            else if (ittr - 1 >= 0 && text[ittr - 1] == '\n' && regex.IsMatch(text[ittr + 1].ToString()))
                            {
                                string temp = null;
                                int j = ittr;
                                while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                {
                                    temp += text[ittr + 1];
                                    ittr++;
                                }
                                an._write("signed integer", temp);
                            }
                            //=+     intfloat
                            else if (ittr - 1 >= 0 && text[ittr - 1] == '=' && text[ittr + 1] == ' ')
                            {
                                int j = ittr;
                                while (text[ittr + 1] == ' ')
                                {
                                    ittr++;
                                }

                                string temp = null;

                                if (regex.IsMatch(text[ittr + 1].ToString()))
                                {
                                    while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        temp += text[ittr + 1];
                                        ittr++;
                                    }
                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                            }
                            //for the (\n)+     7
                            else if (ittr - 1 >= 0 && text[ittr - 1] == '\n' && text[ittr + 1] == ' ')
                            {
                                int j = ittr;
                                while (text[ittr + 1] == ' ')
                                {
                                    ittr++;
                                }

                                string temp = null;

                                if (regex.IsMatch(text[ittr + 1].ToString()))
                                {
                                    while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        temp += text[ittr + 1];
                                        ittr++;
                                    }
                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                            }
                            // =      + && (\n)      
                            else if (ittr - 1 >= 0 && text[ittr - 1] == ' ' && regex.IsMatch(text[ittr + 1].ToString()))
                            {
                                int j = ittr;
                                while (text[ittr - 1] == ' ' && text[ittr - 1] != '\r')
                                {
                                    ittr--;
                                }

                                string temp = null;
                                if (text[ittr - 1] == '=')
                                {
                                    ittr = j;
                                    if (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                        {
                                            temp += text[ittr + 1];
                                            ittr++;
                                        }
                                    }
                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                                else if (text[ittr - 1] == '\n')
                                {
                                    ittr = j;
                                    if (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                        {
                                            temp += text[ittr + 1];
                                            ittr++;
                                        }
                                    }
                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                            }
                            // =     +     N and \n      +     N
                            else if (ittr - 1 >= 0 && text[ittr - 1] == ' ' && text[ittr + 1] == ' ')
                            {
                                int j = ittr;
                                string eqOrcar = null;
                                while (text[ittr - 1] == ' ')
                                {
                                    ittr--;
                                }
                                if (text[ittr - 1] == '=' || text[ittr - 1] == '\n')
                                {
                                    eqOrcar = text[ittr - 1].ToString();
                                }
                                ittr = j;
                                while (text[ittr + 1] == ' ')
                                {
                                    ittr++;
                                }
                                string temp = null;
                                if (regex.IsMatch(text[ittr + 1].ToString()))
                                {
                                    while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        temp += text[ittr + 1];
                                        ittr++;
                                    }
                                }
                                if ((eqOrcar == '='.ToString() && temp != null))
                                {

                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                                else if (eqOrcar == '\n'.ToString() && temp != null)
                                {
                                    an._write("signe integer", temp);
                                    ittr++;
                                }
                            }
                            else
                            {

                                an._write("PM", text[ittr].ToString());
                                ittr++;

                            }
                            break;

                        case '-':
                            //dec --
                            if (text[ittr + 1] == '-')
                            {
                                an._write("DEC", "--");
                                word = null;
                                ittr = ittr + 2;
                            }
                            //signed
                            else if (ittr - 1 >= 0 && text[ittr - 1] == '=' && regex.IsMatch(text[ittr + 1].ToString()))
                            {
                                string temp = null;
                                int j = ittr;
                                while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                {
                                    temp += text[ittr + 1];
                                    ittr++;

                                }
                                an._write("signed integer", temp);
                                ittr++;
                            }
                            //signed with \n
                            else if (ittr - 1 >= 0 && text[ittr - 1] == '\n' && regex.IsMatch(text[ittr + 1].ToString()))
                            {
                                string temp = null;
                                int j = ittr;
                                while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                {
                                    temp += text[ittr + 1];
                                    ittr++;
                                }
                                an._write("signed integer", temp);
                            }
                            //=+     N
                            else if (ittr - 1 >= 0 && text[ittr - 1] == '=' && text[ittr + 1] == ' ')
                            {
                                int j = ittr;
                                while (text[ittr + 1] == ' ')
                                {
                                    ittr++;
                                }

                                string temp = null;

                                if (regex.IsMatch(text[ittr + 1].ToString()))
                                {
                                    while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        temp += text[ittr + 1];
                                        ittr++;
                                    }
                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                            }
                            //(\n)+     N
                            else if (ittr - 1 >= 0 && text[ittr - 1] == '\n' && text[ittr + 1] == ' ')
                            {
                                int j = ittr;
                                while (text[ittr + 1] == ' ')
                                {
                                    ittr++;
                                }

                                string temp = null;

                                if (regex.IsMatch(text[ittr + 1].ToString()))
                                {
                                    while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        temp += text[ittr + 1];
                                        ittr++;
                                    }
                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                            }
                            // =      +N and (\n)      +N
                            else if (ittr - 1 >= 0 && text[ittr - 1] == ' ' && regex.IsMatch(text[ittr + 1].ToString()))
                            {
                                int j = ittr;
                                while (text[ittr - 1] == ' ' && text[ittr - 1] != '\r')
                                {
                                    ittr--;
                                }

                                string temp = null;
                                if (text[ittr - 1] == '=')
                                {
                                    ittr = j;
                                    if (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                        {
                                            temp += text[ittr + 1];
                                            ittr++;
                                        }
                                    }
                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                                else if (text[ittr - 1] == '\n')
                                {
                                    ittr = j;
                                    if (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                        {
                                            temp += text[ittr + 1];
                                            ittr++;
                                        }
                                    }
                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                            }
                            // =     +     N and \n      +     N
                            else if (ittr - 1 >= 0 && text[ittr - 1] == ' ' && text[ittr + 1] == ' ')
                            {
                                int j = ittr;
                                string eqOrcar = null;
                                while (text[ittr - 1] == ' ')
                                {
                                    ittr--;
                                }
                                if (text[ittr - 1] == '=' || text[ittr - 1] == '\n')
                                {
                                    eqOrcar = text[ittr - 1].ToString();
                                }
                                ittr = j;
                                while (text[ittr + 1] == ' ')
                                {
                                    ittr++;
                                }
                                string temp = null;
                                if (regex.IsMatch(text[ittr + 1].ToString()))
                                {
                                    while (regex.IsMatch(text[ittr + 1].ToString()) && ittr + 1 < text.Length - 1)
                                    {
                                        temp += text[ittr + 1];
                                        ittr++;
                                    }
                                }
                                if ((eqOrcar == '='.ToString() && temp != null))
                                {

                                    an._write("signed integer", temp);
                                    ittr++;
                                }
                                else if (eqOrcar == '\n'.ToString() && temp != null)
                                {
                                    an._write("signe integer", temp);
                                    ittr++;
                                }
                            }
                            else
                            {

                                an._write("PM", text[ittr].ToString());
                                ittr++;

                            }
                            break;

                        case '*':

                            an._write("MulDiv", "*");
                            ittr++;
                            break;

                        case '/':
                            if (text[ittr + 1] == '/')
                            {
                                string temp = null;
                                while (text[ittr + 2] != '\r')
                                {
                                    temp += text[ittr + 2];
                                    ittr++;
                                }
                                an._write("Single line comment", "//");
                                ittr += 2;
                            }
                            else if (text[ittr + 1] == '*')
                            {
                                string temp = null;
                            mulLine:
                                while (text[ittr + 2] != '*')
                                {
                                    if (text[ittr + 2] == '\r')
                                    {
                                        lineNum++;
                                    }
                                    temp += text[ittr + 2];
                                    ittr++;
                                }
                                if (text[ittr + 3] == '/')
                                {
                                    an._write("multiple line comment", "/* */");
                                    ittr = ittr + 4;
                                }
                                else
                                {
                                    temp += text[ittr + 2];
                                    ittr++;
                                    goto mulLine;
                                }
                            }
                            else
                            {
                                an._write("MultiDiv", text[ittr].ToString());
                                ittr++;
                            }
                            break;

                        case '%':

                            an._write("Mod", "%");
                            break;

                        case '<':
                            if (text[ittr + 1] == '=')
                            {
                                word = text[ittr].ToString() + text[ittr + 1].ToString();
                                an._write("Relational Op", word);
                                word = null;
                                ittr += 2;
                                break;
                            }
                            else
                            {
                                an._write("Relational Op", text[ittr].ToString());
                                ittr++;
                                break;
                            }
                        case '=':
                            if (text[ittr + 1] == '=')
                            {
                                word = text[ittr].ToString() + text[ittr + 1].ToString();
                                an._write("Relational Op", word);
                                word = null;
                                ittr += 2;
                            }
                            else
                            {
                                an._write("assignment Op", "=");
                                word = null;
                                ittr++;
                            }
                            break;

                        case '>':
                            if (text[ittr + 1] == '=')
                            {
                                word = text[ittr].ToString() + text[ittr + 1].ToString();
                                an._write("Relational Op", word);
                                word = null;
                                ittr += 2;

                            }
                            else
                            {
                                an._write("Relational Op", text[ittr].ToString());
                                ittr++;

                            }
                            break;

                        case '!':
                            if (text[ittr + 1] == '=')
                            {
                                word = text[ittr].ToString() + text[ittr + 1].ToString();
                                an._write("Relational Op", word);
                                word = null;
                                ittr += 2;
                            }
                            else
                            {
                                an._write("log Op", text[ittr].ToString());
                                ittr++;
                            }
                            break;

                        case '&':
                            if (text[ittr + 1] == '&')
                            {
                                word = text[ittr].ToString() + text[ittr + 1].ToString();
                                an._write("Relational Op", word);
                                word = null;
                                ittr += 2;
                            }
                            else
                            {
                                an._write("invalid lexene", text[ittr].ToString());
                                ittr++;
                            }
                            break;

                        case '|':
                            if (text[ittr + 1] == '|')
                            {
                                word = text[ittr].ToString() + text[ittr + 1].ToString();
                                an._write("log Op", word);
                                word = null;
                                ittr += 2;
                            }
                            else
                            {
                                an._write("invalid lexene", text[ittr].ToString());
                                ittr++;
                            }
                            break;
                    }

                }
                else if (text[ittr] == '"')
                {
                    
                    if (word != null)
                    {
                        //Console.WriteLine(word);
                        if (an._chk(word))
                        {
                            //Console.WriteLine(word);
                            word = null;
                        }
                    }
                    word = null;
                    int m = ittr;
                    ittr++;
                    //Console.WriteLine(ittr);
                dblQuot:
                    while (text[ittr] != '"' && text[ittr] != '\r')
                    {
                        //Console.WriteLine("in while");
                        word += text[ittr];
                        ittr++;
                    }
                    //Console.WriteLine(word);
                    if (text[ittr] == '\r')
                    {
                        an._write("invalid lexene", (text[m] + word).ToString());
                        //Console.WriteLine("here in the line 0");
                        word = null;
                        lineNum++;
                    }
                    else if (text[ittr] == '"' && text[ittr - 1] != '\\')
                    {
                        an._write("string constant", word);
                       // Console.WriteLine("Here in the line 1 "+word);
                        word = null;
                    }
                    else if (text[ittr] == '"' && text[ittr - 1] == '\\' && text[ittr - 2] != '\\')
                    {
                        an._write("string constant", word);
                        //Console.WriteLine("Here in line 2");
                        word = null;
                        goto dblQuot;
                    }
                    else if (text[ittr] == '"' && text[ittr - 1] == '\\' && text[ittr - 2] == '\\')
                    {
                        an._write("string constant", word);
                        word = null;
                    }
                    //Console.WriteLine(ittr);
                    ittr++;
                    //Console.WriteLine(ittr);
                }
                else if (text[ittr] == '\'')
                {
                    if (word != null)
                    {
                        if (an._chk(word))
                        {
                            word = null;
                        }
                    }
                    word = null;

                    Regex regex1 = new Regex("^[]+-/*!@#$%^&()_a-zA-Z0-9]{1}$");
                    Regex regex2 = new Regex("^\\[nrt0b\\]{1}$");

                    if (text[ittr + 1] == '\\')
                    {
                        string temp = text[ittr + 1].ToString() + text[ittr + 2].ToString();
                        if (regex2.IsMatch(temp) && text[ittr + 3] == '\'')
                        {
                            an._write("char constant", temp);
                            temp = null;
                        }
                        else
                        {
                            an._write("invalid lexene", lineNum.ToString());
                            temp = null;
                        }
                        ittr = ittr + 4;
                    }
                    else if (text[ittr + 1] != '\\')
                    {
                        string temp = text[ittr + 1].ToString();
                        if (regex1.IsMatch(temp.ToString()) && text[ittr + 2] == '\'')
                        {
                            an._write("char constant", temp);
                            temp = null;
                        }
                        else
                        {
                            an._write("invalid lexene", (temp[ittr].ToString() + temp[ittr + 2].ToString()));
                            temp = null;
                        }
                        ittr += 3;
                    }
                }
             
                else
                {
                    word += text[ittr];
                    ittr++;
                    if (ittr>=text.Length)
                    {
                        if (word!=null)
                        {
                            an._chk(word);
                        }
                        break;
                    }
                }
            }
            string outText = an._writeFile();
            //Console.WriteLine(" here is the last line");
            File.WriteAllText(@"C:\Users\Syed Ashhar Imam\source\repos\LAna\LAna\outpu.txt", outText);
            //foreach (string item in globalVar.lexTokens)
            //{
            //    Console.WriteLine(item);
            //}
            syntaxAnalyser sem = new syntaxAnalyser();
            if (sem.Start())
            {
                Console.WriteLine("parsed succ");
            }
            else
            {
                Console.WriteLine("parsed unn");
            }
            
            Console.WriteLine(globalVar.ittr);
            
        }
    }
    
}
