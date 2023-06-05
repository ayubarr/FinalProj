using FinalProj.Domain.Models.Abstractions.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProj.Tests.Repositories
{
    public class TestEntity : BaseEntity
    {
        public string Name { get; set; }
    }
}
