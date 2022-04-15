namespace ConsultaCep.Application.Dtos.Responses
{
    public class CepIntegrationServiceResponse
    {
        public string Address { get; set; }

        public CepIntegrationServiceResponse(string address)
        {
            Address = address;
        }
    }
}