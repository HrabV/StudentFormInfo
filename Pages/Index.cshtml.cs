using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Studentforms.Model;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Studentforms.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        [BindProperty]
        public User Student { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (Student != null)
            {
                var json = JsonSerializer.Serialize<User>(Student, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    PropertyNameCaseInsensitive = true,
                });
                var stream = new MemoryStream(await GetStudentInfo(json));
                Guid key = Guid.NewGuid();
                BlobContainerClient client = new BlobContainerClient(_configuration.GetConnectionString("Azurekey"), "azurecont");
                await client.UploadBlobAsync($"Studentinfo-{key}.json", stream);
            }
            return RedirectToPage("Index");
        }

        private async Task<byte[]> GetStudentInfo(string data)
        {
            if (data != "")
            {
                using (var ms = new MemoryStream())
                using (var sw = new StreamWriter(ms))
                {
                    await sw.WriteAsync(data);
                    await sw.FlushAsync();
                    return ms.ToArray();
                }
            }
            return null;
        }
        public void OnGet()
        {

        }

    }


}
