using System.Globalization;
using Microsoft.Extensions.Localization;

namespace FPLSP_Tutorial.Application.Interfaces.Services;

public interface ILocalizationService
{
    LocalizedString this[string name] { get; }

    LocalizedString this[string name, params object[] arguments] { get; }

    CultureInfo CurrentCulture { get; }

    IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures);
}