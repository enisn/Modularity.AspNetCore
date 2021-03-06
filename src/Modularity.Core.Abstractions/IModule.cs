﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.Core.Abstractions
{
    public interface IModule
    {
        string Name { get; }
        string AssemblyName { get; }
        Assembly Assembly { get; }
        IList<Exception> Exceptions { get; }
        IList<IEntryObject> EntryObjects { get; }
    }
}
