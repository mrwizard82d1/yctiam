using System;
using TechTalk.SpecFlow;

namespace TimeIsMoneyBin.Specs
{
    [Binding]
    public class ViewActivitiesSteps
    {
        [Given(@"I have registered myself")]
        public void GivenIHaveRegisteredMyself()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have entered no activities for today")]
        public void GivenIHaveEnteredNoActivitiesForToday()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I selecte the ""(.*)"" page")]
        public void WhenISelecteThePage(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I see today's date")]
        public void ThenISeeTodaySDate()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I see an empty activies list")]
        public void ThenISeeAnEmptyActiviesList()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
