using System;
using System.Collections.Generic;
using System.Text;

namespace Community.Utils.Interfaces
{
    public interface IPlatformSpecificSettings
    {
        string UserIdentifier { get; set; }
    }
}
