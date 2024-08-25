using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BlogPost.Queries.GetBlogPostById
{
    public class GetBlogPostByIdHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IMapper mapper;

        public GetBlogPostByIdHandler(IBlogPostRepository blogPostRepository, IMapper mapper)
        {
            this.blogPostRepository = blogPostRepository;
            this.mapper = mapper;
        }

        public async Task<BlogPostDto> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(request.Id);

            if (blogPost == null)
            {
                throw new Exception($"BlogPost with ID {request.Id} not found.");
            }

            return mapper.Map<BlogPostDto>(blogPost);
        }
    }
}
