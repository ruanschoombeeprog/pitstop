Feature: WorkshopPlanning
	In order to effectively plan work shop resources
	As a memeber of the workshop staff.
	I want be able to plan and schedule maintenance jobs for the workshop

Scenario: Planned maintenance jobs should fall within one business day
	Given I have a new workshop planning for 14-11-2019
	When I plan a maintenance job
	Then I should have one job in my planning

Scenario: Maintenance jobs that fall outside of one working day are not allowed.
	Given I have a new workshop planning for 19-11-2019
	When I plan a maintenance job
	Then I should have no job in my planning

