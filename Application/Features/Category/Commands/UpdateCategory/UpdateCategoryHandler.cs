﻿using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Category.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository categoryRepository;

        private readonly IMapper mapper;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var categoryUpdateModel = mapper.Map<Domain.Entities.Category>(command.Request);

            categoryUpdateModel.Id = command.Id;

            var result = await categoryRepository.UpdateAsync(categoryUpdateModel);

            return mapper.Map<CategoryDto>(result);
        }
    }
}
