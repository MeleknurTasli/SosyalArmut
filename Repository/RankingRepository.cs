public class RankingRepository : IRankingRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public RankingRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    private void ActivityPointUpdate(Activity _Activity, Ranking ranking)
    {
        double? toplampuan = 0;
        if (_Activity.Point == null)
        {
            toplampuan = ranking.Value;
        }
        else if(_Activity.Point == 0)
        {
            var ActivityRankings = _BaseDBContext.Rankings.Where(x => x.ActivityId == ranking.ActivityId).ToList();
            if (ActivityRankings.Count == 0) toplampuan = ranking.Value;
            else
            {
                toplampuan = ranking.Value / (double)(ActivityRankings.Count + 1);
            }
        }
        else
        {
            var ActivityRankings = _BaseDBContext.Rankings.Where(x => x.ActivityId == ranking.ActivityId).ToList();
            toplampuan =  _Activity.Point * (double)ActivityRankings.Count;
            toplampuan = (toplampuan + ranking.Value) / (double)(ActivityRankings.Count + 1);
        }
         var Activity = _BaseDBContext.Activities.FirstOrDefault(x => x.Id == ranking.ActivityId);
        Activity.Point = toplampuan;
        _BaseDBContext.SaveChanges();
    }

    public Ranking CreateRanking(Ranking ranking)
    {
        Activity _Activity = _BaseDBContext.Activities.Where(x => x.Id == ranking.ActivityId).FirstOrDefault();
        User _User = _BaseDBContext.Users.Where(x => x.Id == ranking.RatingUserId).FirstOrDefault();
        if (_Activity != null && _User != null)
        {
            ActivityPointUpdate(_Activity, ranking);
            _BaseDBContext.Rankings.Add(ranking);
            _BaseDBContext.SaveChanges();
            return ranking;
        }
        return null;
    }

    public void DeleteteRanking(int Id)
    {
        Ranking ranking = _BaseDBContext.Rankings.FirstOrDefault(x => x.Id == Id);
        Activity _Activity = _BaseDBContext.Activities.Where(x => x.Id == ranking.ActivityId).FirstOrDefault();
        var ActivityRankings = _BaseDBContext.Rankings.Where(x => x.ActivityId == ranking.ActivityId).ToList();
        if (ranking != null && _Activity != null)
        {
            if (ActivityRankings.Count != 1)
            {
                double? toplampuan = _Activity.Point * (double)ActivityRankings.Count;
                toplampuan = (toplampuan - ranking.Value) / (double)(ActivityRankings.Count - 1);
                var Activity = _BaseDBContext.Activities.FirstOrDefault(x => x.Id == ranking.ActivityId);
                Activity.Point = toplampuan;
            }
            else
            {
                var Activity = _BaseDBContext.Activities.FirstOrDefault(x => x.Id == ranking.ActivityId);
                Activity.Point = 0;
            }
        }
        _BaseDBContext.SaveChanges();
        _BaseDBContext.Rankings.Remove(ranking);
        _BaseDBContext.SaveChanges();
    }

    public IEnumerable<Ranking> GetAllRankings()
    {
        return _BaseDBContext.Rankings.Where(x => x.Activity.Visibility == true
                                              && x.RatingUser.Account.Visibility == true).ToList();
    }

    public Ranking GetRankingOfActivityByUsername(int ActivityId, string userName)
    {
        Ranking ranking = (from x in _BaseDBContext.Rankings
                           where x.ActivityId == ActivityId && x.RatingUser.Username == userName
                            && x.Activity.Visibility == true && x.RatingUser.Account.Visibility == true
                           select x
         ).FirstOrDefault();
        return ranking;
    }

    public IEnumerable<Ranking> GetRankingsByUsername(string userName)
    {
        IEnumerable<Ranking> rankings = (from x in _BaseDBContext.Rankings
                                             //join a in _BaseDBContext.Activities on x.ActivityId equals a.Id
                                             //join u in _BaseDBContext.Users on x.RatingUserId equals u.Id
                                         where x.RatingUser.Username == userName
                                          && x.Activity.Visibility == true && x.RatingUser.Account.Visibility == true
                                         select new Ranking()
                                         {
                                             Id = x.Id,
                                             Value = x.Value,
                                             Activity = new Activity()
                                             {
                                                 Id = x.ActivityId,
                                                 Name = x.Activity.Name,
                                                 Price = x.Activity.Price,
                                                 SubCategory = new SubCategory(){Name = x.Activity.SubCategory.Name,
                                                 Category = new Category(){Name = x.Activity.SubCategory.Category.Name}},
                                             },
                                             RatingUser = new User()
                                             {
                                                 Username = x.RatingUser.Username
                                             }
                                         }
        ).ToList();
        return rankings;
    }

    public IEnumerable<Ranking> GetRankingsOfAnActivity(int ActivityId)
    {/*
         IEnumerable<Ranking> rankings = (from x in _BaseDBContext.Rankings
                                         // join a in _BaseDBContext.Activities on x.ActivityId equals a.Id
                                         //join u in _BaseDBContext.Users on x.RatingUserId equals u.Id
                                         where x.ActivityId == ActivityId
                                          && x.Activity.Visibility == true && x.RatingUser.Account.Visibility == true
                                         select new Ranking()
                                         {
                                             Id = x.Id,
                                             Value = x.Value,
                                             Activity = new Activity()
                                             {
                                                 Id = x.ActivityId,
                                                 Name = x.Activity.Name,
                                                 SubCategory = new SubCategory() {Name = x.Activity.SubCategory.Name,
                                                 Category = new Category() {Name = x.Activity.SubCategory.Category.Name}},
                                                 Price = x.Activity.Price
                                             },
                                             RatingUser = new User() 
                                             {
                                                 Username = x.RatingUser.Username
                                             }
                                         }
        ).ToList();*/
        var rankings = _BaseDBContext.Rankings.Where(x => x.ActivityId == ActivityId
                                          && x.Activity.Visibility == true && x.RatingUser.Account.Visibility == true).ToList();
        return rankings;
    }

    public Ranking UpdateRanking(UpdateRankingDTO Ranking)
    {
        Ranking ranking = _BaseDBContext.Rankings.FirstOrDefault(x => x.Id == Ranking.Id);
        Activity _Activity = _BaseDBContext.Activities.Where(x => x.Id == ranking.ActivityId).FirstOrDefault();
        User _User = _BaseDBContext.Users.Where(x => x.Id == ranking.RatingUserId).FirstOrDefault();
        if (_Activity != null && ranking != null && _User != null)
        {
            ActivityPointUpdateOnUpdating(_Activity, ranking, Ranking.Value);
            ranking.Value = Ranking.Value;
            _BaseDBContext.SaveChanges();
            return ranking;
        }
        return null;
    }

    public void ActivityPointUpdateOnUpdating(Activity _Activity, Ranking ranking, double? newValue)
    {
        double? toplampuan = 0;
        var ActivityRankings = _BaseDBContext.Rankings.Where(x => x.ActivityId == ranking.ActivityId).ToList();
        if (_Activity.Point == 0)
        {
            if (ActivityRankings.Count == 1) toplampuan = newValue;
            else
            {
                toplampuan = newValue / (double)(ActivityRankings.Count);
            }
        }
        else
        {
            double? oldValue = (from x in _BaseDBContext.Rankings where x.Id == ranking.Id select x.Value).FirstOrDefault();
            toplampuan = _Activity.Point * (double)ActivityRankings.Count;
            toplampuan = (toplampuan + newValue - oldValue) / (double)(ActivityRankings.Count);
        }
        var Activity = _BaseDBContext.Activities.FirstOrDefault(x => x.Id == ranking.ActivityId);
        Activity.Point = toplampuan;
        _BaseDBContext.SaveChanges();
    }


    public Ranking GetRankingById(int Id)
    {
        return _BaseDBContext.Rankings.Where(x => x.Id == Id).FirstOrDefault();
    }
}

