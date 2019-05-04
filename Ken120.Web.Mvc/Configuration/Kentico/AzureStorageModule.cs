using CMS;
using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.IO;
using Ken120.Web.Mvc.Configuration.Kentico;

[assembly: RegisterModule(typeof(AzureStorageModule))]

namespace Ken120.Web.Mvc.Configuration.Kentico
{
    public class AzureStorageModule : Module
    {
        public AzureStorageModule() : base(nameof(AzureStorageModule)) { }

        // Contains initialization code that is executed when the application starts
        protected override void OnInit()
        {
            base.OnInit();

            bool azureStorageEnabled = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["azure-storage:is-enabled"], true);

            if (!azureStorageEnabled)
            {
                return;
            }

            string contentContainer = ValidationHelper.GetString(SettingsHelper.AppSettings["azure-storage:container-name"], "");

            if (string.IsNullOrWhiteSpace(contentContainer))
            {
                return;
            }

            // Creates a new StorageProvider instance
            var contentProvider = new StorageProvider("Azure", "CMS.AzureStorage")
            {
                // Specifies the target container which should represent the site/codebase
                // ex. 'mag02-cms-content'
                CustomRootPath = contentContainer
            };

            // Maps a directory to the provider
            StorageHelper.MapStoragePath("~/azurestorage", contentProvider);
        }
    }
}
