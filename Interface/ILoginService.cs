public interface ILoginService
{
    LoginResponseDTO Authenticate(LoginDTO model);
    Account FindAccountById(int id);
}