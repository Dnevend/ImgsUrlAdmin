using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUrl.Services.IServices
{
    public interface IRedisService
    {

        string getCode(string key);

        bool generateCode(String key, String code);

        string getString();

    }
}
