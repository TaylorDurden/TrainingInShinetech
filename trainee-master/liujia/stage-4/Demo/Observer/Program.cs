using System;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            var subject = new ConcreateSubject();
            subject.MsgAction += (new InsideLetterMsg()).SendInsideLetterMsg;
            subject.MsgAction += (new MailMsg()).SendMailMsg;
            subject.MsgAction += (new SMSMsg()).SendSMSMsg;

            subject.Notify();

            Console.ReadKey();
        }
    }
}
