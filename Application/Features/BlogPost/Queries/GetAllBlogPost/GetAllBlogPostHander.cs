using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BlogPost.Queries.GetAllBlogPost
{
    public class GetAllBlogPostHander : IRequestHandler<GetAllBlogPostQuery, List<BlogPostDto>>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IMapper mapper;

        public GetAllBlogPostHander(IBlogPostRepository blogPostRepository, IMapper mapper)
        {
            this.blogPostRepository = blogPostRepository;
            this.mapper = mapper;
        }

        public async Task<List<BlogPostDto>> Handle(GetAllBlogPostQuery request, CancellationToken cancellationToken)
        {
            var blogposts =  await blogPostRepository.GetAllAsync();
            var result = mapper.Map<List<BlogPostDto>>(blogposts);
            return result;
            
        }
    }
}
