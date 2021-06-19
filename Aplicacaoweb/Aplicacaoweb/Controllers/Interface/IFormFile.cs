using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacaoweb.Controllers.Interface
{
    public interface IFormFile
    {
        string ContentType { get; }

        string ContentDisposition { get; }



        long Length { get; }

        string Name { get; }

        string FileName { get; }

        Stream OpenReadStream();

        void CopyTo(Stream target);

        Task CopyToAsync(Stream target);



    }
}
