using Dapper;
using ModelRepositories.BaseModel;
using ORM.Define;

namespace Repository.Define
{
    public class NoteRepo : INoteRepository
    {
        private readonly IDataFactory dataFactory;
        public NoteRepo(IDataFactory data)
        {
            dataFactory = data;
        }
        public Task<Note> Add(Note entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", entity.Id);

            string sql = @"SELECT n.[Id], n.[Name], n.[Content] FROM Note n WHERE n.[Id] = @id";

            return dataFactory.Get<Note>(sql, parameters);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Note>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
