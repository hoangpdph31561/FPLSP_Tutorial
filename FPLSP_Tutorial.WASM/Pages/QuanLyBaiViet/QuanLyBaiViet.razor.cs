using FPLSP_Tutorial.Application.DataTransferObjects.ClientPost;
using FPLSP_Tutorial.Application.DataTransferObjects.Tag;
using FPLSP_Tutorial.Application.ValueObjects.Pagination;
using FPLSP_Tutorial.Application.ValueObjects.Response;
using FPLSP_Tutorial.WASM.Pages.QuanLyBaiViet.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using static MudBlazor.CategoryTypes;

namespace FPLSP_Tutorial.WASM.Pages.QuanLyBaiViet
{
    public partial class QuanLyBaiViet
    {
        [Parameter]
        [Inject] public HttpClient _http { get; set; }
        private List<PostListVM> _posts = new List<PostListVM>();
        private MudTable<PostListVM>? _tableRef;

        protected override async Task OnInitializedAsync()
        {
            await GetListPost();
        }

        private TableGroupDefinition<PostListVM> _groupDefinition = new()
        {
            GroupName = "",
            Indentation = false,
            Expandable = true,
            IsInitiallyExpanded = false,
            Selector = (e) => e.Title,
        };


        private async Task GetListPost()
        {
            // lấy danh sách bài viết theo IdMajor
            var postAPI = await _http.GetFromJsonAsync<PaginationResponse<PostMainDTO>>($"https://localhost:7225/api/CLientPosts/getPostByMajorId?MajorId={null}");

            // lấy listPost
            var post = postAPI!.Data!.ToList();

            if (post != null)
            {
                foreach (var item in post)
                {
                    // lấy listTag theo idPost
                    var TagAPI = await _http.GetFromJsonAsync<RequestResult<List<TagDto>>>($"https://localhost:7225/api/Tag?IdMajor={null}&IdPost={item.Id}");

                    var result = new PostListVM()
                    {
                        Id = item.Id,
                        STT = +1,
                        tagDtos = TagAPI!.Data!.ToList(),
                        Title = item.Title,
                        Type = item.PostType
                    };
                    _posts.Add(result);
                }
            }
        }
    }
}
