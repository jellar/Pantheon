using System.Threading.Tasks;
using PantheonTest.Application.Models.Authentication;

namespace PantheonTest.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        // Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
