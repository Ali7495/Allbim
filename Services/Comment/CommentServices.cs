using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Models.Comment;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IRepository<Article> _articlerepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentServices(ICommentRepository commentRepository, IRepository<Article> articleRepository, IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _articlerepository = articleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CommentResultViewModel> ApproveComment(long id, CancellationToken cancellationToken)
        {
            Comment comment = await _commentRepository.GetByIdAsync(cancellationToken, id);
            if (comment == null)
            {
                throw new BadRequestException("همچین نظری وجود ندارد");
            }

            comment.IsApproved = true;

            await _commentRepository.UpdateAsync(comment, cancellationToken);

            return _mapper.Map<CommentResultViewModel>(comment);
        }

        public async Task<bool> DeleteComment(long id, CancellationToken cancellationToken)
        {
            Comment comment = await _commentRepository.GetByIdAsync(cancellationToken, id);
            if (comment == null)
            {
                throw new BadRequestException("همچین نظری وجود ندارد");
            }

            comment.IsDelete = true;

            await _commentRepository.UpdateAsync(comment, cancellationToken);

            return true;
        }

        public async Task<PagedResult<CommentResultViewModel>> GetAllComments(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<Comment> comments = await _commentRepository.GetAllComments(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CommentResultViewModel>>(comments);
        }

        public async Task<List<CommentResultViewModel>> GetArticleComments(long id, CancellationToken cancellationToken)
        {
            Article article = await _articlerepository.GetByIdAsync(cancellationToken, id);
            if (article == null)
            {
                throw new BadRequestException("همچین مقاله ای وجود ندارد");
            }

            List<Comment> comments = await _commentRepository.GetArticleComments(id, cancellationToken);

            return _mapper.Map<List<CommentResultViewModel>>(comments);

        }
        public async Task<List<CommentResultViewModel>> GetArticleCommentsMine(long UserID,long id, CancellationToken cancellationToken)
        {
            Article article = await _articlerepository.GetByIdAsync(cancellationToken, id);
            if (article == null)
            {
                throw new BadRequestException("همچین مقاله ای وجود ندارد");
            }
            await CheckAuthorIDCheck(UserID, article.AuthorId, cancellationToken);
            List<Comment> comments = await _commentRepository.GetArticleComments(id, cancellationToken);

            return _mapper.Map<List<CommentResultViewModel>>(comments);

        }

        public async Task<CommentResultViewModel> PostNewComment(long id, long? userId, CommentInputViewModel model, CancellationToken cancellationToken)
        {
            Article article = await _articlerepository.GetByIdAsync(cancellationToken, id);
            if (article == null)
            {
                throw new BadRequestException("همچین مقاله ای وجود ندارد");
            }

            User user = await _userRepository.GetByIdAsync(cancellationToken, userId.Value);

            Comment comment = new Comment()
            {
                AuthorId = user.PersonId,
                ArticleId = id,
                ParentId = model.ParentId,
                Description = model.Description,
                Score = model.Score,
                IsApproved = false,
                IsDelete = false
            };

            //User user = await _userRepository.GetByIdAsync(cancellationToken,userId.Value);
            //if (user != null)
            //{
            //    comment.AuthorId = user.PersonId;
            //}

            await _commentRepository.AddAsync(comment, cancellationToken);

            return _mapper.Map<CommentResultViewModel>(comment);
        }

        public async Task<CommentResultViewModel> UpdateComment(long id, CommentInputViewModel model, CancellationToken cancellationToken)
        {
            Comment comment = await _commentRepository.GetByIdAsync(cancellationToken, id);
            if (comment == null)
            {
                throw new BadRequestException("همچین نظری وجود ندارد");
            }

            comment.ParentId = model.ParentId;
            comment.Description = model.Description;

            await _commentRepository.UpdateAsync(comment, cancellationToken);

            return _mapper.Map<CommentResultViewModel>(comment);
        }

        public async Task<CommentResultViewModel> UpdateCommentMine(long UserID, long id, CommentInputViewModel model, CancellationToken cancellationToken)
        {

            Comment comment = await _commentRepository.GetByIdAsync(cancellationToken, id);
            if (comment == null)
            {
                throw new BadRequestException("همچین نظری وجود ندارد");
            }
            await CheckAuthorIDCheck(UserID, (long)comment.AuthorId, cancellationToken);

            comment.ParentId = model.ParentId;
            comment.Description = model.Description;

            await _commentRepository.UpdateAsync(comment, cancellationToken);

            return _mapper.Map<CommentResultViewModel>(comment);
        }

        public async Task CheckAuthorIDCheck(long UserID,long? AuthorId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetwithPersonById(cancellationToken, UserID);
            if (user == null)
                throw new NotFoundException("کاربر یافت نشد");
            if (AuthorId != user.PersonId)
                throw new BadRequestException("شما ثبت کننده این کامنت نیستید");
        }

        public async Task<bool> DeleteCommentMine(long UserID, long id, CancellationToken cancellationToken)
        {
            Comment comment = await _commentRepository.GetByIdAsync(cancellationToken, id);
            if (comment == null)
            {
                throw new BadRequestException("همچین نظری وجود ندارد");
            }
            await CheckAuthorIDCheck(UserID, (long)comment.AuthorId, cancellationToken);

            comment.IsDelete = true;

            await _commentRepository.UpdateAsync(comment, cancellationToken);

            return true;
        }

        public async Task<CommentResultViewModel> GetCommentMine(long UserID, long id, CancellationToken cancellationToken)
        {
            Comment comment = await _commentRepository.GetByIdAsync(cancellationToken, id);
            if (comment == null)
            {
                throw new BadRequestException("همچین نظری وجود ندارد");
            }
            await CheckAuthorIDCheck(UserID, (long)comment.AuthorId, cancellationToken);
            return _mapper.Map<CommentResultViewModel>(comment);
        }

        public async Task<CommentResultViewModel> GetComment(long id, CancellationToken cancellationToken)
        {
            Comment comment = await _commentRepository.GetByIdAsync(cancellationToken, id);
            if (comment == null)
            {
                throw new BadRequestException("همچین نظری وجود ندارد");
            }
            return _mapper.Map<CommentResultViewModel>(comment);
        }
    }
}
