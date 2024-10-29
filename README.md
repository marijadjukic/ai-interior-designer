# ai-interior-designer

Web App powered by AI to design your interior.

## Prerequisites

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/dotnet).
- [Visual Studio 2022](https://www.visualstudio.com/downloads) with the **ASP.NET and web development** workload.
- **(Optional)** To try GitHub Copilot, a [GitHub Copilot account](https://docs.github.com/copilot/using-github-copilot/using-github-copilot-code-suggestions-in-your-editor). A 30-day free trial is available.

## Deploy to Azure

### Deploy Azure Open AI

Run following bash commands from terminal to deploy Azure Open AI with GPT and DALL-E:

   ```bash
   # login to Azure
   az login
   # set subscription
   az account set --subscription "<subscription-id>"
   # create resource group
   az group create --location eastus --name "<rg-name>"
   # deploy web app
   az deployment group create --resource-group "<rg-name>" --template-file "./quickstarts/azuredeploy.json"
   ```

### Publish Web App

1. Publish your web app to Azure with new profile: [Publish Web App](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-vs#2-publish-your-web-app)

2. In web app (App Service), under _Settings_, go to _Environment Variables_ and add following:
   - OPENAI_API_ENDPOINT
   - OPENAI_API_KEY

   > Get OpenAI API endpoint and key from Developer tab in overview page of deployed Azure OpenAI resource.

3. When publishing updated code to Azure follow [Redeploy Web App](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-vs#3-update-the-app-and-redeploy)

## Run Locally

1. Ensure that you deployed Azure Open AI from previous section: [Deploy Azure Open AI](#deploy-azure-open-ai)

2. Update environment variables **OPENAI_API_ENDPOINT** and **OPENAI_API_KEY** in [launchSettings.json](./AIInteriorDesignerWebApp/Properties/launchSettings.json)

3. In Visual Studio, next to _Debug_ | _Any CPU_, select **https** launch mode

4. Go to _Debug_ -> _Start Without Debugging_ or _Start Debugging_ (depending whether you want to debug a code or not)
   > This will launch web app in new browser tab
