Feature: Transaction History
	In order to show transaction history
	As a api consumer
	I want to be able to get list of transactions

@mytag
Scenario: Get Transaction history
	Given I call for transactions for a certain client
	Then the result should contains list of transactions
