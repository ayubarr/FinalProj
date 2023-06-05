using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProj.Tests.Entities
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }

        public ServiceResult(T data)
        {
            Data = data;
        }
    }

}
