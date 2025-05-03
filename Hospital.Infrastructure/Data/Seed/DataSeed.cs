using System.Diagnostics.CodeAnalysis;
using Hospital.Core.Enums;
using Hospital.Core.Helpers;
using Hospital.Core.Models.Entities;

namespace Hospital.Infrastructure.Data.Seed;

internal class DataSeed
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public void Seed(AppDbContext context)
    {
        context.Users.AddRange(new User("admin", Encryptor.EncryptString("admin"), UserRoles.Admin), 
            new User("doctor", Encryptor.EncryptString("doctor"), UserRoles.Doctor));
        
        context.Diseases.AddRange(new Disease("Болезнь 1", PeriodTypes.Warm), 
            new Disease("Болезнь 2", PeriodTypes.Warm));

        context.Patients.AddRange(new Patient("Каримов А.Л.", new DateOnly(1990, 4, 23), "12345678900"), 
            new Patient("Родионов В.В.", new DateOnly(2000, 1, 15), "92345678954"), 
            new Patient("Карпунин Д.Г.", new DateOnly(1988, 10, 2), "97345678915"));
    }
}