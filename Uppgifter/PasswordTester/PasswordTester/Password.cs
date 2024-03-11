using System.Text.RegularExpressions;

public class PasswordValidator
{
    public static bool ValidatePassword(string password)
    {
        // Lösenordet måste vara minst 8 tecken långt
        if (password.Length < 8)
            return false;

        // Lösenordet måste innehålla minst en stor bokstav
        if (!Regex.IsMatch(password, "[A-Z]"))
            return false;

        // Lösenordet måste innehålla minst en liten bokstav
        if (!Regex.IsMatch(password, "[a-z]"))
            return false;

        // Lösenordet måste innehålla minst en siffra
        if (!Regex.IsMatch(password, "[0-9]"))
            return false;

        // Lösenordet måste innehålla minst ett specialtecken
        if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            return false;

        // Alla valideringskrav uppfyllda
        return true;
    }
}
