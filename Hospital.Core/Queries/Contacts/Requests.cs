using Ardalis.Result;
using Hospital.Core.Models.DTO;
using MediatR;

namespace Hospital.Core.Queries.Contacts;

public sealed record GetContactRequest(Guid Id) : IRequest<Result<ContactDto>>;

public sealed record GetContactsRequest() : IRequest<Result<IEnumerable<ContactDto>>>;