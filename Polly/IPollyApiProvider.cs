using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollyClient
{
    public interface IPollyApiProvider
    {
        [Get("/retry")]
        Task<List<string>> GetRetry();

        [Get("/pollyservice/Timeout")]
        Task<List<string>> GetTimeout();

        [Get("/pollyservice/Error")]
        Task<List<string>> GetError();

        [Get("/longaction")]
        Task<List<string>> GetLongAction();

        [Get("/cache")]
        Task<List<string>> GetCache(string start);
    }
}
