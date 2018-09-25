using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebCalzadosAnnies.Core.Identity
{
    public class CustomApplicationUser : IUser
    {
        public string Id { get; }
        public string UserName { get; set; }
        public Annies.Common.Response<Annies.Entities.Usuario> Usuario { get; set; }

        public CustomApplicationUser() : this(new Annies.Common.Response<Annies.Entities.Usuario>(default(Annies.Entities.Usuario)))
        {
        }


        public CustomApplicationUser(Annies.Common.Response<Annies.Entities.Usuario> usuario)
        {
            Usuario = usuario;

            if (usuario.InternalStatus == Annies.Common.EnumTypes.InternalStatus.Success)
            {
                Id = usuario.Data.Tipo_Usu.ToString();
                UserName = usuario.Data.Nombre_Usu;
            }
            else
            {
                // Valores por defecto, lo cuales no tendran relevancia, puesto que la aplicación no iniciará sesión.
                Id = "01";
                UserName = "USER";
            }
        }


    }
}