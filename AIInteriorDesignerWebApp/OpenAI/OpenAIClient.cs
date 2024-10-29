using AIInteriorDesignerWebApp.Enums;

using Azure.AI.OpenAI;

using OpenAI.Chat;
using OpenAI.Images;
using System.ClientModel;

namespace AIInteriorDesignerWebApp.OpenAI
{
    public class OpenAIClient
    {
        private AzureOpenAIClient _azureOpenAIClient;

        private ImageClient _imageClient;

        private ChatClient _chatClient;

        public OpenAIClient(string endpoint, string openAiKey)
        {
            _azureOpenAIClient = new(
                new Uri(endpoint),
                new ApiKeyCredential(openAiKey));

            _imageClient = _azureOpenAIClient.GetImageClient("dall-e-3");
            _chatClient = _azureOpenAIClient.GetChatClient("gpt-4o-mini");
        }

        public async Task<string?> DescribeImage(RoomType room, Style style, byte[] imageBytes, string mediaType)
        {
            var chatMessages = new List<UserChatMessage>
            {
                new UserChatMessage(
                    ChatMessageContentPart.CreateTextPart("Describe following image with correct dimensions.")),
                new UserChatMessage(
                    ChatMessageContentPart.CreateImagePart(
                        new BinaryData(imageBytes),
                        mediaType,
                        ChatImageDetailLevel.High)),
                new UserChatMessage(
                    ChatMessageContentPart.CreateTextPart($"Describe this image with furniture for ${room} in style ${style}."))
            };
            ClientResult<ChatCompletion> response = await _chatClient.CompleteChatAsync(chatMessages);

            return response.Value.Content[0].Text;
        }

        public async Task<byte[]> GetImageVariationAsync(string prompt, string fileName, Stream image)
        {
            ClientResult<GeneratedImage> response = await _imageClient.GenerateImageAsync(prompt,
                new ImageGenerationOptions
                {
                    Size = GeneratedImageSize.W1024xH1024,
                    ResponseFormat = GeneratedImageFormat.Bytes
                });

            return response.Value.ImageBytes.ToArray();
        }
    }
}
