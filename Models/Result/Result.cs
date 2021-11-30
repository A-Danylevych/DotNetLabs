using System;
using System.Web.Mvc;

namespace Models.Result
{
    public class Result : ActionResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }

        public static Result Successful()
        {
            return new Result
            {
                Success = true,
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class Result<TData> : Result where TData : class
    {
        public TData? Data { get; set; }

        public static Result<TData> Successful(TData data)
        {
            return new Result<TData>
            {
                Success = true,
                Data = data,
            };
        }
    }
}