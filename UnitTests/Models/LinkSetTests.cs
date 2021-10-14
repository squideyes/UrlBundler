using SquidEyes.UrlBundler.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class LinkSetTests
    {
        [Fact]
        public void ValidPropertiesYieldNoErrors()
        {
            var linkSet = new LinkSet()
            {
                LinkSetId = "faves",
                Title = "My Favorites",
                UserName = "somedude@someco.com",
            };

            linkSet.Groups.Add(new Group()
            {
                GroupId = "astronomy",
                Title = "Astronomy Links",
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Status = LinkStatus.New,
                        Alias = "aaa-bbb",
                        Title = "Some Link",
                        Uri = new Uri("http://somesite.com"),
                        Excerpt = "Short excerpt goes here"
                    }
                }
            });

            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(
                linkSet, new ValidationContext(linkSet), results, true))
            {
            }
        }
    }
}
