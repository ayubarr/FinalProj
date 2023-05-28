﻿using FinalApp.ApiModels.DTOs.CommonDTOs.BaseDTOs;
using FinalApp.ApiModels.Response.Implemintations;
using Microsoft.AspNetCore.Identity;

namespace FinalApp.ApiModels.Response.Helpers
{
    public static class ResponseFactory<T>
    {
        public static BaseResponse<T> CreateSuccessResponse(T model)
        {
           
            return new BaseResponse<T>
            {
                IsSuccess = true,
                Data = model,
                StatusCode = 200,
            };
        }

        public static BaseResponse<T> CreateNotFoundResponse(Exception exception)
        {
            return new BaseResponse<T>()
            {
                StatusCode = 404,
                IsSuccess = false,
                Message = "no records found in the database.\n\r" +
                    $"Error: {exception}",
            };
        }

        public static BaseResponse<T> CreateErrorResponse(Exception exception)
        {
            return new BaseResponse<T>()
            {
                StatusCode = 500,
                IsSuccess = false,
                Message = "internal server error.\n\r" +
                $"Error: {exception}",
            };
        }
        public static BaseResponse<T> CreateInvalidOperationResponse(Exception exception)
        {
            return new BaseResponse<T>()
            {
                StatusCode = 400,
                IsSuccess = false,
                Message = "Unable to create the specified model.\n\r" +
                $"Error: {exception}",
            };
        }
        public static BaseResponse<T> CreateUnauthorizedResponse(Exception exception)
        {
            return new BaseResponse<T>()
            {
                StatusCode = 401,
                IsSuccess = false,
                Message = "Failed authorization attempt \n\r" +
                $"Error: {exception}",
            };
        }
    }
}
