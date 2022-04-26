namespace Validations;
public class FullNameAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) {
            return true;
        }

        var name = value?.ToString()?.Trim();
        return name?.Split(' ').Length >= 2;
    }
}
