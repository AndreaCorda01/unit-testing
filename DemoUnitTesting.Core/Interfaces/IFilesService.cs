using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUnitTesting.Core.Interfaces
{
    public interface IFilesService
    {
        void Save(string path, byte[] content);
    }
}
