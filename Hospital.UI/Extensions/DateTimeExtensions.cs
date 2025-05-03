namespace Hospital.UI.Extensions;

internal static class DateTimeExtensions
{

    public static DateOnly AsDateOnly(this DateTime date)
        => new DateOnly(date.Year, date.Month, date.Day);

    public static DateTime AsDateTime(this DateOnly date)
        => new DateTime(date.Year, date.Month, date.Day);

}