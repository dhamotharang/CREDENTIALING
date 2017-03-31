﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Exceptions.Profiles
{
    public class DuplicateCAQHUserNameException : ApplicationException
    {
        public DuplicateCAQHUserNameException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}