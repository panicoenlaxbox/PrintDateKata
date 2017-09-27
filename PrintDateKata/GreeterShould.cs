using System;
using FluentAssertions;
using Xunit;

namespace PrintDateKata
{
    public class GreeterShould
    {
        [Fact]
        public void greet()
        {
            var writer = new SpyWriter();
            var now = DateTime.Now;
            var dateTimeRetriever = new StubDateTimeRetriever(now);
            var greeter = new Greeter(writer, dateTimeRetriever);
            greeter.Greet();
            writer.Output.Should().StartWith("Hi, " + now);
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
            _writer.Write("Hi, " + _dateTimeRetriever.GetCurrent());
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
