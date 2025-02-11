﻿using Dapper;
using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace IMDB.Repository
{
    public class ActorRepository:BaseRepository<Actor>, IActorRepository
    {
        //private readonly ConnectionString _connectionString;

        public ActorRepository(IOptions<ConnectionString> connectionString)
        :base(connectionString.Value.IMDBDB)
        { 
           // _connectionString=connectionString.Value;
        }
        public async Task<IEnumerable<Actor>> Get()
        {
            const string query = @"SELECT [Id]
	,[Name]
	,[Bio]
	,[DOB]
	,[Sex] AS Gender
FROM [FOUNDATION].Actors";
            return await Get(query);
        }
        public async Task<Actor> Get(int id)
        {
            const string query = @"SELECT [Id]
	,[Name]
	,[Bio]
	,[DOB]
	,[Sex] As Gender
FROM [FOUNDATION].Actors
WHERE [Id] = @Id";
            return await Get(query, new {Id=id});
        }
        public async Task<int> Create(Actor actor)
        {
            var name=actor.Name;
            var gender=actor.Gender;
            var dob = actor.DOB;
            var bio = actor.Bio;
            const string query = @"
INSERT INTO FOUNDATION.Actors (
	[Name]
	,[Sex]
	,[DOB]
	,[Bio]
	)
VALUES (
	@name
	,@gender
	,@dob
	,@bio
	)

SELECT SCOPE_IDENTITY()
";

            return await Create(query,new
             {
                 Name = name,
                 Gender = gender,
                 Bio = bio,
                 DOB = dob
             });
        }
        public async Task Update(Actor actor)
        {
            var id=actor.Id;
            var name = actor.Name;
            var gender = actor.Gender;
            var dob = actor.DOB;
            var bio = actor.Bio;
            const string query = @"
UPDATE FOUNDATION.Actors
SET [Name] = @name
	,[Sex] = @gender
	,[DOB] = @dob
	,[Bio] = @bio
WHERE [Id] = @id
";
            await Update(query, new
            {
                Id = id,
                Name = name,
                Gender = gender,
                Bio = bio,
                DOB = dob
            });
        }
        public async Task Delete(int id)
        {
            const string query = @"EXEC Foundation.usp_Delete_Actor @Id = @Id";
            await Delete(query, new {Id=id});
        }
    }
}
