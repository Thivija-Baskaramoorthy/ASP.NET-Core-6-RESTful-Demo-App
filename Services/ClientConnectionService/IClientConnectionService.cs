namespace WebAppDemo.Services.ClientConnectionService
{
    public interface IClientConnectionService
    {
        public Task<bool> AddOrUpdateConnectionId(string clientId, string connectionId);
        public Task RemoveConnection(string clientId);
        public Task<string> GetConnectionIdByClientId(string clientId);
    }
}
