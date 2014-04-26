Feature: Transaction History
	In order to show transaction history
	As a api consumer
	I want to be able to get list of transactions


Scenario: Get Transaction history
	Given I call get transactions for a certain accounts
	Then the result should contains list of transactions
