using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Application.DataTransferObjects.ClientPost.Response
{
    //Liệt kê các bài viết mẹ và con (theo id)
    public class ClientPostParentChildResponse
    {
        //Thông tin bài viết mẹ
        public ClientPostDTO ParentPost { get; set; }
        //Thông tin các bài viết con
        public List<ClientPostDTO> ChildPosts { get; set; }
    }
}
