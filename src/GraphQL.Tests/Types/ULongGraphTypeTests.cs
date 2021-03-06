using System;
using GraphQL.Types;
using Shouldly;
using Xunit;

namespace GraphQL.Tests.Types
{
    public class ULongGraphTypeTests : ScalarGraphTypeTest<ULongGraphType>
    {
        [Fact]
        public void Coerces_given_inputs_to_null()
            => type.ParseValue(null).ShouldBe(null);

        [Theory]
        [InlineData("18446744073709551616")]
        [InlineData(-1)]
        [InlineData("-1")]
        public void Coerces_given_inputs_to_overflow_exception(object input) =>
            AssertException<OverflowException>(input);

        [Theory]
        [InlineData("abc")]
        [InlineData("-999999.99")]
        public void Coerces_given_inputs_to_out_of_bound_exception(object input) =>
            AssertException<FormatException>(input);

        [Fact]
        public void Coerces_double_to_invalid_operation_exception()
            => AssertException<InvalidOperationException>(999999.99);

        [Theory]
        [InlineData("1844674407", 1844674407)]
        [InlineData("18446744073709551615", 18446744073709551615)]
        [InlineData(0, 0)]
        [InlineData(65535, 65535)]
        public void Coerces_input_to_valid_ulong(object input, ulong expected)
            => type.ParseValue(input).ShouldBe(expected);
    }
}
