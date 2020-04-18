using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreModels.ViewModel
{
    public class ResultResponse<TModel>
    {
        public TModel Entity { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
