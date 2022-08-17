using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Hosting
{
    public interface IHandler
    {
    }

    public interface IHandler<TMessage> : IHandler where TMessage : class
    {
        void Handle(TMessage message);
    }
}
