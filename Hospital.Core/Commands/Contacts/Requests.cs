using Ardalis.Result;
using Hospital.Core.Models.DTO;
using Hospital.Core.Models.Entities;
using MediatR;

namespace Hospital.Core.Commands.Contacts;

public sealed record AddContactRequest(Guid PatientId, Guid DiseaseId, DateOnly Date, 
        List<ResourceSpent> ResourcesSpent) 
    : IRequest<Result<ContactDto>>;

public sealed record EditContactRequest(Guid Id, Guid PatientId, Guid DiseaseId, DateOnly Date) 
    : IRequest<Result<ContactDto>>;

public sealed record DeleteContactRequest(Guid Id) : IRequest<Result>;