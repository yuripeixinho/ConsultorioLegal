using CL.Core.Shared.ModelViews;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Manager.Validator;

public class AlteraClienteValidator : AbstractValidator<AlteraCliente>
{
    public AlteraClienteValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().GreaterThan(0);
        Include(new NovoClienteValidator()); // ao achamar o construtor do NovoClienteValidator, todas as validações valerão para o "AlteraClienteValidator"
    }
}
