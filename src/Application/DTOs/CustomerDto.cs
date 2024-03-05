namespace Application.DTOs;

public record CustomerDto(Guid Id, NameDto Name, DniDto Dni, AddressDto Address, ContactInfoDto ContactInfo);

