public class ActivityRepository : ControllerBase,IActivityRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public ActivityRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public  async Task<ActionResult>  ChangeVisibilityOfActivity(int Id)
    {
        Activity activity = await _BaseDBContext.Activities.FindAsync(Id);
        activity.Visibility = false;
        await _BaseDBContext.SaveChangesAsync();
        return Ok();
    }

    public  async Task <Activity> CreateActivity(CreateActivityDTO ActivityDTO)
    {
        Activity activity = new Activity(ActivityDTO);
        await _BaseDBContext.Activities.AddAsync(activity);
        await _BaseDBContext.SaveChangesAsync();
        return activity;                                                         //await GetActivityById(activity.Id);
    }

    public  async Task<ActionResult>  DeleteActivity(int Id)
    {
        List<Ranking> ranking = _BaseDBContext.Rankings.Where(x=>x.ActivityId == Id).ToList();
        foreach(var item in ranking)
        {
            _BaseDBContext.Rankings.Remove(ranking.FirstOrDefault());
            await _BaseDBContext.SaveChangesAsync();
        }
        /*
        List<UserActivityTimeTable> userActivityTimeTables = _BaseDBContext.UserActivityTimeTables
        .Where(x=>x.ActivityTimeTable.ActivityId == Id).ToList();
        foreach(var item in userActivityTimeTables)
        {
            _BaseDBContext.UserActivityTimeTables.Remove(userActivityTimeTables.FirstOrDefault());
            await _BaseDBContext.SaveChangesAsync();
        }
        */
        List<ActivityTimeTable> timeTables = _BaseDBContext.ActivityTimeTables.Include(x=>x.AllAttendantUsers).Where(x=>x.ActivityId == Id).ToList();
        foreach(ActivityTimeTable item in timeTables)
        {
            foreach(User user in item.AllAttendantUsers)
            {
                 user.RecordedActivities.Remove(timeTables.FirstOrDefault());
            }
            await _BaseDBContext.SaveChangesAsync();
            _BaseDBContext.ActivityTimeTables.Remove(timeTables.FirstOrDefault());
            await _BaseDBContext.SaveChangesAsync();
        }
        List<WishedActivity> wisheds = _BaseDBContext.WishedActivites.Where(x=>x.ActivityId == Id).ToList();
        foreach(var item in wisheds)
        {
            _BaseDBContext.WishedActivites.Remove(wisheds.FirstOrDefault());
            await _BaseDBContext.SaveChangesAsync();
        }
        Activity Activity = await _BaseDBContext.Activities.FirstOrDefaultAsync(x=>x.Id == Id);
        _BaseDBContext.Activities.Remove(Activity);
        await _BaseDBContext.SaveChangesAsync();
        
        return Ok();
    }

    public  async Task <IEnumerable<Activity>> GetActivitiesByCategoryNameOrderByCreatedTime(string CategoryName)
    {
         return await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      //sor join ctg in _BaseDBContext.Categories on subctg.CategoryId equals ctg.Id
                      where x.SubCategory.Category.Name == CategoryName && x.Visibility == true
                      orderby x.CreatedTime descending
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                           TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).ToListAsync();
    }

    public  async Task <IEnumerable<Activity>> GetActivitiesByNeighbourhood(Neighbourhood Neighbourhood)
    {
          return await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      where x.Address.NeighbourhoodId == Neighbourhood.Id && x.Visibility == true
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                           TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).ToListAsync();
    }

    public  async Task <IEnumerable<Activity>> GetActivitiesByOwnerUsername(string Username)
    {
          return await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      where x.OwnerUser.Username == Username && x.Visibility == true
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                           TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).ToListAsync();
    }

    public  async Task <IEnumerable<Activity>> GetActivitiesByPriceLimits(double minPrice, double maxPrice)
    {
          return await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      where x.Price <= maxPrice && x.Price >= minPrice && x.Visibility == true
                      orderby x.Price descending
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                          TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).ToListAsync();
    }

    public  async Task <IEnumerable<Activity>> GetActivitiesBySubCategoryNameOrderByCreatedTime(string SubCategoryName)
    {
         return await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      where x.SubCategory.Name == SubCategoryName && x.Visibility == true
                      orderby x.CreatedTime descending
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                           TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).ToListAsync();
    }

    public  async Task <Activity> GetActivityById(int Id)
    {
         Activity? Activity = await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      where x.Id == Id 
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                          TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).FirstOrDefaultAsync();
        return Activity;
    }

    public  async Task <Activity> GetActivityByName(string Name)
    {
        var Activity = await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      where x.Name == Name && x.Visibility == true
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                          TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).FirstOrDefaultAsync();
        return Activity;
    }

    public  async Task <IEnumerable<Activity>> GetAllActivitiesOrderByCreatedTime()
    {
        return await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      where x.Visibility == true
                      orderby x.CreatedTime descending
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                          TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).ToListAsync();
    }

    public  async Task <Activity> UpdateActivity(UpdateActivityDTO ActivityDto)
    {
        Activity activity = await _BaseDBContext.Activities.FirstOrDefaultAsync(x=>x.Id == ActivityDto.Id);
        activity.Name = ActivityDto.Name;
        activity.Description = ActivityDto.Description;
        activity.Image = ActivityDto.Image;
        activity.Visibility = ActivityDto.Visibility;
        activity.Price = ActivityDto.Price;
        activity.SubCategoryId = ActivityDto.SubCategoryId;
        /*if( _BaseDBContext.Addresses.Any(x=>x.Id == ActivityDto.AddressId ))*/ activity.AddressId = ActivityDto.AddressId;
        await _BaseDBContext.SaveChangesAsync();
        return await GetActivityById(activity.Id);
    }
    public async Task <IEnumerable<Activity>> GetAllActivitiesByPointLimit(double minValue)
    {
        return await (
                      from x in _BaseDBContext.Activities
                      join address in _BaseDBContext.Addresses on x.AddressId equals address.Id
                      join user in _BaseDBContext.Users on x.OwnerUserId equals user.Id
                      join subctg in _BaseDBContext.SubCategories on x.SubCategoryId equals subctg.Id
                      where x.Visibility == true
                      where x.Point >= minValue
                      orderby x.Point descending
                      select new Activity()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Image = x.Image,
                          Visibility = x.Visibility,
                          CreatedTime = x.CreatedTime,
                          Price = x.Price, Point = x.Point,
                          SubCategoryId = x.SubCategoryId,
                          AddressId = x.AddressId,
                          OwnerUserId = x.OwnerUserId,
                          Address = new Address()
                          {
                              Name = x.Address.Name,
                              OpenAddress1 = x.Address.OpenAddress1,
                              OpenAddress2 = x.Address.OpenAddress2,
                              Neighbourhood = new Neighbourhood()
                              {
                                  Name = x.Address.Neighbourhood.Name,
                                  District = new District()
                                  {
                                      Name = x.Address.Neighbourhood.District.Name,
                                      City = new City()
                                      {
                                          Name = x.Address.Neighbourhood.District.City.Name,
                                          State = new State()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.State.Name
                                          },
                                          Country = new Country()
                                          {
                                              Name = x.Address.Neighbourhood.District.City.Country.Name
                                          }
                                      }
                                  }
                              }
                          },
                          SubCategory = new SubCategory()
                          {
                              Id = x.SubCategory.Id,
                              Name = x.SubCategory.Name
                          },
                          OwnerUser = new User()
                          {
                              Username = x.OwnerUser.Username,
                              FirstName = x.OwnerUser.FirstName,
                              LastName = x.OwnerUser.LastName,
                              ProfilePhoto = x.OwnerUser.ProfilePhoto,
                              PhoneNumber = x.OwnerUser.PhoneNumber
                          },
                          TimeTable = (from tt in _BaseDBContext.ActivityTimeTables
                                       where tt.ActivityId == x.Id
                                       select tt).ToList()
                      }
        ).ToListAsync();
    }

    public bool IsAllGivenIDsCorrect(int? AddressId, int? SubCategoryId, int userId)
    {
        Address? address = _BaseDBContext.Addresses.FirstOrDefault(x=>x.Id == AddressId);
        SubCategory? subCategory = _BaseDBContext.SubCategories.FirstOrDefault(x=>x.Id == SubCategoryId);
        User? user = null;
        if (userId != 0)
        {
            user = _BaseDBContext.Users.FirstOrDefault(x => x.Id == userId);
        }
        else
        {
            user = _BaseDBContext.Users.FirstOrDefault();
        }
        
        if(address != null && subCategory != null && user != null)
        {
            return true;
        }
        return false;
    }
}