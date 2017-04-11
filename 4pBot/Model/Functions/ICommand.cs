using CoreBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pBot.Model.Functions
{
    public interface ICommand
    {
        Actions AvailableActions { get; } 
    }
}
