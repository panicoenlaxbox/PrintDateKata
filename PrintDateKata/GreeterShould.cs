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
            var writer = new Writer();
            var greeter = new Greeter(writer);
            var message = DateTime.Now.ToString();
            greeter.Greet(message);
            writer.Output.Should().Be(message);
        }
    }

    public class Greeter
    {
        private readonly Writer _writer;

        public Greeter(Writer writer)
        {
            _writer = writer;
        }

        public void Greet(string message)
        {
            _writer.Write(message);
        }
    }

    public class Writer
    {
        public void Write(string message)
        {
            Output = message;
        }

        public string Output { get; set; }
    }
}
