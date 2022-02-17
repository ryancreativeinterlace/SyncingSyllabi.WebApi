using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Data.Models.Base
{
    public class BaseResponseModel<T> where T : class, new()
    {
        protected BaseResponseModel()
        {
            Data = new T();
            Errors = Enumerable.Empty<string>();
        }
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool HasErrors { get => Errors.Any(); }
    }
}
