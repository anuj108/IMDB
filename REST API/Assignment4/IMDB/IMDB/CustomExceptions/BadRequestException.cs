﻿using System;

namespace IMDB.CustomExceptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
