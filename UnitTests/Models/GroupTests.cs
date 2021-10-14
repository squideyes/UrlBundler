using FluentAssertions;
using SquidEyes.UrlBundler.Common.Models;
using SquidEyes.UrlBundler.Common.Validators;
using System;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class GroupTests
    {
        public enum UriToUse
        {
            Null = 1,
            Relative,
            Absolute
        }

        [Theory]
        [InlineData("xxx", "XXX", true, 0)]
        [InlineData(null, "XXX", true, 1)]
        [InlineData("", "XXX", true, 1)]
        [InlineData(" xxx", "XXX", true, 1)]
        [InlineData("xxx ", "XXX", true, 1)]
        [InlineData("x@x", "XXX", true, 1)]
        [InlineData("xxx", null, true, 1)]
        [InlineData("xxx", "", true, 1)]
        [InlineData("xxx", " XXX", true, 1)]
        [InlineData("xxx", "XXX ", true, 1)]
        [InlineData("xxx", "XXX", false, 2)]
        public void GroupValidatorFullCoverage(
            string groupId, string title, bool goodLink, int errorCount)
        {
            var validator = new GroupValidator();

            var group = new Group()
            {
                GroupId = groupId,
                Title = title
            };

            if (goodLink)
            {
                group.Links.Add(new Link()
                {
                    Alias = "aaa",
                    Uri = new Uri("http://someco.com"),
                    Status = LinkStatus.HasInfo,
                    Title = "ABC",
                    Excerpt = "XXX"
                });
            }
            else
            {
                group.Links.Add(new Link());
            }

            var result = validator.Validate(group);

            result.Errors.Count.Should().Be(errorCount);
        }
    }
}
