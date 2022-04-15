using ConsultaCep.Application.Dtos.Responses;

namespace ConsultaCep.Application.Interfaces
{
    public interface IGetAddressUseCase : IUseCase<string, GetAddressUseCaseResponse?>
    {
    }
}