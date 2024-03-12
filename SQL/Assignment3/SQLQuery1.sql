
use IMDB;

/* Write a query to get the age of the Actors in Days(Number of days). */
select  name,  DATEDIFF(DAY, Foundation.Actors.date_of_birth, GETDATE()) AS AgeInDays from Foundation.Actors


/* Write a query to get the list of Actors who have worked with a given producer KEVIN FEIGE */
select Distinct Foundation.Actors.name 
from Foundation.Producers 
inner join Foundation.Movies 
on Foundation.Producers.id=Foundation.Movies.producerId 
inner join Foundation.Actors_Movies 
on Foundation.Actors_Movies.movieId=Foundation.Movies.id 
inner join Foundation.Actors 
on Foundation.Actors.id=Foundation.Actors_Movies.actorId 
where Foundation.Producers.name='Kevin Feige'


/* Write a query to get the list of actors who have acted together in two or more movies. */

Select A1.name as Actor1,A2.name as Actor2
from Foundation.Actors_Movies AM1
join Foundation.Actors_Movies AM2
on AM1.movieId=AM2.movieId AND AM1.actorId>AM2.actorId
join Foundation.Actors A1
on A1.id=AM1.actorId
join Foundation.Actors A2
on A2.id=AM2.actorId
group by AM1.actorId,AM2.actorId,A1.name,A2.name
having Count(AM1.movieId)>1

/* Write a query to get the youngest actor. */
select Top 1 name from Foundation.Actors order by Foundation.Actors.dateOfBirth desc


/* Write a query to get the actors who have never worked together. */

select A1.name,A2.name
from Foundation.Actors A1 
cross join Foundation.Actors A2
Where A1.id<A2.id
And NOT EXISTS(Select AM1.actorId
from Foundation.Actors_Movies AM1
join Foundation.Actors_Movies AM2
on AM1.movieId=AM2.movieId And AM1.actorId<AM2.actorId
where A1.id=AM1.actorId AND A2.id=AM2.actorId)



/* Write a query to get the number of movies in each language. */
select Language,Count(*) AS [MOVIE COUNT] from Foundation.Movies
group by Language


/* Write a query to get me the total profit of all the movies in each language separately. */
select Language,sum(Profit) AS [Profit] from Foundation.Movies
group by Language

/* Write a query to get the total profit of movies which have actor 'Chris Evans' in each language. */
select A.name,M.Language,Sum(Profit) from 
Foundation.Actors_Movies AM
Join Foundation.Actors A
on AM.actorId=A.id
join Foundation.Movies M
on M.id=AM.movieId
group by A.name,M.Language
Having A.name='Chris Evans'


/*  STORED PROCEDURE   */

/* 
Write an SP to insert a movie:
a. Take the movie details
b. Take the Actor Details ( Actors IDs)
c. Takes the producer Details (Producer IDs)
d. Adds to the required tables.
*/

Create Proc usp_insert_a_movie
@id INT,
@title NVARCHAR(50),
@yearOfRelease INT,
@poster VARBINARY(MAX),
@producerId INT,
@plot NVARCHAR(MAX),
@Language VARCHAR(50),
@Profit INT,
@actorId VARCHAR(20)
As
Begin 
insert into Foundation.Movies
values(@id,@title,@yearOfRelease,@plot,@poster,@producerId,GETDATE(),null,@Language,@Profit)

INSERT INTO Foundation.Actors_Movies (movieId, actorId)
SELECT @id, value
FROM STRING_SPLIT(@actorId, ',');

End

usp_insert_a_movie @id=11,@title='ABCD',@yearOfRelease=2020,@plot='jbhkjbk',@poster=null,@producerId=2,@Language='Hindi',@Profit=100,@actorId='1,2,3'

/*
Write an SP to Delete the Movie
a. Takes the movie Id
*/

Create Proc usp_delete_movie
@id INT
As
Begin
delete from Foundation.Actors_Movies where movieId=@id
delete from Foundation.Movies where id=@id
End

usp_delete_movie 11

/*
Write an SP to Delete a Producer
a. Takes ProducerId
b. Delete the movies directed by the producer as well
*/

Create Proc usp_delete_producer
@id INT
as
Begin
delete from Foundation.Movies where producerId=@id
delete from Foundation.Producers where id=@id
End

usp_delete_producer 3

/*
Write an SP to Delete a Actor
a.Takes ActorId
*/

Create Proc usp_delete_actor
@id INT
as
Begin
delete from Foundation.Actors_Movies where actorId=@id
delete from Foundation.Actors where id=@id
End

usp_delete_actor 7
