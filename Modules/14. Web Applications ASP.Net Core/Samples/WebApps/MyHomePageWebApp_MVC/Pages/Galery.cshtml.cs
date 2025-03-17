using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;
using System.Net.Mime;

namespace MyHomePageWebApp.Pages
{
    public class GaleryModel : PageModel
    {
        private readonly IFileProvider galeryProvider;
        public IEnumerable<IFileInfo> FileInfos { get; private set; }

        public GaleryModel(
            [FromKeyedServices("galeryProvider")]
            IFileProvider galeryProvider)
        {
            this.galeryProvider = galeryProvider;
            FileInfos = Enumerable.Empty<IFileInfo>();
        }

        public IActionResult OnGet(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                FileInfos = galeryProvider.GetDirectoryContents("");
                return Page();
            }

            var fileInfo = galeryProvider.GetFileInfo(fileName);
            if (fileInfo.Exists)
                return File(fileInfo.CreateReadStream(), MediaTypeNames.Image.Jpeg);
            return NotFound();
        }
    }

}
