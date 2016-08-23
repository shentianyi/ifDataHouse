using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Brilliantech.Qmail.Framework.Util
{
    public class ParameterUtil
    {
        public static List<List<string>> GetParameterFromFile(string file)
        {
            List<List<string>> parameters = new List<List<string>>();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Parameter",file);
            if (File.Exists(filePath))
            {
                string line;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        List<string> parameter = new List<string>();
                        foreach (string p in line.Split(';'))
                        {
                            parameter.Add(p);
                        }
                        parameters.Add(parameter);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            return parameters;
        }
    }
}
