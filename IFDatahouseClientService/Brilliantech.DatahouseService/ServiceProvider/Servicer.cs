using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thrift.Transport;
using Thrift.Protocol;
using Brilliantech.BaseClassLib.Util; 

namespace Brilliantech.DatahouseService.ServiceProvider
{
    public class Servicer : IDisposable
    {
        ConnectionProvider pool;
        TTransport transport;
        Datahouse.Client client;
        bool disposed;
        public Servicer()
        {
            try
            {
                disposed = false;
                pool = new ConnectionProvider();
                transport = pool.GetConnection();
                TProtocol protocol = new TBinaryProtocol(transport);
                client = new Datahouse.Client(protocol);
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
            }
        }

        /// <summary>
        /// api for package system
        /// </summary>
        /// <param name="dataMap"></param>
        public void AddProductPack(Dictionary<string, string> dataMap)
        {
            try
            {
                client.addProductPack(Conf.TServiceAccessKey, dataMap);
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
            }
        }

        /// <summary>
        /// api for testing system
        /// </summary>
        /// <param name="dataMap"></param>
        public void AddProductInspect(Dictionary<string, string> dataMap)
        {
            try
            {
                client.addProductInspect(Conf.TServiceAccessKey, dataMap);
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
            }
        }

        public void Log(string message)
        {
            LogUtil.Logger.Error(message);
        }

        ~Servicer()
        {
            Dispose(true);
        }
        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transport != null && transport.IsOpen)
                    {
                        transport.Close();
                        transport.Dispose();
                    }
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
