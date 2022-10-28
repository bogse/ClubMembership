using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Models.DBObjects;
using System.Collections;
using System.Linq;

namespace clubmembership.Repository
{
    public class CodeSnippetRepository
    {
        private readonly ApplicationDbContext _DBContext;
        //private MemberRepository _memberRepository;
        public CodeSnippetRepository()
        {
            //_memberRepository = new MemberRepository();
            _DBContext = new ApplicationDbContext();
        }
        public CodeSnippetRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private CodeSnippetModel MapDBObjectToModel(CodeSnippet dbObject)
        {
            var model = new CodeSnippetModel();
            if(dbObject != null)
            {
                model.IdcodeSnippet = dbObject.IdcodeSnippet;
                model.Title = dbObject.Title;
                model.ContentCode = dbObject.ContentCode;
                model.Idmember = dbObject.Idmember;
                model.Revision = dbObject.Revision;
                model.IdsnippetPreviousVersion = dbObject.IdsnippetPreviousVersion;
                model.DateTimeAdded = dbObject.DateTimeAdded;
                model.IsPublished = dbObject.IsPublished;
            }
            return model;
        }

        private CodeSnippet MapModelToDBObject(CodeSnippetModel model)
        {
            var dbObject = new CodeSnippet();
            if(dbObject != null)
            {
                dbObject.IdcodeSnippet = model.IdcodeSnippet;
                dbObject.Title = model.Title;
                dbObject.ContentCode = model.ContentCode;
                dbObject.Idmember = model.Idmember;
                dbObject.Revision = model.Revision;
                dbObject.IdsnippetPreviousVersion = model.IdsnippetPreviousVersion;
                dbObject.DateTimeAdded = model.DateTimeAdded;
                dbObject.IsPublished = model.IsPublished;
            }
            return dbObject;
        }
        public List<CodeSnippetModel> GetAllCodeSnippets()
        {
            var list = new List<CodeSnippetModel>();
            foreach(var dbObject in _DBContext.CodeSnippets)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }

        public CodeSnippetModel GetCodeSnippetById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == id));
        }

        public CodeSnippetModel GetLatestCodeSnippet()
        {

            return MapDBObjectToModel(_DBContext.CodeSnippets.OrderByDescending(x => x.DateTimeAdded).FirstOrDefault()); //solutie Codrut
        }

        public void InsertCodeSnippet(CodeSnippetModel model)
        {
            model.IdcodeSnippet = Guid.NewGuid();
            model.DateTimeAdded = DateTime.Now;
            var dict = new SortedDictionary<DateTime, Guid>();
            foreach (var dbObject in _DBContext.CodeSnippets)
            {
               // if (dbObject != null)
               // {
                    MapDBObjectToModel(dbObject);
                    dict.Add(dbObject.DateTimeAdded, dbObject.IdcodeSnippet);
                    model.IdsnippetPreviousVersion = dict.Values.Last();
               // }
            }

            _DBContext.CodeSnippets.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdateCodeSnippet(CodeSnippetModel model)
        {
            var dbObject = _DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == model.IdcodeSnippet);
            if (dbObject != null)
            {
                dbObject.IdcodeSnippet = model.IdcodeSnippet;
                dbObject.Title = model.Title;
                dbObject.ContentCode = model.ContentCode;
                dbObject.Idmember = model.Idmember;
                dbObject.Revision = model.Revision;
                dbObject.IdsnippetPreviousVersion = model.IdsnippetPreviousVersion;
                dbObject.DateTimeAdded = model.DateTimeAdded;
                dbObject.IsPublished = model.IsPublished;
                _DBContext.SaveChanges();
            }
        }

        public void DeleteCodeSnippet(Guid id)
        {
            var dbObject = _DBContext.CodeSnippets.FirstOrDefault(x => x.IdcodeSnippet == id);
            if (dbObject != null)
            {
                _DBContext.CodeSnippets.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }
}
