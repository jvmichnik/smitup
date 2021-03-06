﻿using SmitUp.Domain.Core.Commands;
using SmitUp.Customers.Domain.Enum;
using System;

namespace SmitUp.Customers.Domain.Commands.CustomerCommands
{
    public abstract class CustomerCommand : Command
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public EMaritalStatus MaritalStatus { get; set; }
    }
}
