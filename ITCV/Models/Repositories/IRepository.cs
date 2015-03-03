using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITCV.Models.Views;
using ITCV.Models.Entities;
using System.Data.Entity;

namespace ITCV.Models.Repositories
{
    interface IRepository<T>
    {
        List<T> All();

        T Find(int id);

        void Insert(CV cv);

        void Remove();
        
    }

    public class CVRepository : TransientEntity, IRepository<CV>
    {
        
        CVContext ctx = new CVContext();
        public CVRepository()
        {
            Database.SetInitializer<CVContext>(new DropCreateDatabaseIfModelChanges<CVContext>());
        }

        public List<CV> All()
        {
            CVEntity e = ctx.CVs.First();
            CV c = (CV)Transit(e, null, Transient.ToView);
            return ctx.CVs.ToList().Select(m => (CV)Transit(m, null, Transient.ToView)).ToList();
        }

        public void Insert(CV cv)
        {
            
            CVEntity entityToInsert = (CVEntity)Transit(cv, null, Transient.ToEntity);
            try
            {
                ctx.CVs.Add(entityToInsert);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }


        public CV Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
