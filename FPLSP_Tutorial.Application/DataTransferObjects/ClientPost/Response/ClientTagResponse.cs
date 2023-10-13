using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response
{
    public class ClientTagResponse
    {
        //id tag
        public Guid Id { get; set; }
        //name tag
        public string Name { get; set; }
    }
}
