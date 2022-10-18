using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Models.DBObjects;

namespace clubmembership.Repository
{
    public class MembershipRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public MembershipRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public MembershipRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private MembershipModel MapDBObjectToModel(Membership dbObject)
        {
            var model = new MembershipModel();
            if (dbObject != null)
            {
                model.Idmembership = dbObject.Idmembership;
                model.Idmember = dbObject.Idmember;
                model.IdmembershipType = dbObject.IdmembershipType;
                model.StartDate = dbObject.StartDate;
                model.EndDate = dbObject.EndDate;
                model.Level = dbObject.Level;
            }
            return model;
        }

        private Membership MapModelToDBObject(MembershipModel model)
        {
            var dbObject = new Membership();
            if (dbObject != null)
            {
                dbObject.Idmembership = model.Idmembership;
                dbObject.Idmember = model.Idmember;
                dbObject.IdmembershipType = model.IdmembershipType;
                dbObject.StartDate = model.StartDate;
                dbObject.EndDate = model.EndDate;
                dbObject.Level = model.Level;
            }
            return dbObject;
        }

        public List<MembershipModel> GetAllMemberships()
        {
            var list = new List<MembershipModel>();
            foreach(var dbObject in _DBContext.Memberships)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }

        public MembershipModel GetMembershipById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Memberships.FirstOrDefault(x => x.Idmembership == id));
        }

        public void InsertMembership(MembershipModel model)
        {
            model.Idmembership = Guid.NewGuid();
            _DBContext.Memberships.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateMembership(MembershipModel model)
        {
            var dbObject = _DBContext.Memberships.FirstOrDefault(x => x.Idmembership == model.Idmembership);
            if (dbObject != null)
            {
                dbObject.Idmembership = model.Idmembership;
                dbObject.Idmember = model.Idmember;
                dbObject.IdmembershipType = model.IdmembershipType;
                dbObject.StartDate = model.StartDate;
                dbObject.EndDate = model.EndDate;
                dbObject.Level = model.Level;
                _DBContext.SaveChanges();
            }
        }

        public void DeleteMembership(MembershipModel model)
        {
            var dbObject = _DBContext.Memberships.FirstOrDefault(x => x.Idmembership == model.Idmembership);
            if (dbObject != null)
            {
                _DBContext.Memberships.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }

}
