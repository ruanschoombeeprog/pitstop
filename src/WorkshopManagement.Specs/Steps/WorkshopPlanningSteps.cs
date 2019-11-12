using System;
using TechTalk.SpecFlow;

namespace WorkshopManagement.AcceptanceTests.Steps
{
    [Binding]
    public class WorkshopPlanningRetrievalSteps
    {
        [Given(@"The WebApp has requested the planning by date from the WorkshopManagementApi by passing the following parameters")]
        public void GivenTheWebAppHasRequestedThePlanningByDateFromTheWorkshopManagementApiByPassingTheFollowingParameters(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the controller calls the planning repository to GetWorkshopPlanningAsync")]
        public void WhenTheControllerCallsThePlanningRepositoryToGetWorkshopPlanningAsync()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the planning entries exist for the specified date")]
        public void WhenThePlanningEntriesExistForTheSpecifiedDate()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"a code '(.*)' is returned with the planning object in the response body")]
        public void ThenACodeIsReturnedWithThePlanningObjectInTheResponseBody(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"if the planning does not exist for specified date")]
        public void ThenIfThePlanningDoesNotExistForSpecifiedDate()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"a code '(.*)' is returned")]
        public void ThenACodeIsReturned(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
