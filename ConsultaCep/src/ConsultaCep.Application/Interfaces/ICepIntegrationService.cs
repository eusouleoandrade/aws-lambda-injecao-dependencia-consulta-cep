using ConsultaCep.Application.Dtos.Responses;
using ConsultaCep.Infrastructure.Notification.Interfaces;

namespace ConsultaCep.Application.Interfaces
{
    public interface ICepIntegrationService : INotifiable
    {
        Task<CepIntegrationServiceResponse> GetAddressAsync(string cep);
    }
}