Feature: WorkshopPlanning
	In order to effectively plan work shop resources
	As a memeber of the workshop staff.
	I want be able to plan and schedule maintenance jobs for the workshop

Scenario Outline: Maintenance jobs can be scheduled
	Given I have a new workshop planning for <date>
	And the <startTime> and <endTime> is set for vehicle <vehicleId>
	When I plan a maintenance job
	Then the WorkshopPlanning should contain <numberJobs> jobs

	Examples:
		| date       | startTime           | endTime             | vehicleId                            | numberJobs |
		| 14-11-2019 | 14-11-2019 08:00:00 | 14-11-2019 10:00:00 | 02f89e3b-b542-4f9b-9fa3-de3be8b35b9a | 1          |
		| 14-11-2019 | 14-11-2019 08:00:00 | 14-11-2019 10:00:00 | 02f89e3b-b542-4f9b-9fa3-de3be8b35b9b | 2          |
		| 14-11-2019 | 14-11-2019 08:00:00 | 14-11-2019 10:00:00 | 02f89e3b-b542-4f9b-9fa3-de3be8b35b9c | 3          |
		| 14-11-2019 | 14-11-2019 08:00:00 | 15-11-2019 10:00:00 | 02f89e3b-b542-4f9b-9fa3-de3be8b35b9a | 3          |
		| 14-11-2019 | 14-11-2019 08:00:00 | 14-11-2019 10:00:00 | 02f89e3b-b542-4f9b-9fa3-de3be8b35b9d | 3          |
		| 14-11-2019 | 14-11-2019 09:00:00 | 14-11-2019 10:00:00 | 02f89e3b-b542-4f9b-9fa3-de3be8b35b9a | 3          |
		| 14-11-2019 | 14-11-2019 12:00:00 | 14-11-2019 14:00:00 | 02f89e3b-b542-4f9b-9fa3-de3be8b35b9a | 4          |

Scenario: Selecting a date returns the maintenance jobs planned for the date in question
	Given I have a existing workshop planning
	When I view the planning for that date
	Then I see 4 maintenance job in the workshop planning

Scenario: Selecting a planned maintenance job returns the job details
	Given I have a existing workshop planning
	When I select a maintenance job
	Then I see the job status is Planned

Scenario: After a maintennance job has been completed the job is marked as finished
	Given I have a existing workshop planning
	When I select a maintenance job
	And complete the selected job
	Then I see the job status is Finished