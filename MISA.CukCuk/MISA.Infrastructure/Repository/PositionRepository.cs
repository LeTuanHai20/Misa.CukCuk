using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface.RepositoryInterface;
using MISA.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure.Repository
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        private readonly IConfiguration configuration;
        public PositionRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
