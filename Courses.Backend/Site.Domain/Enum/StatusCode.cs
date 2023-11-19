using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.Enum
{
    public enum StatusCode
    {
        UserAlreadyExists = 1,
        UserNotFound = 2,

        Ok = 200,
        InternalServerError = 500
    }
}
