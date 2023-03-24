using INFORCE.Models;
using Microsoft.AspNetCore.Mvc;

namespace INFORCE.Services
{
    public interface IUrlService
    {
        List<URLs> GetUrls();
        void CreateUrl([FromBody] URLs value);
        URLs UpdateUrl([FromBody] URLs value);
        Task<ActionResult<List<URLs>>> DeleteUrl(Guid id);
        List<URLs>? SearchByCodedUrl(string Name);
    }
}
