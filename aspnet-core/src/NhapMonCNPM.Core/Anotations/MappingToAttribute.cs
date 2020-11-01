using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.Anotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MappingToAttribute : Attribute
    {
        public Type MapTo { get; set; }
    }
}
