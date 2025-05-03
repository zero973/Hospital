using Ardalis.Result;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Patients;

public sealed record AddPatientRequest(string FIO, DateOnly Birthday, string Snils) : IRequest<Result<Patient>>;

public sealed record EditPatientRequest(Guid Id, string FIO, DateOnly Birthday, string Snils) : IRequest<Result<Patient>>;

public sealed record DeletePatientRequest(Guid Id) : IRequest<Result>;