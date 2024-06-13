﻿using API.ViewModels.ShortViewModels;
using FluentValidation;

namespace API.Validators;

public class ShortClientViewModelValidator : AbstractValidator<ShortClientViewModel>
{
    public ShortClientViewModelValidator()
    {
        RuleFor(c => c.FirstName)
            .MinimumLength(2)
            .MaximumLength(20);

        RuleFor(c => c.LastName)
            .MinimumLength(2)
            .MaximumLength(20);

        RuleFor(c => c.Email)
            .EmailAddress();

        RuleFor(c => c.PhoneNumber)
            .Matches(@"^(\+\d{1,3}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")
            .When(c => c.PhoneNumber is not null).WithMessage("Phone number must match pattern Example: 1234567890 +9211234567890");
    }
}
