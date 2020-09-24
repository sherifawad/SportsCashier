﻿using EFCoreData.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EFCoreData.Specification
{
    public interface ISpecification<T> 
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
