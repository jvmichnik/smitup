﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmitUp.Domain.Core.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
    }
}
