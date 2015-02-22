Feature: View activities
  In order to ensure I report my time accurately
  As an employee of Scrooge McDuck
  I want view my activities at work

Scenario: View activities at start of the day
  Given I have registered myself
  And I have entered no activities for today
  When I selecte the "Add Actitivities" page
  Then I see today's date
  And I see an empty activies list
