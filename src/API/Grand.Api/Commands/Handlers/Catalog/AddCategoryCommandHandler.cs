﻿using Grand.Api.Commands.Models.Catalog;
using Grand.Api.DTOs.Catalog;
using Grand.Api.Extensions;
using Grand.Business.Core.Interfaces.Catalog.Categories;
using Grand.Business.Core.Interfaces.Common.Seo;
using Grand.Domain.Catalog;
using MediatR;

namespace Grand.Api.Commands.Handlers.Catalog;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, CategoryDto>
{
    private readonly ICategoryService _categoryService;
    private readonly ISlugService _slugService;
    private readonly ISlugNameValidator _slugNameValidator;

    public AddCategoryCommandHandler(
        ICategoryService categoryService,
        ISlugService slugService,
        ISlugNameValidator slugNameValidator)
    {
        _categoryService = categoryService;
        _slugService = slugService;
        _slugNameValidator = slugNameValidator;
    }

    public async Task<CategoryDto> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = request.Model.ToEntity();
        await _categoryService.InsertCategory(category);
        request.Model.SeName = await _slugNameValidator.ValidateSeName(category, request.Model.SeName, category.Name, true);
        category.SeName = request.Model.SeName;
        await _categoryService.UpdateCategory(category);
        await _slugService.SaveSlug(category, request.Model.SeName, "");

        return category.ToModel();
    }
}