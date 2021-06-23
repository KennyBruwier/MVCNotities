using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StoreAccountingApp.CustomMethods
{
    public static class ObjMethods
    {
        public static TU CopyProperties<T, TU>(T source) where TU : new()
        {
            var sourceProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanWrite).ToList();
            TU dest = new TU();
            foreach (PropertyInfo sourceProp in sourceProps)
            {
                bool bContinue = true;
                PropertyInfo p = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (p != null)
                {
                    if (!(sourceProp.PropertyType.IsClass && 
                        (sourceProp.PropertyType.Assembly.FullName == typeof(T).Assembly.FullName)))
                    {
                        var sourcePropValue = sourceProp.GetValue(source, null);
                        if (sourceProp.Name.Substring(sourceProp.Name.Trim().Length - 2).ToLower() == "id")
                        {
                            if ((sourcePropValue is int @int && @int == 0) ||
                                (sourcePropValue is double @double && @double == 0))
                                bContinue = false;
                        }
                        if (bContinue)
                        {
                            if (p.CanWrite)
                            {
                                var destValue = p.GetValue(dest, null);
                                if ((p.PropertyType == sourceProp.PropertyType)&&
                                    (p.GetValue(dest,null) != sourceProp.GetValue(source,null)))
                                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
                            }
                        }
                    }
                }
            }
            return dest;
        }
    }
}
