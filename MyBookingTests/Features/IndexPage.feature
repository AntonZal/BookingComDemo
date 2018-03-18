Feature: IndexPage
	
Background: 
	Given I am on booking index page	

Scenario Outline: Change language on Index page
	When I change language to '<language>'
	Then I will see language changed to '<language>'
Examples: 
	| language  |
	| Italiano  |
	| Български |
	| 简体中文   |

Scenario Outline: Change currency on Index page
    When I change currency to '<currency>'
	Then I will see currency changed to '<currency>' on head element
	Then I will see currency changed to '<currency>' on postcard prices
	Then I will see currency changed to '<currency>' on caruosel elements
	Examples: 
    | currency |
    | CAD      |
    | ₪        |
    | US$      |

Scenario: Check warning message when Destination is not filled
	When I fill in the Search form 	     
											# a number of days from today                                 
		| enter location | select location | checkin | checkout | adults | children | rooms |
		|                |                 |    10   |    12    | 2      | 0        | 1     |
	Then I will see warning message

Scenario: Check warning message when duration over 30 days
   When I fill in the Search form 	
        | enter location | select location | checkin | checkout | adults | children | rooms |
		| Minsk          |  Minsk          |    10   |   41     | 2      | 0        | 1     |
	Then I will see warning duration message

#The second way to create data inside script
Scenario: Check warning message when duration over 30 days 2   
   When I fill in the Search form 2	
        | enter location | select location | checkin | checkout | adults | children | rooms |
		| Minsk          |  Minsk          |    10   |   42     | 2      | 0        | 1     |
	Then I will see warning duration message