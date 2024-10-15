using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Login.Helpers.Authentication.Authorization;
using Login.Helpers.Authentication.Security;
using Login.Helpers.TwoStepAuth;
using Login.Models.Dtos.Authentication;
using Login.Models.Dtos.User;
using Login.Models.Entities;
using Login.Models.ViewModels.Authentication;
using System.Security.Cryptography;
using Login.Services.Interfaces;
using Login.Repositories.Interfaces;

namespace Login.Helpers.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserService _userService; // Servicio para usuarios
    private readonly IRolService _rolService; // Servicio para roles
    private readonly IUser _userRepositoty;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IAuth _auth;

    public UserAuthenticationService(IPasswordHasher<User> passwordHasher, IUserService userService, IRolService rolService, IMapper mapper, ITokenService tokenService, IUser userRepositoty, IAuth auth)
    {
        _passwordHasher = passwordHasher;
        _userService = userService;
        _rolService = rolService;
        _mapper = mapper;
        _tokenService = tokenService;
        _userRepositoty = userRepositoty;
        _userRepositoty = userRepositoty;
        _auth = auth;
    }


    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            Email = registerDto.Email,
            Username = registerDto.Username,
            DateCreated = registerDto.DateCreated
        };

        user.Password = _passwordHasher.HashPassword(user, registerDto.Password);

        var existingUser = await _userService.GetByUsernameAsync(registerDto.Username);


        if (existingUser == null)
        {
            var rolDefaultVm = await _rolService.GetRolNameByNameAsync(Authorization.rol_default.ToString());
            var rolDefault = _mapper.Map<Rol>(rolDefaultVm);
                                              
            try
            {
                user.Rols.Add(rolDefault);
                var userDto = _mapper.Map<UserDto>(user);
                await _userService.AddAsync(userDto);

                return $"User  {registerDto.Username} has been registered successfully";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"User {registerDto.Username} already registered.";
        }
    }



    public async Task<DataUserDto> LoginAsync(LoginDto Dto)
    {
        DataUserDto dataUserDto = new DataUserDto();

        var userVm = await _userService.GetByUsernameAsync(Dto.Username);
        var userEntity = _mapper.Map<User>(userVm);

        if (userEntity == null)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"User does not exist with username: {Dto.Username}";
            return dataUserDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password!, Dto.Password);

        if (result == PasswordVerificationResult.Success)
        {

            byte[] QR = _auth.CreateQR(ref userEntity);
            var userDto = _mapper.Map<UserDto>(userEntity);
            await _userService.UpdateAsync(userDto, userEntity.Id);

            // Realizar la autenticación y obtener el token
            DataUserDto tokenData = await GetTokenAsync(Dto);

            // Envía el correo electrónico con el QR
            await _auth.SendEmail(userEntity, QR);

            return tokenData;
        }
        else
        {
            // Devuelve una respuesta indicando que la autenticación falló
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = "La contraseña es incorrecta.";
            return dataUserDto;
        }
    }




    public async Task<DataUserDto> GetTokenAsync(LoginDto model)
    {
        DataUserDto dataUserDto = new DataUserDto();

        var userVm = await _userService.GetByUsernameAsync(model.Username);
        var userEntity = _mapper.Map<User>(userVm);

        if (userEntity == null)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"User does not exist with username: {model.Username}";
            return dataUserDto;
        }

        if (userEntity.Password == null)
        {
            // Manejar el caso donde la contraseña no está disponible
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = "Password is not set.";
            return dataUserDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            dataUserDto.Message = "EL CODIGO QR SE HA ENVIADO AL EMAIL!! <3";
            dataUserDto.IsAuthenticated = true;
            dataUserDto.Token = _tokenService.GenerateJwtToken(userEntity);
            dataUserDto.Email = userEntity.Email!;
            dataUserDto.UserName = userEntity.Username!;
            dataUserDto.Roles = userEntity.Rols
                                            .Select(u => u.Nombre)
                                            .ToList();

            if (userEntity.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = userEntity.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                if (activeRefreshToken != null)
                {
                    dataUserDto.RefreshToken = activeRefreshToken.Token;
                    dataUserDto.RefreshTokenExpiration = activeRefreshToken.Expires;
                }
            }

            else
            {
                var refreshToken = CreateRefreshToken();
                dataUserDto.RefreshToken = refreshToken.Token;
                dataUserDto.RefreshTokenExpiration = refreshToken.Expires;
                userEntity.RefreshTokens.Add(refreshToken);

                var userDto = _mapper.Map<UserDto>(userEntity);
                await _userService.UpdateAsync(userDto, userEntity.Id);
            }

            return dataUserDto;
        }

        dataUserDto.IsAuthenticated = false;
        dataUserDto.Message = $"Credenciales incorrectas para el usuario {userEntity.Username}.";
        return dataUserDto;
    }


    private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }
    }





    public async Task<string> AddRoleAsync(AddRoleDto model)
    {
        if (string.IsNullOrEmpty(model.Username))
        {
            throw new ArgumentNullException(nameof(model.Username), "Username cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(model.Password))
        {
            throw new ArgumentNullException(nameof(model.Username), "Username cannot be null or empty.");
        }

        var userVm = await _userService.GetByUsernameAsync(model.Username);
        var userEntity = _mapper.Map<User>(userVm);

        if (userEntity == null)
        {
            return $"User {model.Username} does not exists.";
        }

        if (string.IsNullOrEmpty(userEntity.Password))
        {
            return $"Password for user {model.Username} is not set.";
        }


        var result = _passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            var rolVm = _rolService.GetRolNameByNameAsync(model.Username);
            var rol = _mapper.Map<Rol>(rolVm);

            if (rol != null)
            {
                var userHasRole = userEntity.Rols
                                            .Any(u => u.Id == rol.Id);

                if (userHasRole == false)
                {
                    userEntity.Rols.Add(rol);
                    var userDto = _mapper.Map<UserDto>(userEntity);
                    await _userService.UpdateAsync(userDto, userVm.Id);
                }

                return $"Role {model.Role} added to user {model.Username} successfully.";
            }

            return $"Role {model.Role} was not found.";
        }
        return $"Invalid Credentials";
    }

    public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var dataUserDto = new DataUserDto();

        var usuario = await _userRepositoty.GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Token is not assigned to any user.";
            return dataUserDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Token is not active.";
            return dataUserDto;
        }
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        var user = _userRepositoty.UpdateAsync(usuario);
        
        //Generate a new Json Web Token
        dataUserDto.IsAuthenticated = true;
        dataUserDto.Token = _tokenService.GenerateJwtToken(usuario);
        dataUserDto.Email = usuario.Email;
        dataUserDto.UserName = usuario.Username;
        dataUserDto.Roles = usuario.Rols
                                        .Select(u => u.Nombre)
                                        .ToList();
        dataUserDto.RefreshToken = newRefreshToken.Token;
        dataUserDto.RefreshTokenExpiration = newRefreshToken.Expires;
        return dataUserDto;
    }

    public async Task<bool> VerifyAsync(AuthDto dto)
    {
        try
        {
            User? user = await _userRepositoty.GetByUsernameAsync(dto.Username!);

            if (user!.TwoStepSecret == null)
            {
                throw new ArgumentNullException(nameof(user.TwoStepSecret), "Two-step secret is null");
            }

            var IsVerify = _auth.VerifyCode(user.TwoStepSecret, dto.Code!);

            return IsVerify;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error verifying code for user {dto.Username}", ex);
        }

    }
}
