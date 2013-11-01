using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.PackSysDataService.Repository;
using Brilliantech.BaseClassLib.Util;
using System.IO;

namespace Brilliantech.PackSysDataService
{
    public class PackDbDataHandler
    {
        public bool WritePackItemViewToFileBy(string startTime, string endTime, string file)
        {
            try
            {
                using (IUnitOfWork unit = new PackSysDataClassesDataContext(Conf.Connstr))
                {
                    //LogUtil.Logger.Info("----------------start time:");
                    //LogUtil.Logger.Info(startTime);
                    //LogUtil.Logger.Info("----------------end time:");
                    //LogUtil.Logger.Info(endTime);
                    PackItemViewRepository rep = new PackItemViewRepository(unit);
                    List<PackItemView> items = rep.GetByTime(startTime, endTime);
                    //LogUtil.Logger.Info(items.Count);
                    if (items.Count == 0)
                        return false;
                    using (FileStream fs = File.Open(file, FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            foreach (PackItemView item in items)
                            {
                                writer.WriteLine(string.Format("{0}{4}{1}{4}{2}{4}{3}", item.partNr, item.packageID, item.projectID,TimeUtil.GetMilliseconds( item.packagingTime.ToUniversalTime()), Conf.DataSpliter));
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                LogUtil.Logger.Info(e.Message);
                return false;
            }
        }
    }
}
