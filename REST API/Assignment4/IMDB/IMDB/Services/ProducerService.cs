﻿using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Services
{
    public class ProducerService:IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        
        public ProducerService(IProducerRepository producerRepository)
        { 
        _producerRepository =producerRepository;
        }

        public async Task<int> Create(ProducerRequest producerRequest)
        {
            if (string.IsNullOrWhiteSpace(producerRequest.Name)) throw new BadRequestException("INVALID PRODUCER NAME");
            if (string.IsNullOrWhiteSpace(producerRequest.Bio)) throw new BadRequestException("INVALID BIO DATA");
            if (producerRequest.DOB.Year < 1800 || producerRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("INVALID DATE OF BIRTH");
            if (!producerRequest.Gender.Equals("Male") && !producerRequest.Gender.Equals("Female")) throw new BadRequestException("INVALID GENDER");
            
            return await _producerRepository.Create(new Producer
            {
               
                Name=producerRequest.Name,
                Bio=producerRequest.Bio,
                DOB=producerRequest.DOB,
                Gender=producerRequest.Gender,
            });
        }

        public async Task<IEnumerable<ProducerResponse>> Get()
        {
            var producerData=await _producerRepository.Get();
            if (!producerData.Any()) throw new NotFoundException("NO PRODUCER FOUND");
            return producerData.Select(x=>new ProducerResponse { Id=x.Id, Name=x.Name, Bio=x.Bio,Gender=x.Gender,DOB=x.DOB}).ToList();
        }

        public async Task<ProducerResponse> GetById(int id)
        {
            var producerData = await _producerRepository.GetById(id);
            if (producerData == null) throw new NotFoundException("NO PRODUCER FOUND");
            return new ProducerResponse
            {
                Id = producerData.Id,
                Name=producerData.Name,
                Bio=producerData.Bio,
                DOB=producerData.DOB,
                Gender=producerData.Gender
            };
        }

        public async Task Update(int id,ProducerRequest producerRequest)
        {
            if (await _producerRepository.GetById(id) == null) throw new NotFoundException("NO PRODUCER FOUND");
            if (string.IsNullOrWhiteSpace(producerRequest.Name)) throw new BadRequestException("INVALID PRODUCER NAME");
            if (string.IsNullOrWhiteSpace(producerRequest.Bio)) throw new BadRequestException("INVALID BIO DATA");
            if (producerRequest.DOB.Year < 1800 || producerRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("INVALID DATE OF BIRTH");
            if (!producerRequest.Gender.Equals("Male") && !producerRequest.Gender.Equals("Female")) throw new BadRequestException("INVALID GENDER");
            await _producerRepository.Update(
                new Producer { 
                    Id=id,
                Name=producerRequest.Name,
                Bio=producerRequest.Bio,
                DOB=producerRequest.DOB,
                Gender=producerRequest.Gender,
                });
        }
        public async Task Delete(int id)
        {
            if (await _producerRepository.GetById(id) == null) throw new NotFoundException("NO PRODUCER FOUND");
            await _producerRepository.Delete(id);
        }

     
    }
}
