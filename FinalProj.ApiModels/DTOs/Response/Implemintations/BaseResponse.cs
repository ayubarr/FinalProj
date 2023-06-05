using FinalProj.ApiModels.Response.Interfaces;
using FinallApp.ValidationHelper;
using Newtonsoft.Json;

namespace FinalProj.ApiModels.Response.Implemintations
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        private T _data;

        public T Data
        {
            get
            {
                return _data;
            }
  
            set
            {
                try
                {
                    ObjectValidator<T>.CheckIsNotNullObject(value);
                    _data = value;
                }
                catch(ArgumentNullException exception)
                {
                    _message += $"\nERROR: {exception}";
                }
            }
        }

        private Guid? _requestId;
        public Guid? requestId
        {
            get
            {
                return _requestId;
            }

            set
            {
                try
                {
                    ObjectValidator<Guid?>.CheckIsNotNullObject(value);
                    _requestId = value;
                }
                catch (ArgumentNullException exception)
                {
                    _message += $"\nERROR: {exception}";
                }
            }
        }
        private string? _userId;
        public string? userId
        {
            get
            {
                return _userId;
            }

            set
            {
                try
                {
                    ObjectValidator<string?>.CheckIsNotNullObject(value);
                    _userId = value;
                }
                catch (ArgumentNullException exception)
                {
                    _message += $"\nERROR: {exception}";
                }
            }
        }

        private int _statusCode;

        public int StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                try
                {
                    NumberValidator<int>.IsPositive(value);
                    _statusCode = value;
                }
                catch(ArgumentException exception)
                {
                    _message += $"\nERROR: {exception}";

                }
            }
        }


        private string _message;

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                try
                {
                    StringValidator.CheckIsNotNull(value);
                    _message = value;
                }
                catch(ArgumentNullException exception)
                {
                    _message += $"\nERROR: {exception}";
                }
            }
        }


        public bool IsSuccess { get; set; }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
