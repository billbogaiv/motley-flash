using Xunit;

namespace MotleyFlash.Test
{
    public class InMemoryMessageProviderTest
    {
        public InMemoryMessageProviderTest()
        {
            sut = new TestInMemoryMessageProvider();
        }

        private InMemoryMessageProvider sut;

        public class Get : InMemoryMessageProviderTest
        {
            [Theory,
                InlineData("test"),
                InlineData(1),
                InlineData(null)]
            public void Should_return_previously_set_item(object item)
            {
                (sut as TestInMemoryMessageProvider).SetItem(item);

                var messages = sut.Get();

                if (item != null)
                {
                    Assert.NotNull(messages);
                }

                Assert.Equal(item, messages);
            }
        }

        public class Set : InMemoryMessageProviderTest
        {
            [Theory,
                InlineData("test"),
                InlineData(1),
                InlineData(null)]
            public void Should_store_item(object item)
            {
                sut = new TestInMemoryMessageProvider();

                sut.Set(item);

                var messages = (sut as TestInMemoryMessageProvider).GetItem();

                if (item != null)
                {
                    Assert.NotNull(messages);
                }

                Assert.Equal(item, messages);
            }
        }

        private class TestInMemoryMessageProvider : InMemoryMessageProvider
        {
            public object GetItem()
            {
                return base.messages;
            }

            public void SetItem(object messages)
            {
                base.messages = messages;
            }
        }
    }
}
