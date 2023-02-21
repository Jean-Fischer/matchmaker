using Business.Services.RandomUserGeneration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.RandomUserGeneration
{
    public  interface IRandomUserGeneration
    {

        Task<GeneratedPlayerResults> GenerateRandomUsers(int numberOfUsers, CancellationToken cancellationToken);
    }
}
