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
        private readonly IWriter _writer;

        public Greeter(IWriter writer)
        {
            _writer = writer;
        }

        public void Greet()
        {
            _writer.Write("Hi, " + DateTime.Now);
        }
    }

    public interface IWriter
    {
        void Write(string message);
    }

    public class SpyWriter : IWriter
    {
        public void Write(string message)
        {
            Output = message;
        }

        public string Output { get; set; }
    }
}
