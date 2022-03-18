public class ActivityService : ControllerBase, IActivityService
{
    private readonly IActivityRepository _ActivityRepository;
    public ActivityService(IActivityRepository ActivityRepository)
    {
        _ActivityRepository = ActivityRepository;
    }

    public async Task<ActionResult> ChangeVisibilityOfActivity(int Id)
    {
        try
        {
            Activity FoundActivity = await _ActivityRepository.GetActivityById(Id);
            if (FoundActivity != null)
            {
                return await _ActivityRepository.ChangeVisibilityOfActivity(Id);
            }
            return  BadRequest("Bu id ile aktivite mevcut değildir.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<UpdateActivityDTO>> UpdateActivity(UpdateActivityDTO Activity)
    {
        try
        {
            Activity FoundActivity = await _ActivityRepository.GetActivityById(Activity.Id);
            if (FoundActivity != null && _ActivityRepository.IsAllGivenIDsCorrect(Activity.AddressId, Activity.SubCategoryId, 0))
            {
                FoundActivity = await _ActivityRepository.UpdateActivity(Activity);
                return Ok(new UpdateActivityDTO(FoundActivity));
            }
            else return BadRequest("Hata : Aktivite bulunamadı.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<ActivityDTO>> CreateActivity(CreateActivityDTO Activity)
    {
        try
        {
            //Activity FoundActivity = await _ActivityRepository.GetActivityById(Activity.Id);
            if(_ActivityRepository.IsAllGivenIDsCorrect(Activity.AddressId, Activity.SubCategoryId, Activity.OwnerUserId))   //if (FoundActivity == null)
            {
                return Ok(new ActivityDTO(await _ActivityRepository.CreateActivity(Activity)));
            }
            else return BadRequest("Hata : Girilen Id'leri kontrol ediniz.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult> DeleteActivity(int Id)
    {
        try
        {
            Activity FoundActivity = await _ActivityRepository.GetActivityById(Id);
            if (FoundActivity != null)
            {
                return await _ActivityRepository.DeleteActivity(Id);
            }
            else return BadRequest("Bu id ile aktivite mevcut değildir.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityDTO>> GetActivitiesByCategoryNameOrderByCreatedTime(string CategoryName)
    {
        try
        {
            return ConvertToActivityDTO(await _ActivityRepository.GetActivitiesByCategoryNameOrderByCreatedTime(CategoryName));
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityDTO>> GetActivitiesByNeighbourhood(Neighbourhood Neighbourhood)
    {
        try
        {
            return ConvertToActivityDTO(await _ActivityRepository.GetActivitiesByNeighbourhood(Neighbourhood));
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityDTO>> GetActivitiesByOwnerUsername(string Username)
    {
        try
        {
            return ConvertToActivityDTO(await _ActivityRepository.GetActivitiesByOwnerUsername(Username));
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetActivitiesByPriceLimits(string minPrice, string maxPrice)
    {
        try
        {
            double _minPrice, _maxPrice;
            bool min = double.TryParse(minPrice, out _minPrice);
            bool max = double.TryParse(maxPrice, out _maxPrice);
            if(min && max)
            {
                return Ok(ConvertToActivityDTO(await _ActivityRepository.GetActivitiesByPriceLimits(_minPrice, _maxPrice)));
            }
            return BadRequest("Hata : Girilen değerler double olmalıdır.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityDTO>> GetActivitiesBySubCategoryNameOrderByCreatedTime(string SubCategoryName)
    {
        try
        {
            return ConvertToActivityDTO(await _ActivityRepository.GetActivitiesBySubCategoryNameOrderByCreatedTime(SubCategoryName));
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<ActivityDTO>> GetActivityById(int Id)
    {
        try
        {
            var activity = await _ActivityRepository.GetActivityById(Id);
            if(activity == null) return BadRequest("Mevcut değil.");
            return new ActivityDTO(activity);
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<ActivityDTO>> GetActivityByName(string Name)
    {
        try
        {
            var activity = await _ActivityRepository.GetActivityByName(Name);
            if(activity == null) return BadRequest("Mevcut değil.");
            return new ActivityDTO(activity);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityDTO>> GetAllActivitiesOrderByCreatedTime()
    {
        try
        {
            return ConvertToActivityDTO(await _ActivityRepository.GetAllActivitiesOrderByCreatedTime());
        }
        catch
        {
            throw;
        }
    }

    private List<ActivityDTO> ConvertToActivityDTO(IEnumerable<Activity> Activities)
    {
        List<ActivityDTO> activityDTOs = new List<ActivityDTO>();
        foreach(Activity activity in Activities)
        {
            activityDTOs.Add(new ActivityDTO(activity));
        }
        return activityDTOs;
    }

    public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetAllActivitiesByPointLimit(string minValue)
    {
        try
        {
            double _minValue;;
            bool isDouble = double.TryParse(minValue, out _minValue);
            if(isDouble)
            {
                return Ok(ConvertToActivityDTO(await _ActivityRepository.GetAllActivitiesByPointLimit(_minValue)));
            }
            return BadRequest("Hata : Girilen limit double olmalıdır.");
        }
        catch
        {
            throw;
        }
    }


}