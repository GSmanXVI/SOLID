using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Interface_Segregation
{
    #region BAD
    //interface IMessage
    //{
    //    void Send();
    //    string Text { get; set; }
    //    string Subject { get; set; }
    //    string ToAddress { get; set; }
    //    string FromAddress { get; set; }
    //}

    //class EmailMessage : IMessage
    //{
    //    public string Subject { get; set; }
    //    public string Text { get; set; }
    //    public string FromAddress { get; set; }
    //    public string ToAddress { get; set; }

    //    public void Send()
    //    {
    //        Console.WriteLine("Отправляем по Email сообщение: {0}", Text);
    //    }
    //}

    //class SmsMessage : IMessage
    //{
    //    public string Text { get; set; }
    //    public string FromAddress { get; set; }
    //    public string ToAddress { get; set; }

    //    public string Subject
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }

    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public void Send()
    //    {
    //        Console.WriteLine("Отправляем по Sms сообщение: {0}", Text);
    //    }
    //}

    //interface IMessage2
    //{
    //    void Send();
    //    string Text { get; set; }
    //    string ToAddress { get; set; }
    //    string Subject { get; set; }
    //    string FromAddress { get; set; }

    //    byte[] Voice { get; set; }
    //}

    //class VoiceMessage : IMessage2
    //{
    //    public string ToAddress { get; set; }
    //    public string FromAddress { get; set; }
    //    public byte[] Voice { get; set; }

    //    public string Text
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }

    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public string Subject
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }

    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public void Send()
    //    {
    //        Console.WriteLine("Передача голосовой почты");
    //    }
    //}
    #endregion

    #region GOOD
    interface IMessage
    {
        void Send();
        string ToAddress { get; set; }
        string FromAddress { get; set; }
    }
    interface IVoiceMessage : IMessage
    {
        byte[] Voice { get; set; }
    }
    interface ITextMessage : IMessage
    {
        string Text { get; set; }
    }

    interface IEmailMessage : ITextMessage
    {
        string Subject { get; set; }
    }

    class VoiceMessage : IVoiceMessage
    {
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }

        public byte[] Voice { get; set; }
        public void Send()
        {
            Console.WriteLine("Передача голосовой почты");
        }
    }
    class EmailMessage : IEmailMessage
    {
        public string Text { get; set; }
        public string Subject { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }

        public void Send()
        {
            Console.WriteLine("Отправляем по Email сообщение: {0}", Text);
        }
    }

    class SmsMessage : ITextMessage
    {
        public string Text { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public void Send()
        {
            Console.WriteLine("Отправляем по Sms сообщение: {0}", Text);
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
