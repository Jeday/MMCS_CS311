using System;
using System.IO;
using SimpleScanner;
using ScannerHelper;
using System.Collections.Generic;

namespace Main
{
    class mymain
    {
        static void Main(string[] args)
        {
            // ����� ������������ ����� �������������� � ������������ � ������� 3.14 (� �� 3,14 ��� � ������� Culture)
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            var fname = @"..\..\a.txt";
            Console.WriteLine(File.ReadAllText(fname));
            Console.WriteLine("-------------------------");

            Scanner scanner = new Scanner(new FileStream(fname, FileMode.Open));
            scanner.IdList = new List<string>();
            int tok = 0;
            do {
                tok = scanner.yylex();
                
                if (tok == (int)Tok.EOF)
                    break;
                Console.WriteLine(scanner.TokToString((Tok)tok));
            } while (true);

            Console.ReadKey();
        }
    }
}
