Feature: IMDB


@addMovie
Scenario: Adding Movie to the list
	Given I have a movie with title "The Shawshank Redemption"
	And the year of release is "1994"
	And the plot is "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."
	And the actors are "6,7"
	And the producer is "3"
	When I add the movie in IMDB app
	Then IMDB app would look like this
	| Title                    | YearofRelease | Plot                                                                                                                          | Actors | Producer |
	| Inception                | 2010          | A thief who enters the dreams of others to steal their secrets must plant an idea into someone's mind in order to return home. | 3,5    | 1        |
	| The Shawshank Redemption | 1994          | Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.        | 6,7    | 3        |
	
	
	

@addMovie
Scenario: Don't add empty movie name
	Given I have a movie with title ""
	And the year of release is "1994"
	And the plot is "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency."
	And the actors are "6,7"
	And the producer is "3"
	When I add the movie in IMDB app
	Then Then I should have an error "TITLE CANNOT BE EMPTY!!! PLEASE TRY AGAIN  --------------------------------------"
	And IMDB app would look like this
	| Title | YearofRelease | Plot | Actors | Producer |
	| Inception                | 2010          | A thief who enters the dreams of others to steal their secrets must plant an idea into someone's mind in order to return home. | 3,5    | 1        |
	

@listMovie
Scenario: List all movies in app
	Given I have collection of movies
	When I fetch my movies
	Then I should have the following movies
	| Title | YearofRelease | Plot | Actors | Producer |
	| Inception                | 2010          | A thief who enters the dreams of others to steal their secrets must plant an idea into someone's mind in order to return home. | 3,5    | 1        |

@addActor
Scenario: Add actor in the app
	Given I have Actor Name "Leonardo DiCaprio"
	And I have Actor's DOB "1974-11-11"
	When I add actor in IMDB app
	Then Actor would be added in app
	| Name | DOB        |
	| Leonardo DiCaprio  | 1974-11-11 |

@addProducer
Scenario: Add producer in the app

Given I have Producer Name "Christopher Nolan"
And I have Producer's DOB "1970-07-30"
When I add producer in IMDB app
Then Producer would be added in app
| Name | DOB        |
| Christopher Nolan   | 1970-07-30 |


@deleteMovie
Scenario: Delete a movie from the app

	Given I have movie name "1"
	When I delete movie in IMDB app
	Then movie gets deleted and shows the result
	| Title | YearofRelease | Plot | Actors | Producer |
	