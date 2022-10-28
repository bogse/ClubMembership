using clubmembership.Models;
using clubmembership.Models.DBObjects;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace clubmembership.Data;

public class AnnouncementRepository
{
    private readonly ApplicationDbContext _DBContext;
    public AnnouncementRepository()
    {
        _DBContext = new ApplicationDbContext();
    }
    public AnnouncementRepository(ApplicationDbContext dbContext)
    {
        _DBContext = dbContext;
    }

    private AnnouncementModel MapDBObjectToModel(Announcement dbObject)
    {
        var model = new AnnouncementModel();
        if(dbObject != null)
        {
            model.Idannouncemment = dbObject.Idannouncemment;
            model.ValidFrom = dbObject.ValidFrom;
            model.ValidTo = dbObject.ValidTo;
            model.Title = dbObject.Title;
            model.Text = dbObject.Text;
            model.EventDateTime = dbObject.EventDateTime;
            model.Tags = dbObject.Tags;
        }
        return model;
    }

    private Announcement MapModelToDBObject(AnnouncementModel model)
    {
        var dbObject = new Announcement();
        if(dbObject != null)
        {
            dbObject.Idannouncemment = model.Idannouncemment;
            dbObject.ValidFrom = model.ValidFrom;
            dbObject.ValidTo = model.ValidTo;
            dbObject.Title = model.Title;
            dbObject.Text = model.Text;
            dbObject.EventDateTime = model.EventDateTime;
            dbObject.Tags = model.Tags;
        }
        return dbObject;
    }

    public List<AnnouncementModel> GetAllAnnouncements()
    {
        var list = new List<AnnouncementModel>();
        foreach(var dbObject in _DBContext.Announcements)
        {
            list.Add(MapDBObjectToModel(dbObject));
        }
        return list;
    }
    public AnnouncementModel GetAnnouncementById(Guid id)
    {
        return MapDBObjectToModel(_DBContext.Announcements.FirstOrDefault(x => x.Idannouncemment == id));
    }

    public void InsertAnnouncement(AnnouncementModel model)
    {
        model.Idannouncemment = Guid.NewGuid();
        _DBContext.Announcements.Add(MapModelToDBObject(model));
        _DBContext.SaveChanges();
    }

    public void UpdateAnnouncement(AnnouncementModel model)
    {
        var dbObject = _DBContext.Announcements.FirstOrDefault(x => x.Idannouncemment == model.Idannouncemment);
        if (dbObject != null)
        {
            dbObject.Idannouncemment = model.Idannouncemment;
            dbObject.ValidFrom = model.ValidFrom;
            dbObject.ValidTo = model.ValidTo;
            dbObject.Title = model.Title;
            dbObject.Text = model.Text;
            dbObject.EventDateTime = model.EventDateTime;
            dbObject.Tags = model.Tags;
            _DBContext.SaveChanges();
        }
    }

    public void DeleteAnnouncement(Guid id) // era AnnouncementeModel inainte de controller
    {
        var dbObject = _DBContext.Announcements.FirstOrDefault(x => x.Idannouncemment == id);
        if (dbObject != null)
        {
            _DBContext.Announcements.Remove(dbObject);
            _DBContext.SaveChanges();
        }
    }
}
