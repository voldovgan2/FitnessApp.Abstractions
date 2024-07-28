﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessApp.Common.Abstractions.Db.Repository.Generic;
using FitnessApp.Common.Abstractions.Extensions;
using FitnessApp.Common.Abstractions.Models.Generic;
using FitnessApp.Common.Abstractions.Models.Validation;
using FitnessApp.Common.Abstractions.Services.Validation;
using FitnessApp.Common.Paged.Models.Input;
using FitnessApp.Common.Paged.Models.Output;

namespace FitnessApp.Common.Abstractions.Services.Generic;

public abstract class GenericService<
    TGenericModel,
    TCreateGenericModel,
    TUpdateGenericModel>(
        IGenericRepository<
            TGenericModel,
            TCreateGenericModel,
            TUpdateGenericModel> repository
    )
    : IGenericService<
        TGenericModel,
        TCreateGenericModel,
        TUpdateGenericModel>
    where TGenericModel : IGenericModel
    where TCreateGenericModel : ICreateGenericModel
    where TUpdateGenericModel : IUpdateGenericModel
{
    public async Task<TGenericModel> GetItemByUserId(string userId)
    {
        ValidationHelper.ThrowExceptionIfNotValidatedEmptyStringField(nameof(userId), userId);

        var result = await repository.GetItemByUserId(userId);
        return result;
    }

    public async Task<IEnumerable<TGenericModel>> GetItemsByIds(IEnumerable<string> ids)
    {
        var result = await repository.GetItemsByIds(ids);
        return result;
    }

    public async Task<PagedDataModel<TGenericModel>> GetItemsByIds(GetPagedByIdsDataModel model)
    {
        var result = await repository.GetItemsByIds(model);
        return result;
    }

    public async Task<TGenericModel> CreateItem(TCreateGenericModel model)
    {
        var validationErrors = ValidateCreateGenericModel(model);
        validationErrors.ThrowIfNotEmpty();

        var result = await repository.CreateItem(model);
        return result;
    }

    public async Task<TGenericModel> UpdateItem(TUpdateGenericModel model)
    {
        var validationErrors = ValidateUpdateGenericModel(model);
        validationErrors.ThrowIfNotEmpty();

        var result = await repository.UpdateItem(model);
        return result;
    }

    public async Task<string> DeleteItem(string userId)
    {
        ValidationHelper.ThrowExceptionIfNotValidatedEmptyStringField(nameof(userId), userId);

        var result = await repository.DeleteItem(userId);
        return result;
    }

    protected virtual IEnumerable<ValidationError> ValidateCreateGenericModel(TCreateGenericModel model)
    {
        var errors = new List<ValidationError>();
        errors.AddIfNotNull(ValidationHelper.ValidateEmptyStringField(nameof(model.UserId), model.UserId));
        return errors;
    }

    protected virtual IEnumerable<ValidationError> ValidateUpdateGenericModel(TUpdateGenericModel model)
    {
        var errors = new List<ValidationError>();
        errors.AddIfNotNull(ValidationHelper.ValidateEmptyStringField(nameof(model.UserId), model.UserId));
        return errors;
    }
}