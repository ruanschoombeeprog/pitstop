Feature: Workshop Planning
		In order for the workshop to function seemlessly planning is of great importance. 
		To assist in planning, retrieving the planned schedule and job details should be possible.
		The client wants to see all planned jobs with their latest state by day.

@WorkshopManagementOverview
Scenario: Retrieve the workhsop schedule by date
		Given the user has selected the Workshop Management menu item
		And todays date is the following
		| Field			| Value			|
		| planningDate  | 12-11-2019	|
		When the WebApp loads the Webshop management page
		Then the WebApp should display 1 job