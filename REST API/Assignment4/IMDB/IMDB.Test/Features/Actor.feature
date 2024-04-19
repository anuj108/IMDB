Feature: Actor

@GetAllActors
Scenario: Get All Actors
	Given I am a Client
	When I make a GET Request '/api/actors'
	Then response code must be '200'
	And response should look like '[{"id":1,"name":"Actor1","bio":"Bio1","dob":"1999-07-08T00:00:00","gender":"Male"},{"id":2,"name":"Actor2","bio":"Bio2","dob":"1999-07-08T00:00:00","gender":"Male"},{"id":3,"name":"Actor3","bio":"Bio3","dob":"1999-07-08T00:00:00","gender":"male"}]'

@GetActorById
Scenario: Get Actor By Id
	Given I am a Client
	When I make a GET Request '<resourceEndpoint>'
	Then response code must be '<status>'
	And response should look like '<response>'

	Examples: 
	| resourceEndpoint | status | response |
	| /api/actors/1    | 200    | {"id":1,"name":"Actor1","bio":"Bio1","dob":"1999-07-08T00:00:00","gender":"Male"}         |
	| /api/actors/2    | 200    | {"id":2,"name":"Actor2","bio":"Bio2","dob":"1999-07-08T00:00:00","gender":"Male"}         |
	|        /api/actors/0          | 404       | NO ACTOR FOUND       |

@CreateActor
Scenario: Create Actor
	Given I am a Client
	When I make a post request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseData>'

	Examples: 
	| resourceEndpoint | postData                                                                                            | statusCode | responseData                                                                                        |
	| /api/actors/          | {"name":"ABC","bio":"some info","dob":"1999-08-08","gender":"Male"} | 201        | 4 |
	| /api/actors/          | {"name":"","bio":"some info","dob":"1999-08-08","gender":"Male"}                  | 400        | INVALID ACTORNAME                                                                                       |
	| /api/actors/          | {"name":"ABC","bio":"","dob":"1999-08-08","gender":"Male"}                   | 400        | INVALID ACTOR BIO                                                                                  |
	| /api/actors/          | {"name":"ABC","bio":"some info","dob":"9900-08-08","gender":"Male"} | 400        | INVALID DATE OF BIRTH                                                                               |
	| /api/actors/          | {"name":"ABC","bio":"some info","dob":"1999-08-08","gender":"Malde"}     | 400        | INVALID GENDER                                                                                     |

@UpdateActor
Scenario: Update Actor
	Given I am a Client
	When I make a put request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples: 
	| resourceEndpoint | postData                                                                                            | statusCode | responseMessage                                                                                        |
	| /api/actors/1          | {"name":"Updated ABC","bio":"some info","dob":"1999-08-08","gender":"Male"} | 200        | ACTOR WITH THE GIVEN ID UPDATED: 1 |
	| /api/actors/1          | {"name":"","bio":"some info","dob":"1999-08-08","gender":"Male"}                  | 400        | INVALID ACTOR NAME                                                                                       |
	| /api/actors/1         | {"name":"ABC","bio":"","dob":"1999-08-08","gender":"Male"}                   | 400        | INVALID ACTOR BIO                                                                                  |
	| /api/actors/1          | {"name":"ABC","bio":"some info","dob":"9900-08-08","gender":"Male"} | 400        | INVALID DATE OF BIRTH                                                                               |
	| /api/actors/1          | {"name":"ABC","bio":"some info","dob":"1999-08-08","gender":"Malde"}     | 400        | INVALID GENDER                                                                                     |

@DeleteActor
Scenario: Delete Actor
	Given I am a Client
	When I make a delete request to '<resourceEndpoint>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples:
	| resourceEndpoint |  statusCode | responseMessage                                                                                        |
	| /api/actors/1          |  200        | ACTOR WITH THE GIVEN ID DELETED: 1 |
	| /api/actors/11         |  404        | NO ACTOR FOUND                                                                                      |
	