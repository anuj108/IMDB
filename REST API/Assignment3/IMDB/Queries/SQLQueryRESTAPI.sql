use IMDB
---------------STORED PROCEDURES-----------------------

--INSERTING MOVIE--
CREATE PROCEDURE Foundation.[usp_Insert_Movie]
@Name VARCHAR(100),
@YearofRelease INT,
@Plot VARCHAR(1000),
@CoverImage VARCHAR(1000),
@ProducerId INT,
@GenreIds VARCHAR(100),
@ActorIds VARCHAR(100)
AS
BEGIN
	INSERT INTO FOUNDATION.Movies(Name,YearofRelease,Plot,CoverImage,ProducerId)
	VALUES(@Name,@YearofRelease,@Plot,@CoverImage,@ProducerId)
	DECLARE @MovieId INT = (SELECT MAX(Id) FROM FOUNDATION.Movies)
	INSERT INTO FOUNDATION.Actors_Movies(MovieId,ActorId)
	SELECT @MovieId, value
	FROM STRING_SPLIT(@ActorIds, ',');	
	INSERT INTO FOUNDATION.Genres_Movies(MovieId,GenreId)
	SELECT @MovieId, value
	FROM STRING_SPLIT(@GenreIds, ',');	
END;

--UPDATING MOVIE--
create proc Foundation.usp_Update_Movie 
@Id INT,
@Name VARCHAR(100),
@YearofRelease INT,
@Plot VARCHAR(1000),
@CoverImage VARCHAR(1000),
@ProducerId INT,
@GenreIds VARCHAR(100),
@ActorIds VARCHAR(100)
As
BEGIN
UPDATE Foundation.Movies
SET Name=@Name,
YearofRelease=@YearofRelease,
Plot=@Plot,
CoverImage=@CoverImage,
ProducerId=@ProducerId
WHERE ID=@Id

DELETE FROM Foundation.Actors_Movies
WHERE MovieId=@Id

DELETE FROM Foundation.Genres_Movies
WHERE MovieId=@Id



INSERT INTO Foundation.Actors_Movies(MovieId,ActorId)
SELECT @Id,value
From string_split(@ActorIds,',')

INSERT INTO FOUNDATION.Genres_Movies(MovieId,GenreId)
	SELECT @Id, value
	FROM STRING_SPLIT(@GenreIds, ',');	
END

--DELETING MOVIE--
CREATE PROCEDURE Foundation.usp_Delete_Movie
@Id INT
AS
BEGIN
	DELETE FROM FOUNDATION.Actors_Movies
	WHERE MovieId=@Id

	DELETE FROM FOUNDATION.Genres_Movies
	WHERE MovieId=@Id

	DELETE FROM FOUNDATION.Movies
	WHERE Id=@Id
	
	DELETE FROM FOUNDATION.Reviews
	WHERE MovieId=@Id
END;

--DELETING PRODUCER--
CREATE PROCEDURE Foundation.usp_Delete_Producer
@Id INT
AS
BEGIN
	DELETE AM FROM FOUNDATION.Actors_Movies AM
	JOIN Foundation.Movies M
	on AM.MovieId=M.id
	Where m.ProducerId=@Id

	DELETE GM FROM FOUNDATION.Genres_Movies GM
	JOIN Foundation.Movies M
	on GM.MovieId=M.id
	Where m.ProducerId=@Id

	DELETE FROM FOUNDATION.Movies
	WHERE ProducerId=@Id

	DELETE FROM FOUNDATION.Producers
	WHERE Id=@Id
END;

EXEC Foundation.usp_Delete_Producer @Id=2

--DELETING ACTOR--
CREATE PROCEDURE Foundation.usp_Delete_Actor
@Id INT
AS
BEGIN
	DELETE FROM FOUNDATION.Actors_Movies
	WHERE ActorId=@Id
	
	DELETE FROM FOUNDATION.Actors
	WHERE Id=@Id
END;

EXEC Foundation.usp_Delete_Producer @Id=2


--DELETING GENRE
CREATE PROCEDURE usp_Delete_Genre
@Id INT
AS
BEGIN
	DELETE FROM FOUNDATION.Genres_Movies
	WHERE GenreId=@Id

	DELETE FROM FOUNDATION.Genres
	WHERE Id=@Id
END;
