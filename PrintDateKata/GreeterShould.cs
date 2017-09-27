using System;
using FluentAssertions;
using Xunit;

namespace PrintDateKata
{
    static class ObjectMother
    {
        public static DateTime GetAfternoon()
        {
            return new DateTime(2017, 1, 1, 12, 0, 0);
        }

        public static DateTime GetMorning()
        {
            return new DateTime(2017, 1, 1, 11, 59, 59);
        }
    }
    public class GreeterShould
    {
        [Fact]
        public void greet_morning()
        {
            var writer = new SpyWriter();
            var now = ObjectMother.GetMorning();
            var dateTimeRetriever = new StubDateTimeRetriever(now);
            var greeter = new Greeter(writer, dateTimeRetriever);
            greeter.Greet();
            writer.Output.Should().StartWith("Hi, " + now + " morning");
        }

        [Fact]
        public void greet_afternoon()
        {
            var writer = new SpyWriter();
            var now = ObjectMother.GetAfternoon();
            var dateTimeRetriever = new StubDateTimeRetriever(now);
            var greeter = new Greeter(writer, dateTimeRetriever);
            greeter.Greet();
            writer.Output.Should().StartWith("Hi, " + now + " afternoon");
        }
    }

    public class Greeter
    {
        private readonly Writer _writer;
        private readonly DateTimeRetriever _dateTimeRetriever;

        public Greeter(Writer writer, DateTimeRetriever dateTimeRetriever)
        {
            _writer = writer;
            _dateTimeRetriever = dateTimeRetriever;
        }

        public void Greet()
        {
            var current = _dateTimeRetriever.GetCurrent();
            var greetingTime = "morning";
            if (current.Hour >= 12)
            {
                greetingTime = "afternoon";
            }
            _writer.Write($"Hi, {current} {greetingTime}");
        }
    }

    public class DateTimeRetriever
    {
        public virtual DateTime GetCurrent()
        {
            return DateTime.Now;
        }
    }

    class StubDateTimeRetriever : DateTimeRetriever
    {
        private readonly DateTime _dateTime;

        public StubDateTimeRetriever(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
        public override DateTime GetCurrent()
        {
            return _dateTime;
        }
    }


    public class Writer
    {
        public virtual void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class SpyWriter : Writer
    {
        public override void Write(string message)
        {
            Output = message;
        }

        public string Output { get; set; }
    }
}
