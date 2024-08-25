using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BlogPost.Commands.CreateBlogPost
{

    public class CreateBlogPostHandler : IRequestHandler<CreateBlogPostCommand, BlogPostDto>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;

        public CreateBlogPostHandler(IBlogPostRepository blogPostRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        public async Task<BlogPostDto> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var BlogPost = mapper.Map<Domain.Entities.BlogPost>(request.Request);

            foreach (var catagoryId in request.Request.Categories) { 
                var category = await categoryRepository.GetByIdAsync(catagoryId);
                if (category != null)
                {
                    BlogPost.Categories.Add(category);
                }
                else
                {
                    throw new Exception("Category not found");
                }
            }
                
            var result = await blogPostRepository.CreateAsync(BlogPost);

            return mapper.Map<BlogPostDto>(result);
        }
    }
}
