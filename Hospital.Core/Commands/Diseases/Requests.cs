using Ardalis.Result;
using Hospital.Core.Enums;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Diseases;

public sealed record AddDiseaseRequest(string Name, PeriodTypes PeriodType) : IRequest<Result<Disease>>;

public sealed record EditDiseaseRequest(Guid Id, string Name, PeriodTypes PeriodType) : IRequest<Result<Disease>>;

public sealed record DeleteDiseaseRequest(Guid Id) : IRequest<Result>;