using ApiTestingForSwapi.Client;
using ApiTestingForSwapi.Models.Request;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiTestingForSwapi.TestSuites.SwapiEndpoints
{
    [TestFixture]
    public class SwapiTestSuite
    {
        ExtentReports extent;
        ExtentReports report;
        ExtentHtmlReporter htmlReporter;
        ExtentTest test;

        string path = (AppDomain.CurrentDomain.BaseDirectory);

        [OneTimeSetUp]
        public void setUpReports()
        {
            htmlReporter = new ExtentHtmlReporter(path, AventStack.ExtentReports.Reporter.Configuration.ViewStyle.SPA);
            htmlReporter.Config.DocumentTitle = (@"Test reports.html");
            htmlReporter.Config.ReportName = ("API Tests Reports for Swapi");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
        
        [Test]
        [TestCase(1, "Tatooine")]
        public void GET_GetPlanetName_ShouldGetProperPlanetNameById(int peopleId, string planetName)
        {
            try
            {
                test = extent.CreateTest("GET_GetPlanetName_ShouldGetProperPlanetNameById");
                
                var client = new SwapiClient();

                var response = client.GetPeople(peopleId);

                var deserializedResponse = JsonConvert.DeserializeObject<People>(response.Content);

                string pattern = (@"(\d+)(?!.*\d)");
                Regex regex = new Regex(pattern);
                Match matchedId = regex.Match(deserializedResponse.Homeworld);
                matchedId.Value.ToString();

                var planetId = Convert.ToInt32(matchedId.Value);

                var response2 = client.GetPlanet(planetId);

                var deserializedResponse2 = JsonConvert.DeserializeObject<Planet>(response2.Content);

                deserializedResponse2.Name.Should().Be(planetName);

                test.Log(Status.Pass, "Test that check proper working of API");
            }
            catch (Exception exception)
            {
                test.Fail(exception.StackTrace);
                test.Log(Status.Fail, exception);
            }
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
        }
    }
}
