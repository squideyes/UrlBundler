using FluentAssertions;
using SquidEyes.UrlBundler.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static SquidEyes.UrlBundler.Common.Models.UrlInfoStatus;

namespace SquidEyes.UnitTests
{
    public class UrlInfoTests
    {
        private const string URL = "http://somesite.com";

        [Theory]
        [InlineData(true, null, "abc123", HasInfo, "ABC", "XYZ", 0)]
        [InlineData(true, null, "abc123", NoInfo, null, null, 0)]
        [InlineData(true, null, "abc123", New, null, null, 0)]
        [InlineData(true, "One", "abc123", HasInfo, "ABC", "XYZ", 0)]
        [InlineData(true, "One", "abc123", NoInfo, null, null, 0)]
        [InlineData(true, "One", "abc123", New, null, null, 0)]
        [InlineData(true, null, "abc123", HasInfo, null, "XYZ", 1)]
        [InlineData(true, null, "abc123", HasInfo, "", "XYZ", 1)]
        [InlineData(true, null, "abc123", HasInfo, " ABC", "XYZ", 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC ", "XYZ", 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", null, 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", "", 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", " XYZ", 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", "XYZ ", 1)]
        [InlineData(true, null, "abc123", NoInfo, "ABC", "XYZ", 1)]
        [InlineData(true, null, "abc123", NoInfo, null, "XYZ", 1)]
        [InlineData(true, null, "abc123", NoInfo, "ABC", null, 1)]
        [InlineData(true, null, "abc123", New, "ABC", "XYZ", 1)]
        [InlineData(true, null, "abc123", New, null, "XYZ", 1)]
        [InlineData(true, null, "abc123", New, "ABC", null, 1)]
        [InlineData(false, null, "abc123", New, null, null, 1)]
        [InlineData(false, null, "abc123", NoInfo, null, null, 1)]
        [InlineData(false, null, "abc123", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, null, null, HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, null, "", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, null, " abc123", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, null, "abc123 ", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, "", "abc123", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, " AAA", "abc123", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, "AAA ", "abc123", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, "A--B", "abc123", HasInfo, "ABC", "XYZ", 1)]
        public void StatusValidatorWorksAsExpected(bool hasUri, 
            string group, string code, UrlInfoStatus status, 
            string title, string excerpt, int errorCount)
        {
            var info = new UrlInfo()
            {
                Alias = code,
                Group = group,
                Uri = hasUri ? new Uri(URL) : null,
                Status = status,
                Title = title,
                Excerpt = excerpt
            };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                info, new ValidationContext(info), results, true);

            isValid.Should().Be(errorCount == 0);

            results.Count.Should().Be(errorCount);
        }
    }
}
