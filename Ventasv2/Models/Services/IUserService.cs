using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ventasv2.Models.Request;
using Ventasv2.Models.Response;
using Ventasv2.Tools;

namespace Ventasv2.Models.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
