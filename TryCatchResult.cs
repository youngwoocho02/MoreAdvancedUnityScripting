using System;
using System.Threading.Tasks;
using UnityEngine;

public static class TryCatchResult
{
    public static Result<T> Try<T>(Func<T> action)
    {
        try
        {
            return Result<T>.Success(action());
        }
        catch (Exception e)
        {
            return Result<T>.Failure(new ErrorDetails(e.GetType().Name, e.Message));
        }
    }
    public static Result<Success> Try(Action action)
    {
        try
        {
            action();
            return Result<Success>.Success(new Success());
        }
        catch (Exception e)
        {
            return Result<Success>.Failure(new ErrorDetails(e.GetType().Name, e.Message));
        }
    }

    public static async Awaitable<Result<T>> ToResult<T>(this Task<T> task)
    {
        try
        {
            return Result<T>.Success(await task);
        }
        catch (Exception e)
        {
            return Result<T>.Failure(new ErrorDetails(e.GetType().Name, e.Message));
        }
    }
    public static async Awaitable<Result<Success>> ToResult(this Task task)
    {
        try
        {
            await task;
            return Result<Success>.Success(new Success());
        }
        catch (Exception e)
        {
            return Result<Success>.Failure(new ErrorDetails(e.GetType().Name, e.Message));
        }
    }
}