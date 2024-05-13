namespace UnicontaAPI.Shared
{
    public class UnicontaErrorCodesError(ErrorCodes errorCodes)
        : Error(nameof(ErrorCodes), errorCodes.ToString())
    { }
}