using System;
using System.Text.RegularExpressions;
using Xunit;

namespace SoftCube.Asserts.UnitTests
{
    using XAssert = Xunit.Assert;

    public class StringAssertsTests
    {
        public class Contains
        {
            [Fact]
            public void 部分文字列を含む_例外を投げない()
            {
                Assert.Contains("wor", "Hello, world!");
            }

            [Fact]
            public void 部分文字列の大文字小文字が違う_例外を投げる()
            {
                var ex = XAssert.Throws<ContainsException>(() => Assert.Contains("WORLD", "Hello, world!"));

                XAssert.Equal("Assert.Contains() Failure" + Environment.NewLine + "Not found: WORLD" + Environment.NewLine + "In value:  Hello, world!", ex.Message);
            }

            [Fact]
            public void 部分文字列を含まない_例外を投げる()
            {
                XAssert.Throws<ContainsException>(() => Assert.Contains("hey", "Hello, world!"));
            }

            [Fact]
            public void 実測値にnullを指定_例外を投げる()
            {
                XAssert.Throws<ContainsException>(() => Assert.Contains("foo", (string)null));
            }

            [Fact]
            public void 期待値にnullを指定_例外を投げる()
            {
                XAssert.Throws<ArgumentNullException>(() => Assert.Contains((string)null, "Hello, world!"));
            }
        }

        public class Contains_WithStringComparison
        {
            [Fact]
            public void OrdinalIgnoreCase_部分文字列の大文字小文字の違いを無視する()
            {
                Assert.Contains("WORLD", "Hello, world!", StringComparison.OrdinalIgnoreCase);
            }
        }

        public class DoesNotContain
        {
            [Fact]
            public void 部分文字列を含まない_例外を投げない()
            {
                Assert.DoesNotContain("hey", "Hello, world!");
            }

            [Fact]
            public void 部分文字列の大文字小文字が違う_例外を投げない()
            {
                Assert.DoesNotContain("WORLD", "Hello, world!");
            }

            [Fact]
            public void 部分文字列を含む_例外を投げる()
            {
                XAssert.Throws<DoesNotContainException>(() => Assert.DoesNotContain("world", "Hello, world!"));
            }

            [Fact]
            public void 実測値にnullを指定_例外を投げない()
            {
                Assert.DoesNotContain("foo", (string)null);
            }

            [Fact]
            public void 期待値にnullを指定_例外を投げる()
            {
                XAssert.Throws<ArgumentNullException>(() => Assert.DoesNotContain((string)null, "Hello, world!"));
            }
        }

        public class DoesNotContain_WithStringComparison
        {
            [Fact]
            public void OrdinalIgnoreCase_部分文字列の大文字小文字の違いを無視する()
            {
                XAssert.Throws<DoesNotContainException>(() => Assert.DoesNotContain("WORLD", "Hello, world!", StringComparison.OrdinalIgnoreCase));
            }
        }

        public class Equal
        {
            [Theory]
            [InlineData(null, null, false, false, false)]
            [InlineData("foo", "foo", false, false, false)]
            [InlineData("foo", "FoO", true, false, false)]
            [InlineData("foo \r\n bar", "foo \r bar", false, true, false)]
            [InlineData("foo \r\n bar", "foo \n bar", false, true, false)]
            [InlineData("foo \n bar", "foo \r bar", false, true, false)]
            [InlineData(" ", "\t", false, false, true)]
            [InlineData(" \t", "\t ", false, false, true)]
            [InlineData("    ", "\t", false, false, true)]
            public void 例外を投げない(string value1, string value2, bool ignoreCase, bool ignoreLineEndingDifferences, bool ignoreWhiteSpaceDifferences)
            {
                Assert.Equal(value1, value2, ignoreCase, ignoreLineEndingDifferences, ignoreWhiteSpaceDifferences);
                Assert.Equal(value2, value1, ignoreCase, ignoreLineEndingDifferences, ignoreWhiteSpaceDifferences);
            }

            [Theory]
            [InlineData(null, "", false, false, false, -1, -1)]
            [InlineData("", null, false, false, false, -1, -1)]
            [InlineData("foo", "foo!", false, false, false, 3, 3)]
            [InlineData("foo", "foo\0", false, false, false, 3, 3)]
            [InlineData("foo bar", "foo   Bar", false, true, true, 4, 6)]
            [InlineData("foo \nbar", "FoO  \rbar", true, false, true, 4, 5)]
            [InlineData("foo\n bar", "FoO\r\n  bar", true, true, false, 5, 6)]
            public void 例外を投げる(string expected, string actual, bool ignoreCase, bool ignoreLineEndingDifferences, bool ignoreWhiteSpaceDifferences, int expectedIndex, int actualIndex)
            {
                var exception      = Record.Exception(() => Assert.Equal(expected, actual, ignoreCase, ignoreLineEndingDifferences, ignoreWhiteSpaceDifferences));
                var equalException = XAssert.IsType<EqualException>(exception);

                XAssert.Equal(expectedIndex, equalException.ExpectedIndex);
                XAssert.Equal(actualIndex, equalException.ActualIndex);
            }
        }

        public class StartsWith
        {
            [Fact]
            public void 部分文字列から始まる_例外を投げない()
            {
                Assert.StartsWith("Hello", "Hello, world!");
            }

            [Fact]
            public void 部分文字列の大文字小文字が違う_例外を投げる()
            {
                var ex = Record.Exception(() => Assert.StartsWith("HELLO", "Hello"));

                XAssert.IsType<StartsWithException>(ex);
                XAssert.Equal("Assert.StartsWith() Failure:" + Environment.NewLine + "Expected: HELLO" + Environment.NewLine + "Actual:   Hello", ex.Message);
            }

            [Fact]
            public void 部分文字列から始まらない_例外を投げる()
            {
                XAssert.Throws<StartsWithException>(() => Assert.StartsWith("hey", "Hello, world!"));
            }

            [Fact]
            public void 実測値にnullを指定_例外を投げる()
            {
                XAssert.Throws<StartsWithException>(() => Assert.StartsWith("foo", null));
            }

            [Fact]
            public void 期待値にnullを指定_例外を投げる()
            {
                XAssert.Throws<ArgumentNullException>(() => Assert.StartsWith((string)null, "Hello, world!"));
            }
        }

        public class StartsWith_WithStringComparison
        {
            [Fact]
            public void OrdinalIgnoreCase_部分文字列の大文字小文字の違いを無視する()
            {
                Assert.StartsWith("HELLO", "Hello, world!", StringComparison.OrdinalIgnoreCase);
            }
        }

        public class EndsWith
        {
            [Fact]
            public void 部分文字列で終わる_例外を投げない()
            {
                Assert.EndsWith("world!", "Hello, world!");
            }

            [Fact]
            public void 部分文字列の大文字小文字が違う_例外を投げる()
            {
                var ex = Record.Exception(() => Assert.EndsWith("WORLD!", "world!"));

                XAssert.IsType<EndsWithException>(ex);
                XAssert.Equal("Assert.EndsWith() Failure:" + Environment.NewLine + "Expected: WORLD!" + Environment.NewLine + "Actual:   world!", ex.Message);
            }

            [Fact]
            public void 部分文字列で終わらない_例外を投げる()
            {
                XAssert.Throws<EndsWithException>(() => Assert.EndsWith("hey", "Hello, world!"));
            }

            [Fact]
            public void 実測値にnullを指定_例外を投げる()
            {
                XAssert.Throws<EndsWithException>(() => Assert.EndsWith("foo", null));
            }

            [Fact]
            public void 期待値にnullを指定_例外を投げる()
            {
                XAssert.Throws<ArgumentNullException>(() => Assert.EndsWith((string)null, "Hello, world!"));
            }
        }

        public class EndsWith_WithStringComparison
        {
            [Fact]
            public void OrdinalIgnoreCase_部分文字列の大文字小文字の違いを無視する()
            {
                Assert.EndsWith("WORLD!", "Hello, world!", StringComparison.OrdinalIgnoreCase);
            }
        }

        public class Matches_WithString
        {
            [Fact]
            public void 正規表現にマッチする_例外を投げない()
            {
                Assert.Matches(@"\w", "Hello");
            }

            [Fact]
            public void 正規表現にマッチしない_例外を投げる()
            {
                var ex = Record.Exception(() => Assert.Matches(@"\d+", "Hello, world!"));

                XAssert.IsType<MatchesException>(ex);
                XAssert.Equal("Assert.Matches() Failure:" + Environment.NewLine + @"Regex: \d+" + Environment.NewLine + "Value: Hello, world!", ex.Message);
            }

            [Fact]
            public void 正規表現にnullを指定_例外を投げる()
            {
                XAssert.Throws<ArgumentNullException>(() => Assert.Matches((string)null, "Hello, world!"));
            }

            [Fact]
            public void 実測値にnullを指定_例外を投げる()
            {
                XAssert.Throws<MatchesException>(() => Assert.Matches(@"\w+", (string)null));
            }
        }

        public class Matches_WithRegex
        {
            [Fact]
            public void 正規表現にマッチする_例外を投げない()
            {
                Assert.Matches(new Regex(@"\w+"), "Hello");
            }

            [Fact]
            public void 正規表現オプションを使う()
            {
                Assert.Matches(new Regex(@"[a-z]+", RegexOptions.IgnoreCase), "HELLO");
            }

            [Fact]
            public void 正規表現にマッチしない_例外を投げる()
            {
                var ex = Record.Exception(() => Assert.Matches(new Regex(@"\d+"), "Hello, world!"));

                XAssert.IsType<MatchesException>(ex);
                XAssert.Equal("Assert.Matches() Failure:" + Environment.NewLine + @"Regex: \d+" + Environment.NewLine + "Value: Hello, world!", ex.Message);
            }

            [Fact]
            public void 実測値にnullを指定_例外を投げる()
            {
                XAssert.Throws<MatchesException>(() => Assert.Matches(new Regex(@"\w+"), (string)null));
            }

            [Fact]
            public void 正規表現にnullを指定_例外を投げる()
            {
                XAssert.Throws<ArgumentNullException>(() => Assert.Matches((Regex)null, "Hello, world!"));
            }
        }

        public class DoesNotMatch_WithString
        {
            [Fact]
            public void 正規表現にマッチしない_例外を投げない()
            {
                Assert.DoesNotMatch(@"\d", "Hello");
            }

            [Fact]
            public void 正規表現にマッチする_例外を投げる()
            {
                var ex = Record.Exception(() => Assert.DoesNotMatch(@"\w", "Hello, world!"));

                XAssert.IsType<DoesNotMatchException>(ex);
                XAssert.Equal("Assert.DoesNotMatch() Failure:" + Environment.NewLine + @"Regex: \w" + Environment.NewLine + "Value: Hello, world!", ex.Message);
            }

            [Fact]
            public void 正規表現にnullを指定_例外を投げる()
            {
                XAssert.Throws<ArgumentNullException>(() => Assert.DoesNotMatch((string)null, "Hello, world!"));
            }

            [Fact]
            public void 実測値にnullを指定_例外を投げない()
            {
                Assert.DoesNotMatch(@"\w+", (string)null);
            }
        }

        public class DoesNotMatch_WithRegex
        {
            [Fact]
            public void 正規表現にマッチしない_例外を投げない()
            {
                Assert.DoesNotMatch(new Regex(@"\d"), "Hello");
            }

            [Fact]
            public void 正規表現にマッチする_例外を投げる()
            {
                var ex = Record.Exception(() => Assert.DoesNotMatch(new Regex(@"\w"), "Hello, world!"));

                XAssert.IsType<DoesNotMatchException>(ex);
                XAssert.Equal("Assert.DoesNotMatch() Failure:" + Environment.NewLine + @"Regex: \w" + Environment.NewLine + "Value: Hello, world!", ex.Message);
            }

            [Fact]
            public void 正規表現にnullを指定_例外を投げる()
            {
                XAssert.Throws<ArgumentNullException>(() => Assert.DoesNotMatch((Regex)null, "Hello, world!"));
            }

            [Fact]
            public void 実測値にnullを指定_例外を投げない()
            {
                Assert.DoesNotMatch(new Regex(@"\w+"), (string)null);
            }
        }
    }
}
