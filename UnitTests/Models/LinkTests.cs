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
        //[InlineData(true, "aaa", NoInfo, null, null, 0)]
        //[InlineData(true, "aaa", New, null, null, 0)]
        //[InlineData(true, "aaa", HasInfo, "ABC", "XYZ", 0)]
        //[InlineData(true, "", HasInfo, "ABC", "XYZ", 1)]
        [InlineData(true, null, HasInfo, "ABC", "XYZ", 1)]
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
