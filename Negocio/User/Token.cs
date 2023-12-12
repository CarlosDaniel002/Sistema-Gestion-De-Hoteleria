using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.UserData;

namespace Negocio.User
{
    public class Token
    {
        public static Guid GetRandomTokenAndKeepIt()
        {
            dynamic? varToken = Guid.NewGuid();
            return varToken;
        }
        public static Guid GetRandomTokenAndKeepItDB(int ID)
        {
            dynamic?  varToken = Guid.NewGuid();
            RegiststionInDB.SetTokenForUser(ID,varToken);
            return varToken;
        }

        public static dynamic GetTokenForIdUser(int ID)
        {
            dynamic? varToken = RegiststionInDB.GetTokenAtIdUser(ID);
            return varToken;
        }
    }
}
