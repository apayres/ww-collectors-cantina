﻿using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Domain.IUseCases.Collections
{
    public interface IDeleteCollectionHandler
    {
        Task<ApplicationResponse<Collection>> Delete(Guid collectionId);
    }
}