using Booking.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Services.Contract
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);

    }
}
