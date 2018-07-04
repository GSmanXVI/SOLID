using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Liskov
{
#region BAD
    class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public int GetArea()
        {
            return Width * Height;
        }
    }

    class Square : Rectangle
    {
        public override int Width
        {
            get
            {
                return base.Width;
            }

            set
            {
                base.Width = value;
                base.Height = value;
            }
        }

        public override int Height
        {
            get
            {
                return base.Height;
            }

            set
            {
                base.Height = value;
                base.Width = value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Square();
            TestRectangleArea(rect);

            Console.Read();
        }

        public static void TestRectangleArea(Rectangle rect)
        {
            rect.Height = 5;
            rect.Width = 10;
            if (rect.GetArea() != 50)
                throw new Exception("Некорректная площадь!");
        }

        public static void TestRectangleArea2(Rectangle rect)
        {
            if (rect is Square)
            {
                rect.Height = 5;
                if (rect.GetArea() != 25)
                    throw new Exception("Неправильная площадь!");
            }
            else if (rect is Rectangle)
            {
                rect.Height = 5;
                rect.Width = 10;
                if (rect.GetArea() != 50)
                    throw new Exception("Неправильная площадь!");
            }
        }
    }
    #endregion
}
