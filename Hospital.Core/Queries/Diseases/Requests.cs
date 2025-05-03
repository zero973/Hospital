using Ardalis.Result;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Queries.Diseases;

public sealed record GetDiseaseRequest(Guid Id) : IRequest<Result<Disease>>;

public sealed record GetDiseasesRequest() : IRequest<Result<IEnumerable<Disease>>>;