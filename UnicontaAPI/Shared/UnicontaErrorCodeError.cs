using Uniconta.Common;

namespace UnicontaAPI.Shared
{
    public class UnicontaErrorCodeError(ErrorCodes errorCodes)
        : Error(nameof(ErrorCodes), errorCodes.ToString())
    { }
}