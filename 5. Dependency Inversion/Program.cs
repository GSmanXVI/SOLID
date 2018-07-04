using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.Dependency_Inversion
{
    #region BAD
    //class Book
    //{
    //    public string Text { get; set; }
    //    public ConsolePrinter Printer { get; set; }

    //    public void Print()
    //    {
    //        Printer.Print(Text);
    //    }
    //}

    //class ConsolePrinter
    //{
    //    public void Print(string text)
    //    {
    //        Console.WriteLine(text);
    //    }
    //}
    #endregion

    #region GOOD
    interface IPrinter
    {
        void Print(string text);
    }

    class Book
    {
        public string Text { get; set; }
        public IPrinter Printer { get; set; }

        public Book(IPrinter printer)
        {
            this.Printer = printer;
        }

        public void Print()
        {
            Printer.Print(Text);
        }
    }

    class ConsolePrinter : IPrinter
    {
        public void Print(string text)
        {
            Console.WriteLine("Печать на консоли");
        }
    }

    class HtmlPrinter : IPrinter
    {
        public void Print(string text)
        {
            Console.WriteLine("Печать в html");
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book(new ConsolePrinter());
            book.Print();
            book.Printer = new HtmlPrinter();
            book.Print();
        }
    }
}
