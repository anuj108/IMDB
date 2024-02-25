Feature: IMDB


@addMovie
Scenario: Adding Movie to the list
	Given I have a movie with title "a"
	And the year of release is "1"
	And the plot is "b"
	And the actors are
	| Actors |
	| c,d      |
	And the producer is "abcd"
	When I add the movie in IMDB app
	Then IMDB app would look like this
	| Title | YearofRelease | Plot | Actors | Producer |
	| d     | 2             | c    | w,e    | skld     |
	| a     | 1             | b    | c,d    | abcd     |
	
	

@addMovie
Scenario: Don't add empty movie name
	Given I have a movie with title ""
	And the year of release is "1"
	And the plot is "b"
	And the actors are
	| Actors |
	| c,d    |
	And the producer is "abcd"
	When I add the movie in IMDB app
	Then Then I should have an error "Invalid arguments"
	And IMDB app would look like this
	| Title | YearofRelease | Plot | Actors | Producer |
	| d     | 2             | c    | w,e    | skld     |
	

@listMovie
Scenario: List all movies in app
	Given I have collection of movies
	When I fetch my movies
	Then I should have the following movies
	| Title | YearofRelease | Plot | Actors | Producer |
	| d     | 2             | c    | w,e    | skld     |