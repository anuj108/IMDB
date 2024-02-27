Feature: IMDB


@addMovie
Scenario: Adding Movie to the list
	Given I have a movie with title "a"
	And the year of release is "2002"
	And the plot is "b"
	And the actors are
	| Actors |
	| c,d      |
	And the producer is "abcd"
	When I add the movie in IMDB app
	Then IMDB app would look like this
	| Title | YearofRelease | Plot | Actors | Producer |
	| d     | 2002             | c    | w,e    | skld     |
	| a     | 2002             | b    | c,d    | abcd     |
	
	

@addMovie
Scenario: Don't add empty movie name
	Given I have a movie with title ""
	And the year of release is "2002"
	And the plot is "b"
	And the actors are
	| Actors |
	| c,d    |
	And the producer is "abcd"
	When I add the movie in IMDB app
	Then Then I should have an error "TITLE CANNOT BE EMPTY!!!"
	And IMDB app would look like this
	| Title | YearofRelease | Plot | Actors | Producer |
	| d     | 2002             | c    | w,e    | skld     |
	

@listMovie
Scenario: List all movies in app
	Given I have collection of movies
	When I fetch my movies
	Then I should have the following movies
	| Title | YearofRelease | Plot | Actors | Producer |
	| d     | 2002             | c    | w,e    | skld     |

@addActor
Scenario: Add actor in the app
	Given I have Actor Name "ABC"
	And I have Actor's DOB "2002-02-02"
	When I add actor in IMDB app
	Then Actor would be added in app
	| Name | DOB        |
	| ABC  | 2002-02-02 |

@addProducer
Scenario: Add producer in the app

Given I have Producer Name "AV"
And I have Producer's DOB "1990-02-02"
When I add producer in IMDB app
Then Producer would be added in app
| Name | DOB        |
| AV   | 1990-02-02 |


@deleteMovie
Scenario: Delete a movie from the app

	Given I have movie name "d"
	When I delete movie in IMDB app
	Then movie gets deleted and shows the result
	| Title | YearofRelease | Plot | Actors | Producer |
	