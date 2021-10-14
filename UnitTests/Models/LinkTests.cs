using FluentAssertions;
using SquidEyes.UrlBundler.Common.Models;
using SquidEyes.UrlBundler.Common.Validators;
using System;
using Xunit;
using static SquidEyes.UrlBundler.Common.Models.LinkStatus;

namespace SquidEyes.UnitTests
{
    public class LinkTests
    {
        private const string URL = "http://someco.com";

        [Theory]
        [InlineData(true, "aaa", HasInfo, "ABC", "XYZ", 0)]
        [InlineData(true, "aaa", NoInfo, null, null, 0)]
        [InlineData(true, "aaa", New, null, null, 0)]
        [InlineData(true, "aaa", HasInfo, null, "XYZ", 1)]
        [InlineData(true, "aaa", HasInfo, "", "XYZ", 2)]
        [InlineData(true, "aaa", HasInfo, " ABC", "XYZ", 1)]
        [InlineData(true, "aaa", HasInfo, "ABC ", "XYZ", 1)]
        [InlineData(true, "aaa", HasInfo, "ABC", null, 1)]
        [InlineData(true, "aaa", HasInfo, "ABC", "", 2)]
        [InlineData(true, "aaa", HasInfo, "ABC", " XYZ", 1)]
        [InlineData(true, "aaa", HasInfo, "ABC", "XYZ ", 1)]        
        [InlineData(true, "aaa", New, "ABC", "XYZ", 1)]
        [InlineData(true, "aaa", New, null, "XYZ", 1)]
        [InlineData(true, "aaa", New, "ABC", null, 1)]
        [InlineData(true, "aaa", New, "", "XYZ", 2)]
        [InlineData(true, "aaa", New, "ABC", "", 2)]
        [InlineData(true, "aaa", NoInfo, "ABC", "XYZ", 1)]
        [InlineData(true, "aaa", NoInfo, null, "XYZ", 1)]
        [InlineData(true, "aaa", NoInfo, "ABC", null, 1)]
        [InlineData(true, "aaa", NoInfo, "", "XYZ", 2)]
        [InlineData(true, "aaa", NoInfo, "ABC", "", 2)]
        [InlineData(false, "aaa", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(false, "aaa", New, null, null, 1)]
        [InlineData(false, "aaa", NoInfo, null, null, 1)]
        [InlineData(true, null, HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, "", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, " aaa", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, "aaa ", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, "a@a", HasInfo, "ABC", "XYZ", 1)]
        public void StatusValidatorWorksAsExpected(bool hasUri, string alias, 
            LinkStatus status, string title, string excerpt, int errorCount)
        {
            var link = new Link()
            {
                Alias = alias,
                Uri = hasUri ? new Uri(URL) : null,
                Status = status,
                Title = title,
                Excerpt = excerpt
            };

            var validator = new LinkValidator();

            var result = validator.Validate(link);

            result.Errors.Count.Should().Be(errorCount);
        }
    }
}
