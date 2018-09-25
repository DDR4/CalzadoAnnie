using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Annies.Entities;

namespace Annies.BusinessLogic
{
    public class Authorization
    {
        private DataAccess.Authorization repository;

        public Authorization()
        {
            repository = new DataAccess.Authorization();
        }
        public async Task<Common.Response<Entities.Usuario>> Authorize(Usuario credential)
        {
            try
            {
                var result = await repository.Authorize(credential);

                if (result == null)
                {
                    return new Common.Response<Usuario>("Usuario o password incorrectos.");
                }

                return new Common.Response<Usuario>(result);
            }
            catch (Exception ex)
            {
                return new Common.Response<Usuario>(ex);
            }
        }
    }
}
