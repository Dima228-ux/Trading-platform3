using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionEntities
{
    class WrongUpdateException:Exception
    {
        public WrongUpdateException() : base("Не получилось обновить данные пользователя") { }
    }
}
