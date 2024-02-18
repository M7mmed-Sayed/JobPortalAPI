namespace JobPortal.Utility;

public class ErrorsList
{
    public static Error CannotFindUser = new Error
        { Code = "CannotFindUser", Description = "user is not registered in the system" };
    public static Error IncorrectEmailOrPassword = new Error
        { Code = "IncorrectEmailOrPassword", Description = "can't find a user with this email and password" };

}