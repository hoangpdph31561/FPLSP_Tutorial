﻿using AutoMapper;
using FPLSP_Tutorial.Application.DataTransferObjects.Post.Request;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Application.Interfaces.Services;
using FPLSP_Tutorial.Infrastructure.ViewModels.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Tutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostReadOnlyRespository _postReadOnlyRespository;
        private readonly IPostReadWriteRespository _postReadWriteRespository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public PostsController(IPostReadOnlyRespository postReadOnlyRespository, IPostReadWriteRespository postReadWriteRespository, ILocalizationService localizationService, IMapper mapper)
        {
            _localizationService = localizationService;
            _mapper = mapper;
            _postReadOnlyRespository = postReadOnlyRespository;
            _postReadWriteRespository = postReadWriteRespository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] ViewPostWithPaginationRequest request, CancellationToken cancellationToken)
        {
            PostListWithPaginationViewModel vm = new(_postReadOnlyRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id, CancellationToken cancellationToken)
        {
            PostViewModel vm = new(_postReadOnlyRespository, _localizationService);
            await vm.HandleAsync(id, cancellationToken);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewPost(PostCreateRequest request, CancellationToken cancellationToken)
        {
            PostCreateViewModel vm = new(_postReadOnlyRespository, _postReadWriteRespository, _localizationService, _mapper);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePost(PostUpdateRequest request, CancellationToken cancellationToken)
        {
            PostUpdateViewModel vm = new(_postReadWriteRespository, _localizationService, _mapper);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePost(PostDeleteRequest request, CancellationToken cancellationToken)
        {
            PostDeleteViewModel vm = new(_postReadWriteRespository, _localizationService);
            await vm.HandleAsync(request, cancellationToken);
            return Ok(vm);
        }
    }
}
