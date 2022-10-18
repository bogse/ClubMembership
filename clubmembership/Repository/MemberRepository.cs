using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Models.DBObjects;

namespace clubmembership.Repository
{
    public class MemberRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public MemberRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public MemberRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private MemberModel MapDBObjectToModel(Member dbObject)
        {
            var model = new MemberModel();
            if(dbObject != null)
            {
                model.Idmember = dbObject.Idmember;
                model.Name = dbObject.Name;
                model.Title = dbObject.Title;
                model.Position = dbObject.Position;
                model.Description = dbObject.Description;
                model.Resume = dbObject.Resume;
            }
            return model;
        }

        private Member MapModelToDBObject(MemberModel model)
        {
            var dbObject = new Member();
            if (dbObject != null)
            {
                dbObject.Idmember = model.Idmember;
                dbObject.Name = model.Name;
                dbObject.Title = model.Title;
                dbObject.Position = model.Position;
                dbObject.Description = model.Description;
                dbObject.Resume = model.Resume;
            }
            return dbObject;
        }

        public List<MemberModel> GetAllMembers()
        {
            var list = new List<MemberModel>();
            foreach(var dbObject in _DBContext.Members)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }

        public MemberModel GetMemberById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Members.FirstOrDefault(x => x.Idmember == id));
        }

        public void InsertMember(MemberModel model)
        {
            model.Idmember = Guid.NewGuid();
            _DBContext.Members.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateMember(MemberModel model)
        {
            var dbObject = _DBContext.Members.FirstOrDefault(x => x.Idmember == model.Idmember);
            if (dbObject != null)
            {
                dbObject.Idmember = model.Idmember;
                dbObject.Name = model.Name;
                dbObject.Title = model.Title;
                dbObject.Position = model.Position;
                dbObject.Description = model.Description;
                dbObject.Resume = model.Resume;
                _DBContext.SaveChanges();
            }
        }

        public void DeleteMember(MemberModel model)
        {
            var dbObject = _DBContext.Members.FirstOrDefault(x => x.Idmember == model.Idmember);
            if (dbObject != null)
            {
                _DBContext.Members.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }
}
