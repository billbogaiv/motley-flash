using System;
using Xunit;

namespace MotleyFlash.Test
{
    public class MessageTest
    {
        private Message sut;

        public class Constructor : MessageTest
        {
            [Fact]
            public void Should_set_message()
            {
                sut = new Message("test message");

                Assert.Equal("test message", sut.Text);
            }

            [Theory,
                InlineData(null),
                InlineData("")]
            public void Should_throw_exception_if_message_is_not_valid(string text)
            {
                var exception = Assert.Throws<ArgumentNullException>(() => new Message(text));

                Assert.Equal("text", exception.ParamName);
            }

            [Fact]
            public void Should_set_title()
            {
                sut = new Message(
                    text: "test message",
                    title: "test");

                Assert.Equal("test", sut.Title);
            }

            [Fact]
            public void Should_set_type()
            {
                sut = new Message(
                    text: "test message",
                    type: "test");

                Assert.Equal("test", sut.Type);
            }
        }
    }
}
