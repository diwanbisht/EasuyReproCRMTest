Feature: CreateAccounts
This feature is ablicable to create New Account in the CRM


Scenario: Account Creation Test Scenario
	Given I Navigated to 'Accounts' Home screen
	When I click button 'New'
	And I click button 'Save'
	Then I can see the value of 'Account Name : Required fields must be filled in.' as the header title
	When I enter the Account Name
	And I enter the PhoneNumber
	And I enter the WebSiteURL
	
	