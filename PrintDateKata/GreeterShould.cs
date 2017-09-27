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
            Greeter greeter = new Greeter(writer);
            greeter.Greet();
            writer.Output.Should().StartWith("Hi, ");
        }
    }

    public class Greeter
    {
        private readonly Writer _writer;

        public Greeter(Writer writer)
        {
            _writer = writer;
        }

        public void Greet()
        {
            _writer.Write("Hi, " + DateTime.Now);
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
