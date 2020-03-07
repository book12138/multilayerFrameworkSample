using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace MultilayerFrameworkSample.Model.Base
{
    public class BaseValidate<T> : AbstractValidator<T> where T : Entity
    {
    }
}
