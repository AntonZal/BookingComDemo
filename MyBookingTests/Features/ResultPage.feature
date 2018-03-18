Feature: ResultPage

Background: 
    #English Demo version
	Given I am on booking result page And I choose english language	
	
Scenario Outline: Change currency on result page
    When I change currency on result page to '<currency>'
	Then I will see currency changed to '<currency>' on head element on resultPage
	Then I will see currency changed to '<currency>' on postcards prices
Examples: 
    | currency |
    | CAD      |
    | ₪        |
    | US$      |

	#booking.com использует нестрогую сортировку
    #два следующих теста чаще всего падают

Scenario: Sort search results by rating
     When I click sort by rating button
	 Then I will see results sorted by rating

 Scenario: Sort search results by price
     When I click sort by price button
	 Then I will see results sorted by price

Scenario Outline: Filter search results by price
    # тест иногда падает из-за округления граничных данных при переводе валют
	# тест не учитывает скидки и различные сценарии учета этих скидок для различных валют
	 When I change currency on result page to '<currency>'
     When I select filter by price '<min price>', '<max price>'
	 Then I will see results filtered by price '<min price>', '<max price>'
Examples: 
   | currency | min price | max price |
   | CAD      | 0         | 79        |       
   | ₪        | 210       | 430       |     
   | US$      | 120       | 180       |     