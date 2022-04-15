using ConsultaCep.Application.Dtos.Responses;
using ConsultaCep.Application.Interfaces;
using ConsultaCep.Infrastructure.Notification.Abstractions;

namespace ConsultaCep.Infrastructure.Integration.Services
{
    public class ViaCepService : Notifiable, ICepIntegrationService
    {
        public async Task<CepIntegrationServiceResponse> GetAddressAsync(string cep)
        {
            var serviceResponse = new CepIntegrationServiceResponse(string.Empty);

            var httpClient = new HttpClient{ BaseAddress = new Uri("https://viacep.com.br/ws/")};
            
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var response = await httpClient.GetAsync($"{cep}/json/");

                if (response.IsSuccessStatusCode)
                    serviceResponse.Address  = response.Content.ReadAsStringAsync().Result;
            }
            catch (System.Exception)
            {
                // TODO: Develop global exception handling
                AddErrorNotification("Error processing ViaCepService");
            }
            finally
            {
                 httpClient.Dispose();
            }

            return await Task.FromResult<CepIntegrationServiceResponse>(serviceResponse);
        }
    }
}