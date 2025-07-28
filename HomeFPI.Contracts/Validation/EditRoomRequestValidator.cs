using FluentValidation;
using HomeAPI.Contracts.Models.Rooms;

namespace HomeAPI.Contracts.Validation;

public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
{
    public EditRoomRequestValidator()
    {
        RuleFor(x => x.NewName).NotEmpty();
        RuleFor(x => x.NewVoltage).NotEmpty();
        //RuleFor(x => x.NewGasConnected).NotEmpty();
    }
}