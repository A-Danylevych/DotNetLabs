using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Models.Result
{
    public class Result
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