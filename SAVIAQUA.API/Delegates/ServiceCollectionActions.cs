using Microsoft.AspNetCore.Antiforgery;

namespace SAVIAQUA.API.Delegates;

public static class ServiceCollectionActions
{
    public static void SetupAntiForgery(AntiforgeryOptions options)
    {
        options.SuppressXFrameOptionsHeader = true;
    }
}
