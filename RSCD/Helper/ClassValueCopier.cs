using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace RSCD.Helper
{
    public class ClassValueCopier
    {
        public T ConvertAndCopy<T,Source>(Source source) where T : class, new () where Source : class
        {

            PropertyInfo[] sourceClassInfo = source.GetType().GetProperties();

            T transferClass = new T();
            Type transferClassType = transferClass.GetType();

            foreach (PropertyInfo property in transferClassType.GetProperties())
            {
                try
                {
                    property.SetValue(transferClass, sourceClassInfo.First(r => r.Name == property.Name).GetValue(source, null),null);
                }
                catch
                {
                    continue;
                }
            }

            return transferClass;
        }

        public T ConvertAndCopy<T, Source>(Source source,T destination) where T : class, new() where Source : class
        {

            PropertyInfo[] sourceClassInfo = source.GetType().GetProperties();

            T transferClass = destination;
            Type transferClassType = transferClass.GetType();

            foreach (PropertyInfo property in transferClassType.GetProperties())
            {
                try
                {
                    var value = sourceClassInfo.First(r => r.Name == property.Name).GetValue(source, null);

                    if(value != null)
                    {
                        property.SetValue(transferClass, value, null);
                    }
                }
                catch
                {
                    continue;
                }
            }

            return transferClass;
        }
    }
}
