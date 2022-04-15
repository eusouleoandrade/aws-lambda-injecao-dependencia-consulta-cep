namespace ConsultaCep.Application.Dtos.Responses
{
    public class GetAddressUseCaseResponse
    {
        public string Address { get; set; }

        public GetAddressUseCaseResponse(string address)
        {
            Address = address;
        }
    }
}