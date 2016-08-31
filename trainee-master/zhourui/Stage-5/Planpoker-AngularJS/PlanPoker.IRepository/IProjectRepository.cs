﻿using PlanPoker.Data.Models;

namespace PlanPoker.IRepository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project Get(string name);
    }
}