using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interface;
using MISA.ApplicationCore.Interface.RepositoryInterface;
using MISA.Infrastructure.BaseRepository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MISA.Infrastructure.Repository
{
    /// <summary>
    /// Thực thi các phương thức của ICustomerRepository
    /// </summary>
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly IConfiguration configuration;

        public CustomerRepository(IConfiguration configuration):base(configuration)
        {
           
        }
    }
}
