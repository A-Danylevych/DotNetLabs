using System;

namespace Models.Error.Base
{
    public abstract class BaseException: Exception
    {
        public override string Message { get; } = "An exception occur while running.";
    }
}