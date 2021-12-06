using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Define
{
    public interface IUnitOfWork
    {
        INoteRepository NoteRepository { get; }
    }
}
