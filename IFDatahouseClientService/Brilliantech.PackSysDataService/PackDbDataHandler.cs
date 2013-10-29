﻿using System;
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
        public bool WritePackItemViewToFileBy(DateTime startTime, DateTime endTime, string file)
        {
            try
            {
                using (IUnitOfWork unit = new PackSysDataClassesDataContext(Conf.Connstr))
                {
                    PackItemViewRepository rep = new PackItemViewRepository(unit);
                    List<PackItemView> items = rep.GetByTime(startTime, endTime);
                    if (items.Count == 0)
                        return false;
                    LogUtil.Logger.Error(items.Count);
                    using (FileStream fs = File.Open(file, FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            foreach (PackItemView item in items)
                            {
                                writer.WriteLine(item.partNr);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
                return false;
            }
        }
    }
}
