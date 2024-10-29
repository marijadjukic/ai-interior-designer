using AIInteriorDesignerWebApp.Enums;
using AIInteriorDesignerWebApp.Extensions;
using AIInteriorDesignerWebApp.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AIInteriorDesignerWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IWebHostEnvironment _environment;

        private OpenAI.OpenAIClient _openAIClient;

        public DesignForm _form;

        [BindProperty] public DesignForm? Current { get; set; }

        public List<SelectListItem> RoomTypes { get; } = SelectListExtension.Create<RoomType>();

        public List<SelectListItem> DesingStyles { get; } = SelectListExtension.Create<Style>();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            _form = new DesignForm() { Id = Guid.NewGuid().ToString() };

            var aiEndpoint = Environment.GetEnvironmentVariable("OPENAI_API_ENDPOINT");
            var openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            _openAIClient = new(aiEndpoint!, openAiKey!);

        }

        public void OnGet()
        {
            Current = new DesignForm() { Id = Guid.NewGuid().ToString() };
        }

        public async Task OnPostAsync()
        {
            var prompt = await _openAIClient.DescribeImage(Current.RoomType, Current.Style, ImageFile?.OpenReadStream()?.GetByteArrayFromStream(), ImageFile?.ContentType!);

            _logger.LogInformation($"Image Prompt: {prompt}");

            _form.ImageBytes = await _openAIClient.GetImageVariationAsync(prompt!, ImageFile.FileName, ImageFile.OpenReadStream());
            _form.RoomType = Current.RoomType;
            Current = _form;
        }
    }
}
