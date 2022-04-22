using ConsultaCep.Application.Dtos.Responses;
using ConsultaCep.Application.Interfaces;
using ConsultaCep.Infrastructure.Notification.Abstractions;

namespace ConsultaCep.Application.UseCases
{
    public class GetAddressUseCase : Notifiable, IGetAddressUseCase
    {
        private readonly ICepIntegrationService _cepIntegrationService;

        public GetAddressUseCase(ICepIntegrationService cepIntegrationService)
            => _cepIntegrationService = cepIntegrationService;

        public async Task<GetAddressUseCaseResponse> ExecuteAsync(string cep)
        {
            Validate(cep);

            if (HasErrorNotification)
                return await Task.FromResult<GetAddressUseCaseResponse>(default);

            var serviceResponse = await _cepIntegrationService.GetAddressAsync(cep);

            if (_cepIntegrationService.HasErrorNotification)
            {
                AddErrorNotification(_cepIntegrationService.ErrorNotificationResult);
                return await Task.FromResult<GetAddressUseCaseResponse>(default);
            }

            return await Task.FromResult<GetAddressUseCaseResponse>(new GetAddressUseCaseResponse(serviceResponse.Address));
        }

        private void Validate(string cep)
        {
            if (string.IsNullOrEmpty(cep) || string.IsNullOrWhiteSpace(cep))
                AddErrorNotification("Invalid zip code");
        }
    }
}