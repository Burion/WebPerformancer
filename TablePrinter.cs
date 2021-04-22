using System;
using System.Collections.Generic;

namespace WebPerformancer
{
    public class TablePrinter
    {
        readonly int tableWidth;
        List<LinkRecordModel> _records;
        public TablePrinter(List<LinkRecordModel> records, int tableWidth)
        {
              _records = records;
              this.tableWidth = tableWidth;
        }
        
        public void Print()
        {
            PrintLine();
            PrintRow("Url", "Timing (ms)", "Taken From");
            PrintLine();
            foreach(var l in _records)
            {   
                
                PrintRow(l.Link, $"{l.AnswerTimeMills}", $"{(Sources)l.TakenFrom}");
                PrintLine();
            }
        }
        void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}