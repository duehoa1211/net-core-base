using ORM.Define;
using Repository.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        public INoteRepository NoteRepository { get; }


        public UnitOfWork(IDataFactory data, string[] keys)
        {
            foreach (var key in keys)
                switch (key)
                {
                    case "note":
                        NoteRepository = new NoteRepo(data);
                        break;
                    default:
                        break;
                }
        }
    }
}
