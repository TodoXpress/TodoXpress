namespace TodoXpress.Domain.Administration.Identity;

public record struct Permission(Guid Id, Ressource Ressource, Scope Scope);