using pBot.Model.Commands;
using pBot.Model.Core;

namespace pBot.Model.ComunicateService
{
    public interface IXmpp
    {
        void SendIfNotNull(string message);
        void PrivateSend(string user, string message);
        string ChangeRoom(Command command);
    }
}