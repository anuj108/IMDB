Feature: Genre

@GetAllGenres
Scenario Outline: Get All Genres
	Given I am a Client
	When I make a GET Request '/api/Genres'
	Then response code must be '200'
	And response should look like '[{"id":1,"name":"Dummy"},{"id":2,"name":"Dummy2"},{"id":3,"name":"Dummy3"}]'

@GetGenreById
Scenario Outline: Get Genre By Id
	Given I am a Client
	When I make a GET Request '<resourceEndpoint>'
	Then response code must be '<status>'
	And response should look like '<response>'

	Examples: 
	| resourceEndpoint | status | response						  |
	| /api/Genres/1    | 200    | {"id":1,"name":"Dummy"}         |
	| /api/Genres/2    | 200    | {"id":2,"name":"Dummy2"}        |
	| /api/Genres/0    | 404    | EMPTY GENRE LIST			      |

@CreateGenre
Scenario Outline: Create Genre
	Given I am a Client
	When I make a post request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseData>'

	Examples: 
	| resourceEndpoint		| postData          | statusCode | responseData       |
	| /api/Genres/          | {"name":"Dummy2"} | 201        | 4				  |
	| /api/Genres/          | {"name":""}       | 400        | INVALID GENRE NAME |
	

@UpdateGenre
Scenario Outline: Update Genre
	Given I am a Client
	When I make a put request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples: 
	| resourceEndpoint		 | postData						| statusCode | responseMessage         |
	| /api/Genres/1          | {"name":"Updated Genre"}		| 200        | GENRE UPDATED WITH ID 1 |
	| /api/Genres/1          | {"name":""}                  | 400        | INVALID GENRE NAME      |
                                                                                  

@DeleteGenre
Scenario Outline: Delete Genre
	Given I am a Client
	When I make a delete request to '<resourceEndpoint>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples:
	| resourceEndpoint		 |  statusCode | responseMessage         |
	| /api/Genres/1          |  200        | GENRE DELETED WITH ID 1 |
	| /api/Genres/11         |  404        | NO GENRE FOUND          |
	