using System.Text.RegularExpressions;
using BarberBoss.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace BarberBoss.Application.UseCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ErrorMessageKey = "ErrorMessage";
    public override string Name => "PasswordValidator";
    
    /// <summary>
    /// Check if a password is valid
    /// </summary>
    /// <param name="context">Context of the validation</param>
    /// <param name="password">The password</param>
    /// <returns>bool</returns>
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.PASSWORD_TOO_SHORT);
            return false;
        }

        if (password.Length > 100)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.PASSWORD_TOO_LONG);
            return false;
        }
        
        if (UpperCaseRegex().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }

        if (LowerCaseRegex().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }

        if (NumberRegex().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }

        if (SpecialCharacterRegex().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }

        return true;
    }
    
    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseRegex();
    
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowerCaseRegex();
    
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex NumberRegex();
    
    [GeneratedRegex(@"[\!\@\#\$\%\&\*\(\)\.\,\<\>\;\:\/\?\|]+")]
    private static partial Regex SpecialCharacterRegex();
}