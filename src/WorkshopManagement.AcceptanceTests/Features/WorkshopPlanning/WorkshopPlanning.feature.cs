// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace WorkshopManagement.AcceptanceTests.Features.WorkshopPlanning
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class WorkshopPlanningFeature : Xunit.IClassFixture<WorkshopPlanningFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "WorkshopPlanning.feature"
#line hidden
        
        public WorkshopPlanningFeature(WorkshopPlanningFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Workshop Planning", "\t\tIn order for the workshop to function seemlessly planning is of great importanc" +
                    "e. \r\n\t\tTo assist in planning, retrieving the planned schedule and job details sh" +
                    "ould be possible.\r\n\t\tThe client wants to see all planned jobs with their latest " +
                    "state by day.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Retrieve the workhsop schedule by date")]
        [Xunit.TraitAttribute("FeatureTitle", "Workshop Planning")]
        [Xunit.TraitAttribute("Description", "Retrieve the workhsop schedule by date")]
        [Xunit.TraitAttribute("Category", "WorkshopManagementOverview")]
        public virtual void RetrieveTheWorkhsopScheduleByDate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retrieve the workhsop schedule by date", null, new string[] {
                        "WorkshopManagementOverview"});
#line 7
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 8
  testRunner.Given("the user has selected the Workshop Management menu item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table1.AddRow(new string[] {
                        "planningDate",
                        "12-11-2019"});
#line 9
  testRunner.And("todays date is the following", ((string)(null)), table1, "And ");
#line 12
  testRunner.When("the WebApp loads the Webshop management page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
  testRunner.Then("the WebApp should display 1 job", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                WorkshopPlanningFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                WorkshopPlanningFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
