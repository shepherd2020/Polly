using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollyClient
{
    public interface IPollyApiProvider
    {
        [Get("/pollyservice/Timeout")]
        Task<List<string>> GetTimeout();

        [Get("/pollyservice/Error")]
        Task<List<string>> GetError();

        [Get("/pollyservice/Working")]
        Task<List<string>> GetWorking();

        [Get("/pollyservice/WorkingDelayed")]
        Task<List<string>> GetWorkingDelayed();

        [Get("/pollyservice/Cache")]
        Task<List<string>> GetCache(string start);
    }
}
