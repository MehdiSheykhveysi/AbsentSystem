using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace AbsentSystem.Infrastructure.DTO
{
    public class AjaxResult
    {
        public string Status { get; set; }
        public string RedirectUrl { get; set; }
        public string MessageWhenSuccessed { get; set; }
        public List<string> Errors { get; set; }

        public AjaxResult()
        {
            this.Errors = new List<string>();
        }

        public AjaxResult(string Status) : this()
        {
            this.Status = Status;
        }

        public void AddErrrs(ModelStateDictionary Modelstate)
        {
            this.Errors.AddRange(Modelstate.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage));
        }
    }
}
