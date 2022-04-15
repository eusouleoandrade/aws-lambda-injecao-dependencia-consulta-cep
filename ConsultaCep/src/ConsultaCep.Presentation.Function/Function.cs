using Amazon.Lambda.Core;
using ConsultaCep.Application.Interfaces;
using ConsultaCep.Application.UseCases;
using ConsultaCep.Infrastructure.Integration.Services;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ConsultaCep.Presentation.Function;

public class Function
{
    private ServiceCollection _serviceCollection;
    private IGetAddressUseCase _getAddressUseCase;
    private readonly ServiceProvider _serviceProvider;

    public Function()
    {
        ConfigureServices();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
        _getAddressUseCase = _serviceProvider.GetService<IGetAddressUseCase>();
    }

    /// <summary>
    /// Returns the address of the zip code
    /// </summary>
    /// <param name="cep"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(string cep, ILambdaContext context)
    {
        var response = await _getAddressUseCase.ExecuteAsync(cep);

        if (_getAddressUseCase.HasErrorNotification)
            return string.Join(',', _getAddressUseCase.ErrorNotificationResult.Select(s => s.Message));

        return await Task.FromResult<string>(response.Address);
    }

    private void ConfigureServices()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddTransient<IGetAddressUseCase, GetAddressUseCase>();
        _serviceCollection.AddTransient<ICepIntegrationService, ViaCepService>();
    }
}
