﻿using DeepClone.Builder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DeepClone
{
    public static class ObjectCloneOperator
    {

        public readonly static ConcurrentDictionary<Type, Func<object,object>> MethodCache;
        static ObjectCloneOperator()
        {
            MethodCache = new ConcurrentDictionary<Type, Func<object,object>>();
        }

        public static object Clone(object instance)
        {

            if (instance==null)
            {

                return null;

            }
            else
            {

                var type = instance.GetType();
                if (type==typeof(object))
                {

                    return new object();

                }
                else if (MethodCache.ContainsKey(type))
                {

                    return MethodCache[type](instance);

                }
                else
                {

                    var func = ObjectCloneBuilder.Create(type);
                    MethodCache[type] = func;
                    return func(instance);
                }

            }

        }

    }

}
