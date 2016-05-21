using System;
using Xunit;

namespace MotleyFlash.Test
{
    public class MessageTypesTest
    {
        public MessageTypesTest()
        {
            guid = Guid.NewGuid().ToString();
        }

        private readonly string guid;

        [Fact]
        public void Sets_error_property()
        {
            var sut = new MessageTypes(error: guid);

            Assert.Equal(guid, sut.Error);
        }

        [Fact]
        public void Sets_information_property()
        {
            var sut = new MessageTypes(information: guid);

            Assert.Equal(guid, sut.Information);
        }

        [Fact]
        public void Sets_notice_property()
        {
            var sut = new MessageTypes(notice: guid);

            Assert.Equal(guid, sut.Notice);
        }

        [Fact]
        public void Sets_success_property()
        {
            var sut = new MessageTypes(success: guid);

            Assert.Equal(guid, sut.Success);
        }

        [Fact]
        public void Sets_warning_property()
        {
            var sut = new MessageTypes(warning: guid);

            Assert.Equal(guid, sut.Warning);
        }
    }
}
