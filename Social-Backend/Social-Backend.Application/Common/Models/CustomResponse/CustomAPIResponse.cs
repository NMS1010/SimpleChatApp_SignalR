﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Models.CustomResponse
{
    public class CustomAPIResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public bool IsSuccess { get; set; }

        public static CustomAPIResponse<T> Success(T data, int statusCode)
        {
            return new CustomAPIResponse<T>() { Data = data, StatusCode = statusCode, IsSuccess = true };
        }

        public static CustomAPIResponse<T> Success(int statusCode)
        {
            return new CustomAPIResponse<T>() { StatusCode = statusCode, IsSuccess = true };
        }

        public static CustomAPIResponse<T> Fail(int statusCode, string error)
        {
            return new CustomAPIResponse<T>() { StatusCode = statusCode, Errors = new List<string>() { error }, IsSuccess = false };
        }

        public static CustomAPIResponse<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomAPIResponse<T>() { StatusCode = statusCode, Errors = errors, IsSuccess = false };
        }
    }
}