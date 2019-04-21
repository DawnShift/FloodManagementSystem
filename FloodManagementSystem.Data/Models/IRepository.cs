/*---------------------------------------------------------------------------------------------------------------
 * File Name   : IRepository.cs
 * Purpose     : Interface Class for Repository
 * Date Created: 16 April 2018
 * Created By  : 10xDS Team
 * 
 * History
 * ==============================================================================================================
 * Number   Date Modified   Modification Information      Modified By
 * ==============================================================================================================
 *       
 * 
 * ==============================================================================================================
 *
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FloodManagementSystem.Data.Models
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IQueryable<T> FilteredGetAll();
        IQueryable<T> FilteredGet();
        SqlParameter[] ExecuteSPCommands(string sqlQuery, SqlParameter[] parameters);
        T Get(long id);
        void Insert(T entity);
        void InsertMany(List<T> entity);
        void Update(T entity);
        void UpdateMany(List<T> entity);
        void Delete(T entity);
        void DeleteMany(List<T> entity);
        void Remove(T entity);
        void InitContext(FloodManagementSystemContext context);
        void SaveChanges();
        IQueryable<T> ReadFromStoredProcedure(string sqlQuery, SqlParameter[] parameters);
        void Attach(T entity);
    }
}
