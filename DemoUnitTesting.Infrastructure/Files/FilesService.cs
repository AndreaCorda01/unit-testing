using DemoUnitTesting.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUnitTesting.Services
{
    public class FilesService : IFilesService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FilesService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void Save(string fileName, byte[] content)
        {
            var serverPath = Path.Combine(
                _hostingEnvironment.WebRootPath,
                "posters",
                fileName);

            File.WriteAllBytes(serverPath, content);
        }
    }
}
