using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ConsultaCep.Presentation.Function;

public class Function
{
    
    /// <summary>
    /// Returns the address of the zip code
    /// </summary>
    /// <param name="cep"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(string cep, ILambdaContext context)
    {
        return cep.ToUpper();
    }
}
