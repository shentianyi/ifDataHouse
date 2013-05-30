using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thrift.Transport;

namespace Brilliantech.DatahouseService.ServiceProvider
{
   public interface IConnectionProvider
    {
        TTransport GetConnection();
        void ReturnConnection(TTransport transport);
    }
}
