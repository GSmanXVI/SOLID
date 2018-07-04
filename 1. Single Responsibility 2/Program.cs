using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Single_Responsibility_2
{
    #region BAD
    //class Phone
    //{
    //    public string Model { get; set; }
    //    public int Price { get; set; }
    //}

    //class MobileStore
    //{
    //    List<Phone> phones = new List<Phone>();
    //    public void Process()
    //    {
    //        Console.WriteLine("Введите модель:");
    //        string model = Console.ReadLine();
    //        Console.WriteLine("Введите цену:");
    //        int price = 0;
    //        bool result = Int32.TryParse(Console.ReadLine(), out price);

    //        if (result == false || price <= 0 || String.IsNullOrEmpty(model))
    //        {
    //            throw new Exception("Некорректно введены данные");
    //        }
    //        else
    //        {
    //            phones.Add(new Phone { Model = model, Price = price });
    //            // сохраняем данные в файл
    //            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("store.txt", true))
    //            {
    //                writer.WriteLine(model);
    //                writer.WriteLine(price);
    //            }
    //            Console.WriteLine("Данные успешно обработаны");
    //        }
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {

    //    }
    //}
    #endregion

    #region GOOD
    class Phone
    {
        public string Model { get; set; }
        public int Price { get; set; }
    }

    class MobileStore
    {
        List<Phone> phones = new List<Phone>();

        public IPhoneReader Reader { get; set; }
        public IPhoneBinder Binder { get; set; }
        public IPhoneValidator Validator { get; set; }
        public IPhoneSaver Saver { get; set; }

        public MobileStore(IPhoneReader reader, IPhoneBinder binder, IPhoneValidator validator, IPhoneSaver saver)
        {
            this.Reader = reader;
            this.Binder = binder;
            this.Validator = validator;
            this.Saver = saver;
        }

        public void Process()
        {
            string[] data = Reader.GetInputData();
            Phone phone = Binder.CreatePhone(data);
            if (Validator.IsValid(phone))
            {
                phones.Add(phone);
                Saver.Save(phone, "store.txt");
                Console.WriteLine("Данные успешно обработаны");
            }
            else
            {
                Console.WriteLine("Некорректные данные");
            }
        }
    }

    interface IPhoneReader
    {
        string[] GetInputData();
    }
    class ConsolePhoneReader : IPhoneReader
    {
        public string[] GetInputData()
        {
            Console.WriteLine("Введите модель:");
            string model = Console.ReadLine();
            Console.WriteLine("Введите цену:");
            string price = Console.ReadLine();
            return new string[] { model, price };
        }
    }

    interface IPhoneBinder
    {
        Phone CreatePhone(string[] data);
    }

    class GeneralPhoneBinder : IPhoneBinder
    {
        public Phone CreatePhone(string[] data)
        {
            if (data.Length >= 2)
            {
                int price = 0;
                if (Int32.TryParse(data[1], out price))
                {
                    return new Phone { Model = data[0], Price = price };
                }
                else
                {
                    throw new Exception("Ошибка привязчика модели Phone. Некорректные данные для свойства Price");
                }
            }
            else
            {
                throw new Exception("Ошибка привязчика модели Phone. Недостаточно данных для создания модели");
            }
        }
    }

    interface IPhoneValidator
    {
        bool IsValid(Phone phone);
    }

    class GeneralPhoneValidator : IPhoneValidator
    {
        public bool IsValid(Phone phone)
        {
            if (String.IsNullOrEmpty(phone.Model) || phone.Price <= 0)
                return false;

            return true;
        }
    }

    interface IPhoneSaver
    {
        void Save(Phone phone, string fileName);
    }

    class TextPhoneSaver : IPhoneSaver
    {
        public void Save(Phone phone, string fileName)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName, true))
            {
                writer.WriteLine(phone.Model);
                writer.WriteLine(phone.Price);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            MobileStore store = new MobileStore(
                new ConsolePhoneReader(), new GeneralPhoneBinder(),
                new GeneralPhoneValidator(), new TextPhoneSaver());
            store.Process();
        }
    }
    #endregion
}
