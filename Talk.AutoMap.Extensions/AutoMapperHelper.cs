﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talk.Extensions;

namespace Talk.AutoMap.Extensions
{
    public static class AutoMapperHelper
    {       
        internal static void CreateMap(IEnumerable<Type> types, Type[] AttributeTypes)
        {
            Mapper.Initialize(c =>
            {
                foreach (var type in types)
                {
                    foreach (Type TAttribute in AttributeTypes)
                    {
                        foreach (AutoMapAttribute autoMapToAttribute in type.GetCustomAttributes(TAttribute))
                        {
                            if (autoMapToAttribute.TargetTypes.IsNullOrEmpty())
                            {
                                continue;
                            }

                            foreach (var targetType in autoMapToAttribute.TargetTypes)
                            {
                                if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                                {
                                    c.CreateMap(type, targetType);
                                }

                                if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                                {
                                    c.CreateMap(targetType, type);
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}