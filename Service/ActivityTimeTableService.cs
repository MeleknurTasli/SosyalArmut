public class ActivityTimeTableService : ControllerBase , IActivityTimeTableService
{
    private readonly IActivityTimeTableRepository _ActivityTimeTableRepository;
    public ActivityTimeTableService(IActivityTimeTableRepository activityTimeTableRepository)
    {
        _ActivityTimeTableRepository = activityTimeTableRepository;
    }

    public async Task<ActionResult<ActivityTimeTable>> CreateActivityTimeTable(CreateUpdateActivityTimeTableDTO createActivityTimeTableDTO)
    {
        try
        {
            if (await _ActivityTimeTableRepository.IsActivityExist(createActivityTimeTableDTO.ActivityId))
            {
                return Ok(await _ActivityTimeTableRepository.CreateActivityTimeTable(createActivityTimeTableDTO));
            }
            else return BadRequest("Hata : Aktivite mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<ActivityTimeTable>> UpdateActivityTimeTable(int Id, CreateUpdateActivityTimeTableDTO updateActivityTimeTableDTO)
    {
        try
        {
            if (await _ActivityTimeTableRepository.IsActivityExist(updateActivityTimeTableDTO.ActivityId)
                && await _ActivityTimeTableRepository.GetActivityTimeTableById(Id) != null)
            {
                return Ok(await _ActivityTimeTableRepository.UpdateActivityTimeTable(Id, updateActivityTimeTableDTO));
            }
            else return BadRequest("Hata : Aktivite ya da activite zaman tablosu mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteActivityTimeTable(int activityTimeTableId)
    {
        try
        {
            if (await _ActivityTimeTableRepository.GetActivityTimeTableById(activityTimeTableId) != null)
            {
                await _ActivityTimeTableRepository.DeleteActivityTimeTable(activityTimeTableId);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActivityTimeTable> GetActivityTimeTableById(int timeTableId)
    {
        try
        {
            return await _ActivityTimeTableRepository.GetActivityTimeTableById(timeTableId);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityId(int ActivityId)
    {
        try
        {
            return await _ActivityTimeTableRepository.GetActivityTimeTablesByActivityId(ActivityId);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityIdNotFullAndIsUpcoming(int ActivityId)
    {
        try
        {
            return await _ActivityTimeTableRepository.GetActivityTimeTablesByActivityIdNotFullAndIsUpcoming(ActivityId);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesBySubCategoryNameNotFullAndIsUpcoming(string name)
    {
        try
        {
            return await _ActivityTimeTableRepository.GetActivityTimeTablesBySubCategoryNameNotFullAndIsUpcoming(name);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<ActivityTimeTable>> GetAllActivityTimeTables()
    {
        try
        {
            return await _ActivityTimeTableRepository.GetAllActivityTimeTables();
        }
        catch
        {
            throw;
        }
    }




    public async Task<ActionResult<ActivityTimeTable>> RecordUserToActivityTimeTable(string Username, int timeTableId)
    {
        try
        {
            if (await _ActivityTimeTableRepository.GetActivityTimeTableById(timeTableId) != null
                     && await _ActivityTimeTableRepository.IsUsernameExist(Username))
            {
                var activityTimeTable = _ActivityTimeTableRepository.RecordUserToActivityTimeTable(Username, timeTableId);
                if(activityTimeTable == null)  return BadRequest("Hata : Katılımcı sayısı kotaya eşittir.");
                return Ok(activityTimeTable);
            }
            else return BadRequest("Hata : Girilen aktivite zaman tablosu Id mevcut değil ya da kullanıcı adı mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<ActivityTimeTable>> UpdateIsUpcoming(int timeTableId)
    {
        try
        {
            if (await _ActivityTimeTableRepository.GetActivityTimeTableById(timeTableId) != null)
            {
                return _ActivityTimeTableRepository.UpdateIsUpcoming(timeTableId);
            }
            else return BadRequest("Hata : Girilen aktivite zaman tablosu Id mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteUserFromActivityTimeTable(string Username, int timeTableId)
    {
        try
        {
            if (await _ActivityTimeTableRepository.GetActivityTimeTableById(timeTableId) != null && 
                 await _ActivityTimeTableRepository.IsUsernameExist(Username))
            {
                _ActivityTimeTableRepository.DeleteUserFromActivityTimeTable(Username, timeTableId);
            }
            else BadRequest("Hata : Girilen aktivite zaman tablosu Id mevcut değil ya da kullanıcı adı mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<IEnumerable<ActivityTimeTable>>> GetPastRecordsByUserName(string Username)
    {
        try
        {
            if (await _ActivityTimeTableRepository.IsUsernameExist(Username))
                    return Ok(_ActivityTimeTableRepository.GetPastRecordsByUserName(Username));
            else return BadRequest("Girilen kullanıcı adı mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<ActivityTimeTable>> GetRecordByUserNameAndTimeTableId(string Username, int timeTableId)
    {
        try
        {
            if (await _ActivityTimeTableRepository.GetActivityTimeTableById(timeTableId) != null && 
                 await _ActivityTimeTableRepository.IsUsernameExist(Username))
            {
                return _ActivityTimeTableRepository.GetRecordByUserNameAndTimeTableId(Username, timeTableId);
            }
            else return BadRequest("Hata : Girilen aktivite zaman tablosu Id mevcut değil ya da kullanıcı adı mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<IEnumerable<ActivityTimeTable>>> GetUpcomingRecordsByUserName(string Username)
    {
        try
        {
            if (await _ActivityTimeTableRepository.IsUsernameExist(Username))
                    return Ok(_ActivityTimeTableRepository.GetUpcomingRecordsByUserName(Username));
            else return BadRequest("Girilen kullanıcı adı mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<IEnumerable<UserDTO>>> GetUsersWhoAttendedToActivity(int ActivityId)
    {
        List<List<UserDTO>> AllUserDTOs = new List<List<UserDTO>>();
        List<UserDTO> UserDTOs;
        IEnumerable<IEnumerable<User>> AllUsers = _ActivityTimeTableRepository.GetUsersWhoAttendedToActivity(ActivityId);
        foreach(var enumerable in AllUsers)
        {
            UserDTOs = new List<UserDTO>();
            foreach(User user in enumerable)
            {
                UserDTOs.Add(new UserDTO(user));
            }
            AllUserDTOs.Add(UserDTOs);
        }
        return AllUserDTOs;
    }

    public async Task<IEnumerable<IEnumerable<UserDTO>>> GetUsersWhoWillAttendToActivity(int ActivityId)
    {
        List<List<UserDTO>> AllUserDTOs = new List<List<UserDTO>>();
        List<UserDTO> UserDTOs;
        IEnumerable<IEnumerable<User>> AllUsers = _ActivityTimeTableRepository.GetUsersWhoWillAttendToActivity(ActivityId);
        foreach(var enumerable in AllUsers)
        {
            UserDTOs = new List<UserDTO>();
            foreach(User user in enumerable)
            {
                UserDTOs.Add(new UserDTO(user));
            }
            AllUserDTOs.Add(UserDTOs);
        }
        return AllUserDTOs;
    }
}