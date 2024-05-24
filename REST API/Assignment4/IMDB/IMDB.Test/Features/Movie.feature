Feature: Movie

@GetAllMovies
Scenario Outline: Get All movies
	Given I am a Client
	When I make a GET Request '/api/movies'
	Then response code must be '200'
	And response should look like '[{"id":1,"name":"Foo","yearOfRelease":2000,"plot":"something","actors":[{"id":1,"name":"Actor1","bio":"Bio1","dob":"1999-07-08T00:00:00","gender":"Male"}],"genres":[{"id":1,"name":"Dummy"}],"producer":{"id":1,"name":"ABC","bio":"some info","dob":"1999-08-08T00:00:00","gender":"Male"},"coverImage":"1.jpg"}]'

@GetMovieById
Scenario Outline: Get movie By Id
	Given I am a Client
	When I make a GET Request '<resourceEndpoint>'
	Then response code must be '<status>'
	And response should look like '<response>'

	Examples: 
	| resourceEndpoint | status | response																																																																												|
	| /api/movies/1    | 200    |{"id":1,"name":"Foo","yearOfRelease":2000,"plot":"something","actors":[{"id":1,"name":"Actor1","bio":"Bio1","dob":"1999-07-08T00:00:00","gender":"Male"}],"genres":[{"id":1,"name":"Dummy"}],"producer":{"id":1,"name":"ABC","bio":"some info","dob":"1999-08-08T00:00:00","gender":"Male"},"coverImage":"1.jpg"}      |
	| /api/movies/0	   | 404    | NO MOVIE FOUND																																																																										|

@GetMoviesByYear
Scenario Outline: Get movies by year
	Given I am a Client
	When I make a GET Request '<resourceEndpoint>'
	Then response code must be '<status>'
	And response should look like '<response>'

	Examples: 
	| resourceEndpoint				  | status | response																																																																											|
	| /api/movies/filter?year=2000    | 200    |[{"id":1,"name":"Foo","yearOfRelease":2000,"plot":"something","actors":[{"id":1,"name":"Actor1","bio":"Bio1","dob":"1999-07-08T00:00:00","gender":"Male"}],"genres":[{"id":1,"name":"Dummy"}],"producer":{"id":1,"name":"ABC","bio":"some info","dob":"1999-08-08T00:00:00","gender":"Male"},"coverImage":"1.jpg"}] |
	| /api/movies/0					  | 404    | NO MOVIE FOUND																																																																										|

@CreateMovie
Scenario Outline: Create movie
	Given I am a Client
	When I make a post request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseData>'

	Examples: 
	| resourceEndpoint		| postData																														| statusCode | responseData         |
	| /api/movies/          | {"Name":"Foo","YearOfRelease":2000,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":1,"CoverImage":"1.jpg"}	| 201        | 2					|
	| /api/movies/          | {"Name":"","YearOfRelease":2000,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":1,"CoverImage":"1.jpg"}       | 400        | INVALID MOVIE NAME   |
	| /api/movies/          | {"Name":"Foo","YearOfRelease":0,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":1,"CoverImage":"1.jpg"}       | 400        | INVALID YEAR         |
	| /api/movies/          | {"Name":"Foo","YearOfRelease":2000,"Plot":"","ActorIds":[1,2],"GenreIds":[],"ProducerId":1,"CoverImage":"1.jpg"}				| 400        | INVALID MOVIE PLOT   |
	| /api/movies/          | {"Name":"Foo","YearOfRelease":2000,"Plot":"Something","ActorIds":[],"GenreIds":[1],"ProducerId":1,"CoverImage":"1.jpg"}		| 404        | ACTORS EMPTY         |
	| /api/movies/          | {"Name":"Foo","YearOfRelease":2000,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":0,"CoverImage":"1.jpg"}    | 404        | PRODUCER INVALID     |
	| /api/movies/          | {"Name":"Foo","YearOfRelease":2000,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":1,"CoverImage":""}			| 400        | INVALID COVERIMAGE   |

@UpdateMovie
Scenario Outline: Update movie
	Given I am a Client
	When I make a put request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples: 
	| resourceEndpoint		 | postData																															 | statusCode | responseMessage           |
	| /api/movies/1          | {"Name":"FooUpdated","YearOfRelease":2000,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":1,"CoverImage":"1.jpg"} | 200        | MOVIE UPDATED WITH ID : 1 |
	| /api/movies/1          | {"Name":"","YearOfRelease":2000,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":1,"CoverImage":"1.jpg"}           | 400        | INVALID MOVIE NAME        |
	| /api/movies/1          | {"Name":"Foo","YearOfRelease":0,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":1,"CoverImage":"1.jpg"}           | 400        | INVALID YEAR              |
	| /api/movies/1          | {"Name":"Foo","YearOfRelease":2000,"Plot":"","ActorIds":[1,2],"GenreIds":[],"ProducerId":1,"CoverImage":"1.jpg"}					 | 400        | INVALID MOVIE PLOT        |
	| /api/movies/1          | {"Name":"Foo","YearOfRelease":2000,"Plot":"Something","ActorIds":[],"GenreIds":[1],"ProducerId":1,"CoverImage":"1.jpg"}			 | 404        | ACTORS EMPTY              |
	| /api/movies/1          | {"Name":"Foo","YearOfRelease":2000,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":0,"CoverImage":"1.jpg"}		 | 404        | PRODUCER INVALID          |
	| /api/movies/1          | {"Name":"Foo","YearOfRelease":2000,"Plot":"Something","ActorIds":[1,2],"GenreIds":[1],"ProducerId":1,"CoverImage":""}			 | 400        | INVALID COVERIMAGE        |

@DeleteMovie
Scenario Outline: Delete movie
	Given I am a Client
	When I make a delete request to '<resourceEndpoint>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples:
	| resourceEndpoint		 |  statusCode | responseMessage          |
	| /api/movies/1          |  200        | MOVIE DELETED WITH ID :1 |
	| /api/movies/111        |  404        | NO MOVIE FOUND           |
