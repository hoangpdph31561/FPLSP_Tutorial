using System.Globalization;
using System.Reflection;
using FPLSP_Tutorial.Application.Interfaces.Services;
using Microsoft.Extensions.Localization;

namespace FPLSP_Tutorial.Infrastructure.Implements.Services;

public class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer _stringLocalizer;

    public LocalizationService(IStringLocalizerFactory factory)
    {
        _stringLocalizer = factory.Create("AppResource", Assembly.GetEntryAssembly()?.FullName ?? string.Empty);
    }

    public LocalizedString this[string name] => _stringLocalizer[name];

    public LocalizedString this[string name, params object[] arguments] => _stringLocalizer[name, arguments];

    public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _stringLocalizer.GetAllStrings(includeParentCultures);
    }
}