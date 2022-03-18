public class WishedActivityService : ControllerBase, IWishedActivityService
{
    private readonly IWishedActivityRepository _wishedActivityRepository;
    public WishedActivityService(IWishedActivityRepository wishedActivityRepository)
    {
        this._wishedActivityRepository = wishedActivityRepository;
    }

    public async Task<ActionResult<WishedActivityDTO>> CreateWishedActivity(WishedActivity wishedActivity)
    {
        try
        {
            string username = _wishedActivityRepository.getUserNameById(wishedActivity.UserId);
            if(username == null || !_wishedActivityRepository.IsExistsInActivities(wishedActivity.ActivityId)) 
                    return BadRequest("User Id veya activity Id mevcut değildir.");

            WishedActivity WishedActivity = await _wishedActivityRepository.
                GetWishedActivityByUsernameAndActivityId(username, wishedActivity.ActivityId);
            WishedActivity WishedActivityById = await _wishedActivityRepository.
                GetWishedActivityById(wishedActivity.Id);
            bool IsSelected = _wishedActivityRepository.
                IsExistsinUserActivityTimeTable(wishedActivity);

            if (WishedActivity == null && WishedActivityById == null && !IsSelected)
            {
                var createdWishedActivity = await _wishedActivityRepository.CreateWishedActivity(wishedActivity);
                if (createdWishedActivity != null)
                {
                    return new WishedActivityDTO(wishedActivity);
                }
                else
                {
                    return  BadRequest("User Id veya activity Id mevcut değildir.");
                }
            }
            return  BadRequest("Zaten mevcuttur.");
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteWishedActivity(string UserName, int ActivityId)
    {
        try
        {
            if (await _wishedActivityRepository.GetWishedActivityByUsernameAndActivityId(UserName, ActivityId) != null)
            {
                await _wishedActivityRepository.DeleteWishedActivity(UserName, ActivityId);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<WishedActivityDTO>> GetAllWishedActivities()
    {
        try
        {
            var wishedActivities = await _wishedActivityRepository.GetAllWishedActivities();
            return ConvertToWishedActivityDTO(wishedActivities);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<WishedActivityDTO>> GetWishedActivitiesByActivityId(int ActivityId)
    {
        try
        {
            var wishedActivities = await _wishedActivityRepository.GetWishedActivitiesByActivityId(ActivityId);
            return ConvertToWishedActivityDTO(wishedActivities);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<WishedActivityDTO>> GetWishedActivitiesByUserName(string Username)
    {
        try
        {
            var wishedActivities = await _wishedActivityRepository.GetWishedActivitiesByUserName(Username);
            return ConvertToWishedActivityDTO(wishedActivities);
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<WishedActivityDTO>> GetWishedActivityById(int Id)
    {
        try
        {
            var wishedActivity = await _wishedActivityRepository.GetWishedActivityById(Id);
            if (wishedActivity != null)
            {
                return new WishedActivityDTO(wishedActivity);
            }
            else
            {
                return BadRequest("Mevcut değildir.");
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<WishedActivityDTO>> GetWishedActivityByUsernameAndActivityId(string Username, int ActivityId)
    {
        try
        {
            var wishedActivity = await _wishedActivityRepository.GetWishedActivityByUsernameAndActivityId(Username, ActivityId);
            if (wishedActivity != null)
            {
                return new WishedActivityDTO(wishedActivity);
            }
            else
            {
                return BadRequest("Mevcut değildir.");
            }
        }
        catch
        {
            throw;
        }

    }

    private List<WishedActivityDTO> ConvertToWishedActivityDTO(IEnumerable<WishedActivity> WishedActivities)
    {
        List<WishedActivityDTO> WishedActivityDTOs = new List<WishedActivityDTO>();
        foreach (WishedActivity WishedActivity in WishedActivities)
        {
            WishedActivityDTOs.Add(new WishedActivityDTO(WishedActivity));
        }
        return WishedActivityDTOs;
    }
}