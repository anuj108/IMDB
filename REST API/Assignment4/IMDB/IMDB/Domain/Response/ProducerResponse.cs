﻿using System;

namespace IMDB.Domain.Response
{
    public class ProducerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DOB { get; set; }

        public string Gender { get; set; }
    }
}
