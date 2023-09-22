﻿using FPLSP_Tutorial.Application.ValueObjects.Respone;

namespace FPLSP_Tutorial.Application.ViewModels
{
    /// <summary>
    ///     Provide all common field in view model
    /// </summary>
    public abstract class ViewModelBase<TDataType> : APIRespone
    {
        public abstract Task HandleAsync(TDataType data, CancellationToken cancellationToken);
    }
}
