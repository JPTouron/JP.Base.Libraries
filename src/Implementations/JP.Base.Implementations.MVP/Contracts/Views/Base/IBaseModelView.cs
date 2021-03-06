﻿namespace JP.Base.Implementations.MVP.Contracts.Views.Base
{
    public interface IBaseModelView<TModel> where TModel : class
    {
        /// <summary>
        /// sets and gets the model currently being handled by the view
        /// </summary>
        TModel Model { get; set; }
    }
}