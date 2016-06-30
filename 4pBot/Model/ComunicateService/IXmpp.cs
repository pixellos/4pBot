using pBot.Model.Core.Data;

namespace pBot.Model.ComunicateService
{
    public interface IXmpp
    {
        void SendIfNotNull(string message);
        void PrivateSend(string user, string message);
        string ChangeRoom(Command command);
        string ChangeRoom(string roomName);
    }
}