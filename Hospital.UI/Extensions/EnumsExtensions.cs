using Hospital.Core.Enums;

namespace Hospital.UI.Extensions;

internal static class EnumsExtensions
{

    public static string AsString(this UserRoles role)
    {
        switch (role)
        {
            case UserRoles.Admin:
                return "Администратор";
            case UserRoles.Doctor:
                return "Врач";
            default:
                throw new NotImplementedException();
        }
    }

    public static string AsString(this PeriodTypes type)
    {
        switch (type)
        {
            case PeriodTypes.Warm:
                return "Тёплый период";
            case PeriodTypes.Cold:
                return "Холодный период";
            case PeriodTypes.Transition:
                return "Переходный период";
            default:
                throw new NotImplementedException();
        }
    }

}