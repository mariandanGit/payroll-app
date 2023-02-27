using FluentValidation;
using Proiect_TI.Models;

public class EmployeeValidator : AbstractValidator<EmployeeViewModel>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Nume).NotEmpty().WithMessage("Nume is required.");
        RuleFor(x => x.Prenume).NotEmpty().WithMessage("Prenume is required.");
        RuleFor(x => x.Functie).NotEmpty().WithMessage("Functie is required.");
        RuleFor(x => x.SalarBaza).GreaterThan(0).WithMessage("SalarBaza should be greater than 0.");
        RuleFor(x => x.Spor).GreaterThan(0).WithMessage("SalarBaza should be greater than 0.");
        RuleFor(x => x.PremiiBrute).GreaterThan(0).WithMessage("SalarBaza should be greater than 0.");
        RuleFor(x => x.Retineri).GreaterThan(0).WithMessage("SalarBaza should be greater than 0.");
    }
}