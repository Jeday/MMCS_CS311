using System.Collections.Generic;
public class LexerException : System.Exception
{
    public LexerException(string msg)
        : base(msg)
    {
    }

}

public class Lexer
{

    protected int position;
    protected char currentCh;       // ��������� ��������� ������
    protected int currentCharValue; // ����� �������� ���������� ���������� �������
    protected System.IO.StringReader inputReader;
    protected string inputString;

    public Lexer(string input)
    {
        inputReader = new System.IO.StringReader(input);
        inputString = input;
    }

    public void Error()
    {
        System.Text.StringBuilder o = new System.Text.StringBuilder();
        o.Append(inputString + '\n');
        o.Append(new System.String(' ', position - 1) + "^\n");
        o.AppendFormat("Error in symbol {0}", currentCh);
        throw new LexerException(o.ToString());
    }

    protected void NextCh()
    {
        this.currentCharValue = this.inputReader.Read();
        this.currentCh = (char)currentCharValue;
        this.position += 1;
    }

    public virtual bool Parse()
    {	
		
        return false;
    }
	
}

public class IntLexer : Lexer
{

    protected System.Text.StringBuilder intString;
    public int result;
    protected int sign;
    public IntLexer(string input)
        : base(input)
    {
        intString = new System.Text.StringBuilder();
    }

    public override bool Parse()
    {
		result = 0;
		sign = 0;
        NextCh(); // at first char
        if (currentCh == '+' || currentCh == '-' )
        {
            sign = currentCh == '-' ? -1 : 1;
            NextCh();
			if(char.IsDigit(currentCh))
			 result = sign * (int)(currentCh - '0');
			else 
				return false;
        } 
        else if (char.IsDigit(currentCh)) {
			sign = 1;
			result  = (int)(currentCh - '0');
		}
        while (char.IsDigit(currentCh))
        {
            result = result*10 + sign * (int)(currentCh - '0');
            NextCh();
        }


        if (currentCharValue != -1) // StringReader ������ -1 � ����� ������
        {
            
            return false;
        }

        //System.Console.WriteLine("Integer is recognized");
        return true;
    }
}


public class Program
{
    public static void Main()
    {	
		System.Console.WriteLine("Testing IntLexer:");
		List<string> test_int = new List<string> {"1","123","+123","-123","+a","+","+1233f"," "};
        foreach (var str in test_int) {
			IntLexer L = new IntLexer(str);
        	System.Console.WriteLine(System.String.Format("{0} : {1}",str,L.Parse()));
		}
        
	}    
}