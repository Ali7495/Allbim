using albim.Controllers;
using albim.Result;
using Common.Extensions;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Comment;
using Models.PageAble;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class CommentController : BaseController
    {

        #region fields

        private readonly ICommentServices _commentServices;

        #endregion

        #region ctor


        public CommentController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        #endregion

        [HttpGet("")]
        public async Task<ApiResult<PagedResult<CommentResultViewModel>>> GetAllComments([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _commentServices.GetAllComments(pageAbleResult, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<CommentResultViewModel>> Update(long id, [FromBody]CommentInputViewModel model, CancellationToken cancellationToken)
        {
            return await _commentServices.UpdateComment(id, model, cancellationToken);
        }
        [HttpPut("mine/{id}")]
        public async Task<ApiResult<CommentResultViewModel>> UpdateMine(long id, [FromBody]CommentInputViewModel model, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            return await _commentServices.UpdateCommentMine(userId, id, model, cancellationToken);
        }
        [HttpGet("{id}")]
        public async Task<ApiResult<CommentResultViewModel>> GetComment(long id, [FromBody]CommentInputViewModel model, CancellationToken cancellationToken)
        {
            return await _commentServices.GetComment(id,cancellationToken);
        }
        [HttpGet("mine/{id}")]
        public async Task<ApiResult<CommentResultViewModel>> GetCommentMine(long id, [FromBody]CommentInputViewModel model, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            return await _commentServices.GetCommentMine(userId, id,cancellationToken);
        }


        [HttpPatch("{id}/approve")]
        public async Task<ApiResult<CommentResultViewModel>> ApproveComment(long id, CancellationToken cancellationToken)
        {
            return await _commentServices.ApproveComment(id, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteComment(long id, CancellationToken cancellationToken)
        {
            return await _commentServices.DeleteComment(id, cancellationToken);
        }
        [HttpDelete("mine/{id}")]
        public async Task<bool> DeleteCommentMine(long id, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            return await _commentServices.DeleteCommentMine(userId,id, cancellationToken);
        }
    }
}
