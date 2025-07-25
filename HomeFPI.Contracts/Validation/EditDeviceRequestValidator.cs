using FluentValidation;
using HomeAPI.Contracts.Models.Devices;

namespace HomeAPI.Contracts.Validation;

public class EditDeviceRequestValidator : AbstractValidator<EditDeviceRrequest>
{
    public EditDeviceRequestValidator()
    {
        RuleFor(x => x.NewName).NotEmpty();
        RuleFor(x => x.NewRoom).NotEmpty().Must(BeSupported).WithMessage($"Выберите одну из комнат: {string.Join(", ", Values.ValidRooms)}");
    }

    private bool BeSupported(string location)
    {
        return Values.ValidRooms.Any(e => e == location);
    }
}