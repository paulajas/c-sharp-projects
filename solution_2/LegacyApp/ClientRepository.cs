namespace LegacyApp
{
    public class ClientRepository : IUserRepository
    {
        public ClientRepository()
        {
        }

        public Client GetById(int clientId)
        {
            //Fetching the data...
            return new Client
            {
                ClientId = clientId,
                Name = clientId switch
                {
                    1 => "VeryImportantClient",
                    2 => "ImportantClient",
                    _ => null
                }
            };
        }
    }
}