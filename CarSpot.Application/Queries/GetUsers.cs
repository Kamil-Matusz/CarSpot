﻿using CarSpot.Application.Abstractions;
using CarSpot.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSpot.Application.Queries
{
    public class GetUsers : IQuery<IEnumerable<UserDTO>>
    {

    }
}
