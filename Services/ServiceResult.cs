﻿using System.Net;

namespace App.Services;
public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMesage { get; set; }
    public bool IsSuccess => ErrorMesage == null || ErrorMesage.Count == 0;
    public bool IsFail => !IsSuccess;
    public HttpStatusCode Status { get; set; }
    //static factory method
    public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status = status
        };
    }

    public static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMesage = errorMessages,
            Status = status
        };
    }

    public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMesage = new List<string>() { errorMessage },
            Status = status
        };
    }
}

public class ServiceResult
{
    public List<string>? ErrorMesage { get; set; }
    public bool IsSuccess => ErrorMesage == null || ErrorMesage.Count == 0;
    public bool IsFail => !IsSuccess;
    public HttpStatusCode Status { get; set; }
    //static factory method
    public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            Status = status
        };
    }

    public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMesage = errorMessages,
            Status = status
        };
    }

    public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMesage = new List<string>() { errorMessage },
            Status = status
        };
    }
}


