namespace Langbry
{
    internal interface ILanguage
    {
        string GetLanguageCodeCookieName();

        string GetLanguageCode(string value);
    }
}