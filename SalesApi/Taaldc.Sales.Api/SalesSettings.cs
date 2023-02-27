namespace Taaldc.Sales.API;

public class SalesSettings
{
    public string PicBaseUrl { get; set; }

    public string EventBusConnection { get; set; }

    public bool UseCustomizationData { get; set; }

    public bool AzureStorageEnabled { get; set; }
}