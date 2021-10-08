using FluentAssertions;
using SquidEyes.UrlBundler.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static SquidEyes.UrlBundler.Common.Models.UrlInfoStatus;
using static System.UriKind;

namespace SquidEyes.UnitTests
{
    public class UrlInfoTests
    {
        private const string URL = "http://cnn.com";

        [Theory]
        [InlineData(true, null, "abc123", HasInfo, "ABC", "XYZ", Absolute, URL, 0)]
        [InlineData(true, null, "abc123", NoInfo, null, null, null, null, 0)]
        [InlineData(true, null, "abc123", New, null, null, null, null, 0)]
        [InlineData(true, "One", "abc123", HasInfo, "ABC", "XYZ", Absolute, URL, 0)]
        [InlineData(true, "One", "abc123", NoInfo, null, null, null, null, 0)]
        [InlineData(true, "One", "abc123", New, null, null, null, null, 0)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", "XYZ", Relative, "data", 1)]
        [InlineData(true, null, "abc123", HasInfo, null, "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", HasInfo, "", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", HasInfo, " ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC ", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", null, Absolute, URL, 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", "", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", " XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", HasInfo, "ABC", "XYZ ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", NoInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", NoInfo, null, "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", NoInfo, "ABC", null, Absolute, URL, 1)]
        [InlineData(true, null, "abc123", NoInfo, "ABC", "XYZ", null, null, 1)]
        [InlineData(true, null, "abc123", New, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", New, null, "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123", New, "ABC", null, Absolute, URL, 1)]
        [InlineData(true, null, "abc123", New, "ABC", "XYZ", null, null, 1)]
        [InlineData(false, null, "abc123", New, null, null, null, null, 1)]
        [InlineData(false, null, "abc123", NoInfo, null, null, null, null, 1)]
        [InlineData(false, null, "abc123", HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, null, HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "", HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, " abc123", HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, null, "abc123 ", HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, "", "abc123", HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, " AAA", "abc123", HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, "AAA ", "abc123", HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        [InlineData(true, "A--B", "abc123", HasInfo, "ABC", "XYZ", Absolute, URL, 1)]
        public void StatusValidatorWorksAsExpected(bool goodUri, string group,
            string code, UrlInfoStatus status, string title, string excerpt,
            UriKind? uriKind, string uriString, int errorCount)
        {
            var thumbUri = uriKind switch
            {
                null => null,
                Absolute => new Uri(uriString, Absolute),
                Relative => new Uri(uriString, Relative),
                _ => throw new ArgumentOutOfRangeException(nameof(uriKind))
            };

            var info = new UrlInfo()
            {
                Code = code,
                Group = group,
                Uri = goodUri ? new Uri(URL) : null,
                Status = status,
                Title = title,
                Excerpt = excerpt,
                ThumbUri = thumbUri
            };

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                info, new ValidationContext(info), results, true);

            isValid.Should().Be(errorCount == 0);

            results.Count.Should().Be(errorCount);
        }
    }
}
