using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface.RepositoryInterface;
using MISA.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly IConfiguration configuration;

        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
