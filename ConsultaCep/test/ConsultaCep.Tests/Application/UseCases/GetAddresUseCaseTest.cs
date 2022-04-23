using ConsultaCep.Application.Interfaces;
using ConsultaCep.Application.UseCases;
using Xunit;
using Moq;
using ConsultaCep.Application.Dtos.Responses;

namespace ConsultaCep.Presentation.Function.Tests.Application.UseCases
{
    public class GetAddresUseCaseTest
    {
        private readonly IGetAddressUseCase _getAddressUseCase;
        private readonly Mock<ICepIntegrationService> _cepIntegratioService;

        public GetAddresUseCaseTest()
        {
            _cepIntegratioService = new Mock<ICepIntegrationService>();
            _getAddressUseCase = new GetAddressUseCase(_cepIntegratioService.Object);
        }
        
        /// <summary>
        /// Verify success in ExecuteAsync
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "GetAddressUseCase - Must run successfully")]   
        public async void ShouldExecuteSucessfully()
        {
            // Arranje
            string addressResponse = @"{'cep': '53080-800',
                                    'logradouro': 'Avenida das Garças',
                                    'complemento': '(4ª Etapa)',
                                    'bairro': 'Rio Doce',
                                    'localidade': 'Olinda',
                                    'uf': 'PE',
                                    'ibge': '2609600',
                                    'gia': '',
                                    'ddd': '81',
                                    'siafi': '2491'}";

            var serviceResponse = new CepIntegrationServiceResponse(addressResponse);
            _cepIntegratioService.Setup(x => x.GetAddressAsync(It.IsAny<string>())).ReturnsAsync(serviceResponse);
            
            string useCaseRequest = "53080800";

            // Act
            var useCaseResponse = await _getAddressUseCase.ExecuteAsync(useCaseRequest);

            // Assert
            Assert.NotNull(useCaseResponse);
            Assert.False(_getAddressUseCase.HasErrorNotification);
            Assert.Empty(_getAddressUseCase.ErrorNotificationResult);
        }

        /// <summary>
        /// Check ExecuteAsync validation failure when cep is empty
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        [Theory(DisplayName = "GetCustomerUseCase - Should run with failure when cep is empty")]
        [InlineData("")]
        [InlineData(" ")]
        public async void ShouldNotExecute_WhenNotSendingCep(string cep)
        {
            // Act
            var useCaseResponse = await _getAddressUseCase.ExecuteAsync(cep);

            // Assert
            Assert.False(_getAddressUseCase.HasSuccessNotification);
            Assert.True(_getAddressUseCase.HasErrorNotification);
            Assert.NotEmpty(_getAddressUseCase.ErrorNotificationResult);
            Assert.Empty(_getAddressUseCase.SuccessNotificationResult);
            Assert.Null(useCaseResponse);
            Assert.Contains(_getAddressUseCase.ErrorNotificationResult, item => item.Message == "Invalid zip code");
        }
    }
}