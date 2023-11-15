using FPLSP_Tutorial.Application.DataTransferObjects.Tag;

namespace FPLSP_Tutorial.WASM.Pages.QuanLyBaiViet.ViewModels
{
    public class PostListVM
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public List<TagDto> tagDtos { get; set; }
        public string? Type { get; set; }
        public int? STT { get; set; } = 0;
    }
}
