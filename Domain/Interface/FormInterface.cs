using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface FormInterface
    {
        List<_FormViewModel> GetUserList();
        _FormViewModel GetModelForEdit(int Id);
        string AddUser(_FormViewModel model);
        string UpdateUser( _FormViewModel model);
        string DeleteUser( int Id );
    }
}
