using System.ComponentModel;

namespace pBot.Model.Order.Mask
{
    public enum ArgumentOptions
    {
        [Description("To match argumentName must be equal to string")]
        Core,
        [Description("To match argumentName must match pattern")]
        Required, 
        [Description("Managed by commandDoer parameters")]
        Optional,//Everything to end of input
    }
}