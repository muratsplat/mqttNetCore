using System.Threading.Tasks;

namespace QttWebHook.QttWebHook
{
    public interface IWebHook
    {
        Task Auth(string user, string pass);
        Task Publish(byte[] payload);
        Task Ping();
        Task Connect();
        Task DisConnect();
    }
    
}