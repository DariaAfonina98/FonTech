namespace FonTech2.Domain.Enum;

public enum ErrorCodes
{
    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExists=2,
    
    InternalServerError = 10,
    UserNotFound =11,
    UserAlreadyExists = 12,
    UserUnauthorizedAccess = 13,
    
    PasswordNotEqualsPasswordConfirm = 21,
    PasswordWrong=22,
    
    RoleAlreadyExists = 31
    
    
}