using ConsultaCep.Application.Dtos.Responses;
using ConsultaCep.Infrastructure.Notification.Interfaces;

namespace ConsultaCep.Application.Interfaces
{
    public interface IGetAddressUseCase : INotifiable, IUseCase<string, GetAddressUseCaseResponse>
    {
    }
}