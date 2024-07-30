using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Common
{
    public record Result
    {
        public readonly Error? Error;

        public bool IsSucceeded { get; }
        public bool IsFailed => IsSucceeded == false;

        protected Result(bool isSucceeded)
        {
            IsSucceeded = isSucceeded;
        }
        protected Result(Error error)
        {
            IsSucceeded = false;
            Error = error;
        }

        public static Result Success() => new(true);

        public static Result Failed() => new(false);
        public static Result Failed(Error error) => new(error);

        public static implicit operator Result(Error error) => new(error);
    }

    public record Result<TData> : Result
    {
        public readonly TData? Data;
        private Result(TData data) : base(true)
        {
            Data = data;
        }

        private Result(Error error) : base(error)
        {
        }

        public static Result<TData> CreateSuccessResult(TData value) => new(value);

        public static implicit operator Result<TData>(TData value) => new(value);
        public static implicit operator Result<TData>(Error error) => new(error);
    }

}
