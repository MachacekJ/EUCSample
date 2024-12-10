using EUCApp.Client.Helpers;
using EUCApp.Client.ResX;
using FluentValidation;
using FluentValidation.Validators;

namespace EUCApp.Client.Modules.PatientModule.Components.BirthNumber;

public class BirthNumberValidator<T> : PropertyValidator<T, string?>
{
  public override string Name => "BirthNumberValidator";

  public override bool IsValid(ValidationContext<T> context, string? value)
  {
    var bnv = new BirthNumberHelper(value);
    if (bnv.IsValid)
      return true;

    context.MessageFormatter.AppendArgument("ErrorCode", bnv.ErrorCode);
    //context.AddFailure(ResXMain.BirthNumber_Invalid);
    return false;
  }

  protected override string GetDefaultMessageTemplate(string errorCode)
    => ResXMain.BirthNumber_Invalid;
}

public static class BirthNumberValidatorExtensions
{
  public static IRuleBuilderOptions<T, string?> HasValidBirthNumber<T>(this IRuleBuilder<T, string?> ruleBuilder)
  {
   return  ruleBuilder.SetValidator(new BirthNumberValidator<T>());
  }
}