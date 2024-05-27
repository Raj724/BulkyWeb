﻿using System;
namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository category { get; }

        IProductRepository Product { get; }

        void save();
	}
}
