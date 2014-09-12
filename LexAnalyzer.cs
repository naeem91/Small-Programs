using System;
using System.IO;
using System.Text.RegularExpressions;


public class Scanner
{
    //method used to read all code from the file into one string
    
    public static string getCode()
    {
        string theCode = "";
        string line;
        TextReader reader = new StreamReader("code.txt");
        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
            theCode += line.Trim();
        }
        return theCode;
    }

    //commonly used reserved words in C/C++
    static string[] reservedWords = { "int", "char", "float", "if", "else","void","for","while","double", 
                                      "return","break","false","switch","case","long","true","new","do",
                                      "define","public","register","include","main","cout","cin"
                                     };

    //commonly used symbols/operators that separate different elements in code
    static string[] separatos = { "+", "-", "*", "/", "=", " ", "(", ")", ";", "{", "}", "<", ">", "#", "\"", "," };

    //get all the code from file into this string
    static string code = getCode();

    static string[] words = new string[code.Length + 50];
    static int j = -1;
    static string word = "";


    public static void Main(string[] args)
    {

        for (int i = 0; i < code.Length; i++)
        {
            if (isSeparator(code[i]))
            {
                if (word != "")
                    words[++j] = word;
                word = "";

                if (code[i] == ' ' || code[i] == '+' || code[i] == '=' || code[i] == '<' || code[i] == '>' || code[i] == '-'
                    || code[i] == '!' || code[i] == '-' || code[i] == '"' || code[i] == '/')
                {
                    i = specialCase(code[i], i);
                }
                else
                {
                    words[++j] = code[i].ToString();
                }
            }
            else
            {
                word += code[i];
            }
        }
        if (word != "")
            words[++j] = word;

        Console.Title = "Lexical Analyzer";
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("{0,10}{1,20}", "Lexeme", "Type");
        Console.WriteLine();

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i] != null)
            {
                string type = checkType(words[i]);
                if (type == "invalid")
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("{0,10}{1,20}", words[i], type);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        Console.ReadLine();
    }
    public static bool isSeparator(char w)
    {
        foreach (var sep in separatos)
        {
            if (w.ToString() == sep)
                return true;
        }
        return false;
    }
    public static int specialCase(char ch, int index)
    {
        int temp = index;
        switch (ch)
        {
            case ' ':
                break;
            case '+':
                if (code[++temp] == '+')
                {
                    words[++j] = "++";
                    return temp;
                }
                else
                {
                    words[++j] = "+";
                }
                break;
            case '-':
                if (code[++temp] == '-')
                {
                    words[++j] = "--";
                    return temp;
                }
                else
                {
                    words[++j] = "-";
                }
                break;
            case '=':
                if (code[++temp] == '=')
                {
                    words[++j] = "==";
                    return temp;
                }
                else
                {
                    words[++j] = "=";
                }
                break;
            case '!':
                if (code[++temp] == '!')
                {
                    words[++j] = "!=";
                    return temp;
                }
                else
                {
                    words[++j] = "!";
                }
                break;
            case '<':
                if (code[++temp] == '=')
                {
                    words[++j] = "<=";
                    return temp;
                }

                else if (code[temp] == '<')
                {
                    words[++j] = "<<";

                    return temp;
                }
                else
                {
                    words[++j] = "<";

                }
                break;
            case '>':
                if (code[++temp] == '=')
                {
                    words[++j] = ">=";
                    return temp;
                }
                else if (code[temp] == '>')
                {
                    words[++j] = ">>";
                    return temp;
                }
                else
                {
                    words[++j] = ">";
                }
                break;
            case '"':
                string tW = code[temp].ToString();
                while (code[++temp] != '"')
                {
                    tW += code[temp].ToString();

                }
                tW += code[temp].ToString();
                words[++j] = tW;
                return temp;
                break;
            case '/':
                if (code[++temp] == '*')
                {
                    while (code[temp] != '/')
                    {
                        temp++;
                    }
                }
                else
                {
                    words[++j] = code[index].ToString();
                    return index;
                }
                return temp;
                break;
            default:
                return index;
        }
        return index;
    }
    public static string checkType(string word)
    {
        string type = "invalid";

        //check if reserved words
        if (Regex.IsMatch(word, @"^[a-zA-Z]+$"))
        {
            foreach (var reserv in reservedWords)
            {
                if (word == reserv)
                {
                    return type = "keyword";
                }

            }
        }

        //check if variables
        if ((Regex.IsMatch(word, "^[_]") || Regex.IsMatch(word, "^[a-zA-z]")) && (Regex.IsMatch(word, @"^[a-zA-Z0-9_]+$")))
            return type = "Identifier";


        //check if strings or chars
        if (Regex.IsMatch(word, "^[']") || Regex.IsMatch(word, "^[\"]"))
            return type = "string constant";

        //check digits
        if (Regex.IsMatch(word, "^[0-9]+$"))
            return type = "Constant";

        //if operators
        if (word == "+" || word == "-" || word == "*" || word == "/" || word == "=" || word == "%" || word == "++" || word == "--"
            || word == "<<" || word == ">>")
            return type = "Operator";

        if (word == ";")
            return type = "Semicolon";

        //if equalites,inequalites
        if (word == "<" || word == ">" || word == "<=" || word == ">=" || word == "==")
            return type = "Comparison";

        //if punctuators
        if (word == "(" || word == ")" || word == "{" || word == "}" || word == ",")
            return type = "Punctuators";

        if (word == "#")
            return type = "Directive";

        return type;
    }
}

