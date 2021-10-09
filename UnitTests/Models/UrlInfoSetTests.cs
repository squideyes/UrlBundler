using SquidEyes.UrlBundler.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class UrlInfoSetTests
    {
        [Fact]
        public void ValidPropertiesYieldNoErrors()
        {
            //var urlInfoSet = new UrlInfoSet()
            //{
            //    UrlSetId = "aaa-bbb",
            //    Description = "Descriptive Text",
            //    UserName = "somedude"
            //};

            //urlInfoSet.Groups.Add("Group1", "Group1 Description");

            //urlInfoSet.UrlInfos.Add(new UrlInfo()
            //{
            //    Status = UrlInfoStatus.New,
            //    Code = "ABC123",
            //    Title = "Title Text",
            //    Uri = new Uri("http://somesite.com"),
            //    Group = "Group1",
            //    Excerpt = "Longish descriptive except goes here"
            //});

            //Validator.ValidateObject(
            //    urlInfoSet, new ValidationContext(urlInfoSet), true);
        }
    }
}
