﻿namespace IMDB.Domain.Request
{
    public class ReviewRequest
    {
        public string Message { get; set; }

        public int MovieId { get; set; }
    }
}
