
/* Create a new database called IMDB. */
CREATE DATABASE IMDB;
use IMDB;

/* Create a new schema called Foundation. */
CREATE SCHEMA FOUNDATION

/* Use the schema to create the tables in the design created in Assignment 1. */
CREATE TABLE [Foundation].[Actors] (
  actor_id smallint,
  name varchar(20),
  sex char(1),
  date_of_birth date,
  bio text,
  PRIMARY KEY (actor_id)
);

CREATE TABLE [Foundation].[Producers] (
  producer_id smallint,
  name text,
  sex char(1),
  date_of_birth date,
  bio text,
  PRIMARY KEY (producer_id)
);


CREATE TABLE [Foundation].[Movies] (
  movie_id smallint,
  title varchar(100),
  year_of_release date,
  plot text,
  poster varbinary(max),
  producer_id smallint,
  PRIMARY KEY ([movie_id]),
  CONSTRAINT [FK_Movies.producer_id]
    FOREIGN KEY ([producer_id])
      REFERENCES [Foundation].[Producers]([producer_id])
);


CREATE TABLE [Foundation].[Actors_Movies] (
  actor_id smallint,
  movie_id smallint,
  CONSTRAINT [FK_Actors_Movies.actor_id]
    FOREIGN KEY ([actor_id])
      REFERENCES [Foundation].[Actors]([actor_id]),
  CONSTRAINT [FK_Actors_Movies.movie_id]
    FOREIGN KEY ([movie_id])
      REFERENCES [Foundation].[Movies]([movie_id])
);
/*Fill in the required data making sure of the following conditions:
It should have a movie with more than 2 actors in it.
There must be two actors who have worked together in two movies or more.
There must be a producer who has produced more than 3 movies.
*/
/*ADDING MOVIES*/
insert into Foundation.Movies(movie_id,title,year_of_release,plot,poster,producer_id)
values(1,'The Shawshank Redemption','1994-08-14','Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.',(SELECT bulkcolumn FROM OPENROWSET(BULK 'C:\Users\Anuj\Downloads\OIP.jpg', SINGLE_BLOB) AS poster),1),
(2,'The Avengers','2012-08-14','This is the first Avengers film where Chris Hemsworth plays Thor, the god of thunder, and Chris Evans portrays Steve Rogers/Captain America.',(SELECT bulkcolumn FROM OPENROWSET(BULK 'C:\Users\Anuj\Downloads\OIP.jpg', SINGLE_BLOB) AS poster),2),
(3,'Avengers: Age of Ultron','2015-06-14','The Avengers face a new threat from the sentient robot Ultron, created by Tony Stark (Iron Man), and they must once again come together to save the world.',(SELECT bulkcolumn FROM OPENROWSET(BULK 'C:\Users\Anuj\Downloads\OIP.jpg', SINGLE_BLOB) AS poster),2),
(4,'Guardians of the Galaxy','2014-06-14','"Guardians of the Galaxy" (2014) is a Marvel Cinematic Universe film produced by Kevin Feige. The movie follows a group of intergalactic misfits including Peter Quill, Gamora, Drax the Destroyer, Rocket Raccoon, and Groot, who form an unlikely team to protect the galaxy from the villainous Ronan the Accuser.',(SELECT bulkcolumn FROM OPENROWSET(BULK 'C:\Users\Anuj\Downloads\OIP.jpg', SINGLE_BLOB) AS poster),2);
select * from Foundation.Movies;

/*ADDING PRODUCERS*/
insert into Foundation.Producers(producer_id,name,sex,date_of_birth,bio)
values(1,'Niki Marvin','F','1990-01-01',' Niki Marvin is a film producer known for her work on "The Shawshank Redemption" (1994), which is considered one of the greatest films of all time. She has been involved in other film productions as well, but "The Shawshank Redemption" remains one of her most notable works.')
insert into Foundation.Producers(producer_id,name,sex,date_of_birth,bio)
values(2,'Kevin Feige','M','1973-06-02',' He is an American film producer and the president of Marvel Studios. Feige has been a key figure in the development and success of the Marvel Cinematic Universe, overseeing the production of numerous blockbuster films based on Marvel Comics characters.' )
select * from Foundation.Producers;

/*ADDING ACTORS*/
insert into Foundation.Actors(actor_id,name,sex,date_of_birth,bio)
values(2,'Morgan Freeman','M','1937-06-01','Morgan Freeman is an American actor and film narrator known for his distinctive voice and authoritative presence on screen. In "The Shawshank Redemption" (1994), Freeman portrayed Ellis Boyd  Redding, a role that earned him widespread acclaim and nominations for various awards.'),
(3,'Bob Gunton','M','1945-11-15',' Bob Gunton is an American actor known for his versatile performances in film, television, and theater. In "The Shawshank Redemption" (1994), Gunton portrayed Warden Samuel Norton, the antagonist of the film'),
(4,'Chris Hemsworth','M','1983-08-11',' Chris Hemsworth, born on August 11, 1983, in Melbourne, Australia, is best known for his portrayal of Thor in the Marvel Cinematic Universe. He began his career in the Australian soap opera "Home and Away" before moving to Hollywood. '),
(5,'Chris Evans','M','1981-06-13',' Chris Evans, born on June 13, 1981, in Boston, Massachusetts, USA, is an American actor best known for his role as Steve Rogers/Captain America in the Marvel Cinematic Universe.'),
(6,'Leonardo DiCaprio','M','1981-11-11',' DiCaprio gained early recognition for his role in the television series "Growing Pains" during the late 1980s and early 1990s. He transitioned to film with notable performances in Whats Eating Gilbert Grape (1993) and The Basketball Diaries (1995).');
select * from Foundation.Actors;

insert into Foundation.Actors_Movies(actor_id,movie_id)
values(1,1)
insert into Foundation.Actors_Movies(actor_id,movie_id)
values(2,1)
insert into Foundation.Actors_Movies(actor_id,movie_id)
values(4,2),
(4,3),
(5,2),
(5,3)
select * from Foundation.Actors_Movies


/* Add two new columns called CreatedAt and UpdatedAt using the alter Table command. */
alter table Foundation.Movies
add CreatedAt Datetime, UpdatedAt Datetime;

/* Create a default constraint for CreatedAt to store the current Date. */
ALTER TABLE Foundation.Movies
ADD CONSTRAINT DF_CreatedAt DEFAULT GETDATE() FOR CreatedAt;

/* Alter the table to add a language(varchar) and a profit(Int) column to the movies table. */
alter table Foundation.Movies
add Language varchar(50),Price int 

alter table Foundation.Movies
alter column Language varchar(20)
