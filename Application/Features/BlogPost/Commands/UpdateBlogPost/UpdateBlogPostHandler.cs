using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.BlogPost.Commands.UpdateBlogPost
{
    public class UpdateBlogPostHandler : IRequestHandler<UpdateBlogPostCommand, BlogPostDto>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public UpdateBlogPostHandler(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<BlogPostDto> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPostToUpdate = await blogPostRepository.GetByIdAsync(request.Id);

            if (blogPostToUpdate == null)
            {
                throw new Exception($"BlogPost with ID {request.Id} not found.");
            }

            mapper.Map(request, blogPostToUpdate);

            blogPostToUpdate.Categories.Clear();
            foreach (var categoryId in request.CategoryIds)
            {
                var category = await categoryRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    blogPostToUpdate.Categories.Add(category);
                }
            }

            await blogPostRepository.UpdateAsync(blogPostToUpdate);

            return mapper.Map<BlogPostDto>(blogPostToUpdate);
        }
    }
}