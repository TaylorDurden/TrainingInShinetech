﻿using System;
using BugManagement.DAL.IRepository;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.ILogic;
using System.Collections.Generic;
using System.Linq;
using BugManagement.Logic.ModelExchange;
using BugManagement.Logic.Models;

namespace BugManagement.Logic.Logic
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public DeveloperLogic(IDeveloperRepository developerRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _developerRepository = developerRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(DeveloperLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Create(model.ConvertToDeveloper());
                unitWork.Commit();
            }
        }

        public void Edit(DeveloperLogicModel model)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Edit(model.ConvertToDeveloper());
                unitWork.Commit();
            }
        }

        public void Delete(int id)
        {
            using (var unitWork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _developerRepository.Delete(id);
                unitWork.Commit();
            }
        }

        public DeveloperLogicModel Get(int id)
        {
            var developer = _developerRepository.Get(id);
            return developer.ConvertToDeveloperLogicModel();
        }

        public List<DeveloperLogicModel> GetAll()
        {
            var model = _developerRepository.Query();
            return !model.Any() ? null : model.ToList().Select(m => m.ConvertToDeveloperLogicModel()).ToList();
        }
        
        public int GetPageCountByCondition(string serchCondition)
        {
            return
                _developerRepository
                    .Query()
                    .Count(n =>
                            string.IsNullOrEmpty(serchCondition) || n.FristName.Contains(serchCondition) ||
                            n.LastName.Contains(serchCondition) || n.Email.Contains(serchCondition) ||
                            n.Status.Contains(serchCondition));
        }

        public List<DeveloperLogicModel> GetDeveloperLogicModelsByCondition(string serchCondition, int pageIndex, int pageSize, int count)
        {
            var pageCount = (int)Math.Ceiling(count / (double)pageSize);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            var model = _developerRepository.Query()
                .Where(n =>
                            string.IsNullOrEmpty(serchCondition) || n.FristName.Contains(serchCondition) ||
                            n.LastName.Contains(serchCondition) || n.Email.Contains(serchCondition) ||
                            n.Status.Contains(serchCondition)).OrderBy(n => n.DeveloperId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return model.Select(m => m.ConvertToDeveloperLogicModel()).ToList();
        }
    }
}
