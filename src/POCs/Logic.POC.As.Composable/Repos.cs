using JP.Base.DAL.ADO.Contracts;
using JP.Base.DAL.ADO.EntityMappers.AttributeMapping;

namespace Logic.POC.As.Composable
{
    public interface JPRepo
    {
        Operator GetById(object id);

        void Update(Operator entityToUpdate);
    }

    internal class OperatorRepo : JPRepo//: IRepository<Operator>
    {
        private IDbConnectionExecutables conn;

        public OperatorRepo(IDbConnectionExecutables conn)
        {
            this.conn = conn;
        }

        //public void Delete(Operator entityToDelete)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteById(object id)
        //{
        //    throw new NotImplementedException();
        //}

        public Operator GetById(object id)
        {
            conn.CreateCommand($"select * from Operators  where id ={id}");
            var data = conn.ExecuteReaderCommand();
            var op = data.MapEntity<Operator>();
            return op;
        }

        //public void Insert(Operator entity)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// only updates document / firstname
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public void Update(Operator entityToUpdate)
        {
            conn.CreateCommand($"update Operators set Document ='{entityToUpdate.Document}', FirstName = '{entityToUpdate.FirstName}' where id ={entityToUpdate.Id}");
            conn.ExecuteNonQueryCommand();
        }
    }
}