using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Models.DBObjects;

namespace clubmembership.Repository
{
    public class MembershipTypeRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public MembershipTypeRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public MembershipTypeRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private MembershipTypeModel MapDBObjectToModel(MembershipType dbObject)
        {
            var model = new MembershipTypeModel();
            if(dbObject != null)
            {
                model.IdmembershipType = dbObject.IdmembershipType;
                model.Name = dbObject.Name;
                model.Description = dbObject.Description;
                model.SubscriptionLengthInMonths = dbObject.SubscriptionLengthInMonths;
            }
            return model;
        }

        private MembershipType MapModelToDBObject(MembershipTypeModel model)
        {
            var dbObject = new MembershipType();
            if(dbObject != null)
            {
                dbObject.IdmembershipType = model.IdmembershipType;
                dbObject.Name = model.Name;
                dbObject.Description = model.Description;
                dbObject.SubscriptionLengthInMonths = model.SubscriptionLengthInMonths;
            }
            return dbObject;
        }

        public List<MembershipTypeModel> GetAllMembershipTypes()
        {
            var list = new List<MembershipTypeModel>();
            foreach(var dbObject in _DBContext.MembershipTypes)
            {
                list.Add(MapDBObjectToModel(dbObject)); 
            }
            return list;
        }

        public MembershipTypeModel GetMembershipTypeById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.MembershipTypes.FirstOrDefault(x => x.IdmembershipType == id));
        }

        public void InsertMembershipType(MembershipTypeModel model)
        {
            model.IdmembershipType = Guid.NewGuid();
            _DBContext.MembershipTypes.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateMembershipType(MembershipTypeModel model)
        {
            var dbObject = _DBContext.MembershipTypes.FirstOrDefault(x => x.IdmembershipType == model.IdmembershipType);
            if (dbObject != null)
            {
                dbObject.IdmembershipType = model.IdmembershipType;
                dbObject.Name = model.Name;
                dbObject.Description = model.Description;
                dbObject.SubscriptionLengthInMonths = model.SubscriptionLengthInMonths;
                _DBContext.SaveChanges();
            }
        }

        public void DeleteMembershipType(Guid id)
        {
            //cascade delete
            var dbObject = _DBContext.MembershipTypes.FirstOrDefault(x => x.IdmembershipType == id);
            if (dbObject != null)
            {
                var memberships = _DBContext.Memberships.Where(x => x.IdmembershipType == dbObject.IdmembershipType); 
                foreach(var membership in memberships)
                {
                    _DBContext.Memberships.Remove(membership);
                }
                _DBContext.MembershipTypes.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }
}
