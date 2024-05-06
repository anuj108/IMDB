Feature: Review

@GetAllReviews
Scenario: Get All Reviews
	Given I am a Client
	When I make a GET Request '/api/reviews'
	Then response code must be '200'
	And response should look like '[{"id":1,"message":"Dummy","movieName":"Foo"},{"id":2,"message":"Dummy2","movieName":"Foo"},{"id":3,"message":"Dummy3","movieName":"Foo"}]'

@GetReviewById
Scenario: Get Review By Id
	Given I am a Client
	When I make a GET Request '<resourceEndpoint>'
	Then response code must be '<status>'
	And response should look like '<response>'

	Examples: 
	| resourceEndpoint | status | response |
	| /api/Reviews/1    | 200    | {"id":1,"message":"Dummy","movieName":"Foo"}         |
	| /api/Reviews/2    | 200    | {"id":2,"message":"Dummy2","movieName":"Foo"}         |
	|        /api/Reviews/0          | 404       | NO REVIEW FOUND       |

@GetReviewsByMovieId
Scenario: Get Reviews By MovieId
	Given I am a Client
	When I make a GET Request '<resourceEndpoint>'
	Then response code must be '<status>'
	And response should look like '<response>'

	Examples: 
	| resourceEndpoint | status | response |
	| /api/Movies/1/Reviews    | 200    | [{"id":1,"message":"Dummy","movieName":"Foo"},{"id":2,"message":"Dummy2","movieName":"Foo"},{"id":3,"message":"Dummy3","movieName":"Foo"}]         |
	|        /api/Movies/0/Reviews          | 404       | NO REVIEW FOUND       |

@CreateReview
Scenario: Create Review
	Given I am a Client
	When I make a post request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseData>'

	Examples: 
	| resourceEndpoint | postData                                                                                            | statusCode | responseData                                                                                        |
	| /api/Reviews/          | {"message":"DummyMessage","movieId":1} | 201        | 4 |
	| /api/Reviews/          | {"message":""}                  | 400        | INVALID MESSAGE                                                                                       |
	

@UpdateReview
Scenario: Update Review
	Given I am a Client
	When I make a put request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples: 
	| resourceEndpoint | postData                                                                                            | statusCode | responseMessage                                                                                        |
	| /api/Reviews/1          | {"message":"DummyMessage","movieId":1} | 200        | REVIEW UPDATED WITH ID: 1 |
	| /api/Reviews/1          | {"message":"","movieId":1}                  | 400        | INVALID MESSAGE                                                                                       |
                                                                                  

@DeleteReview
Scenario: Delete Review
	Given I am a Client
	When I make a delete request to '<resourceEndpoint>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples:
	| resourceEndpoint |  statusCode | responseMessage                                                                                        |
	| /api/Reviews/1          |  200        | REVIEW DELETED WITH ID: 1 |
	| /api/Reviews/11         |  404        | NO REVIEW FOUND                                                                                      |
	