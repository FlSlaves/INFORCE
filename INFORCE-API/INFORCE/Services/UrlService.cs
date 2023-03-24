using INFORCE.Models.Data;
using INFORCE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace INFORCE.Services
{
    public class UrlService : IUrlService
    {
        private readonly AppDbContext appDb;
        public UrlService(AppDbContext appDb)
        {
            this.appDb = appDb;
        }


        public List<URLs> GetUrls()
        {
            return  appDb.Urls.ToList();
        }

        private string Shorter(string url)
        {
            //cheking if input string is url
            if (!Uri.TryCreate(url, UriKind.Absolute, out var inputUrl))
                return "Invalid url has been provided";
            var rand = new Random();
            const string chars = "ABCDEFGHIJKLMOPQRSTYVWXYZ1234567890";
            //creating shorturl using random chars
            var randStr = new string(Enumerable.Repeat(chars, 9)
            .Select(s => s[rand.Next(s.Length)]).ToArray());
            return randStr;

        }

        public void CreateUrl([FromBody] URLs value)
        {
            var model = new URLs
            {
                Id = new Guid(),
                UrlName = value.UrlName,
                Url = value.Url,
                ShortUrl = Shorter(value.Url),
                CodedUrl = value.CodedUrl,
                CreatedBy = value.CreatedBy,
                CreatedOn = DateTime.Now
            };
            model.CodedUrl = "https://localhost:7161/" + model.ShortUrl;
            appDb.Urls.Add(model);
            appDb.SaveChanges();
        }

        public URLs UpdateUrl([FromBody] URLs value)
        {
                var update = appDb.Urls.FirstOrDefault(e => e.Id == value.Id);
                update.UrlName = value.UrlName;
                update.Url = value.Url;
                update.ShortUrl = Shorter(value.Url);
                update.CodedUrl = "https://localhost:7161/" + update.ShortUrl;
                appDb.SaveChanges();
                return update;
        }

        public async Task<ActionResult<List<URLs>>> DeleteUrl(Guid id)
        {
            var u = await appDb.Urls.FindAsync(id);         
            appDb.Urls.Remove(u);
            await appDb.SaveChangesAsync();
            return await appDb.Urls.ToListAsync();
        }
        public List<URLs>? SearchByCodedUrl(string Name)
        {
            try
            {
                return appDb.Urls.Where(x => x.UrlName.Contains(Name)).ToList();
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
