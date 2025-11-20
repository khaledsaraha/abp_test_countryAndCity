namespace Acme.BookStore;

public static class BookStoreDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */

    // Country error codes
    public const string CountryNameAlreadyExists = "BookStore:Country:NameAlreadyExists";
    public const string CountryCannotDeleteHasCities = "BookStore:Country:CannotDeleteHasCities";

    // City error codes
    public const string CityNameAlreadyExists = "BookStore:City:NameAlreadyExists";
    public const string CityCountryNotFound = "BookStore:City:CountryNotFound";
}
