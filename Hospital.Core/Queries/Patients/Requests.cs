using Ardalis.Result;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Patients;

public sealed record GetPatientRequest(Guid Id) : IRequest<Result<Patient>>;

public sealed record GetPatientsRequest() : IRequest<Result<IEnumerable<Patient>>>;