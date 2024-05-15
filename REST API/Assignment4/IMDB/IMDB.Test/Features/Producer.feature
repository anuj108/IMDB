Feature: Producer

@GetAllProducers
Scenario Outline: Get All Producers
	Given I am a Client
	When I make a GET Request '/api/producers'
	Then response code must be '200'
	Then response should look like '[{"id":1,"name":"ABC","bio":"some info","dob":"1999-08-08T00:00:00","gender":"Male"},{"id":2,"name":"ABC","bio":"some info","dob":"1999-08-08T00:00:00","gender":"Male"}]'

@GetProducerById
Scenario Outline: Get Producer By Id
	Given I am a Client
	When I make a GET Request '<resourceEndpoint>'
	Then response code must be '<status>'
	And response should look like '<response>'

	Examples: 
	| resourceEndpoint	  | status | response																					|	
	| /api/Producers/1    | 200    | {"id":1,"name":"ABC","bio":"some info","dob":"1999-08-08T00:00:00","gender":"Male"}        |
	| /api/Producers/2    | 200    | {"id":2,"name":"ABC","bio":"some info","dob":"1999-08-08T00:00:00","gender":"Male"}        |
	| /api/Producers/0    | 404    | NO PRODUCER FOUND																			|

@CreateProducer
Scenario Outline: Create Producer
	Given I am a Client
	When I make a post request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseData>'

	Examples: 
	| resourceEndpoint		   | postData                                                            | statusCode | responseData            |
	| /api/Producers/          | {"name":"ABC","bio":"some info","dob":"1999-08-08","gender":"Male"} | 201        | 3						|
	| /api/Producers/          | {"name":"","bio":"some info","dob":"1999-08-08","gender":"Male"}    | 400        | INVALID PRODUCER NAME   |
	| /api/Producers/          | {"name":"ABC","bio":"","dob":"1999-08-08","gender":"Male"}          | 400        | INVALID BIO DATA        |
	| /api/Producers/          | {"name":"ABC","bio":"some info","dob":"9900-08-08","gender":"Male"} | 400        | INVALID DATE OF BIRTH   |
	| /api/Producers/          | {"name":"ABC","bio":"some info","dob":"1999-08-08","gender":"Malde"}| 400        | INVALID GENDER          |

@UpdateProducer
Scenario Outline: Update Producer
	Given I am a Client
	When I make a put request to '<resourceEndpoint>' with the following data '<postData>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples: 
	| resourceEndpoint			| postData                                                                    | statusCode | responseMessage             |
	| /api/Producers/1          | {"name":"Updated ABC","bio":"some info","dob":"1999-08-08","gender":"Male"} | 200        | PRODUCER UPDATED WITH ID: 1 |
	| /api/Producers/1          | {"name":"","bio":"some info","dob":"1999-08-08","gender":"Male"}            | 400        | INVALID PRODUCER NAME       |
	| /api/Producers/1			| {"name":"ABC","bio":"","dob":"1999-08-08","gender":"Male"}                  | 400        | INVALID BIO DATA            |
	| /api/Producers/1          | {"name":"ABC","bio":"some info","dob":"9900-08-08","gender":"Male"}		  | 400        | INVALID DATE OF BIRTH       |
	| /api/Producers/1          | {"name":"ABC","bio":"some info","dob":"1999-08-08","gender":"Malde"}		  | 400        | INVALID GENDER              |

@DeleteProducer
Scenario Outline: Delete Producer
	Given I am a Client
	When I make a delete request to '<resourceEndpoint>'
	Then response code must be '<statusCode>'
	And response should look like '<responseMessage>'

	Examples:
	| resourceEndpoint			|  statusCode | responseMessage             |
	| /api/Producers/1          |  200        | PRODUCER DELETED WITH ID: 1 |
	| /api/Producers/11         |  404        | NO PRODUCER FOUND           |
