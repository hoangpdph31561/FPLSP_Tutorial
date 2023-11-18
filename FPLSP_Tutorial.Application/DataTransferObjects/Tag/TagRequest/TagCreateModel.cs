using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.Tag.TagRequest
{
    public class TagCreateModel
    {
        public string Name { get; set; } = string.Empty;
        public Guid? MajorId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
