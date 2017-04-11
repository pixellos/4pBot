
namespace _4PBot.Model.ComunicateService
{
    public interface IXmpp
    {
        void SendIfNotNull(string message);
        void PrivateSend(string user, string message);
        string ChangeRoom(string roomName);
        void Close();
        void Open();
    }
}