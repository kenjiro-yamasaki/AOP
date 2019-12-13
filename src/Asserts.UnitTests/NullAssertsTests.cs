using System;
using Xunit;
using Xunit.Sdk;

namespace SoftCube.Asserts.UnitTests
{
    using XAssert = Xunit.Assert;

    public class NullAssertsTests
    {
        public class NotNull
        {
            [Fact]
            public void NotNullを指定_例外を投げない()
            {
                Assert.NotNull(new object());
            }

            [Fact]
            public void Nullを指定_例外を投げる()
            {
                var ex = XAssert.Throws<NotNullException>(() => Assert.NotNull(null));

                XAssert.Equal("Assert.NotNull() Failure", ex.Message);
            }
        }

        public class Null
        {
            [Fact]
            public void Nullを指定_例外を投げない()
            {
                Assert.Null(null);
            }

            [Fact]
            public void NotNullを指定_例外を投げる()
            {
                var ex = XAssert.Throws<NullException>(() => Assert.Null(new object()));

                XAssert.Equal("Assert.Null() Failure" + Environment.NewLine + "Expected: (null)" + Environment.NewLine + "Actual:   Object { }", ex.Message);
            }
        }
    }
}

