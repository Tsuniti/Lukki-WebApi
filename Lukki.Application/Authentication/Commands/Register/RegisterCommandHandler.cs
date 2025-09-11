// using ErrorOr;
// using Lukki.Application.Authentication.Common;
// using Lukki.Application.Common.Interfaces.Authentication;
// using Lukki.Application.Common.Interfaces.Persistence;
// using Lukki.Domain.Common.Enums;
// using Lukki.Domain.Common.Errors;
// using Lukki.Domain.Common.Interfaces;
// using Lukki.Domain.CustomerAggregate;
// using Lukki.Domain.SellerAggregate;
// using MediatR;
//
// namespace Lukki.Application.Authentication.Commands.Register;
//
// public class RegisterCommandHandler : 
//     IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
// {
//     
//     private readonly IJwtTokenGenerator _jwtTokenGenerator;
//     private readonly IUserRepository _userRepository;
//
//     public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
//     {
//         _jwtTokenGenerator = jwtTokenGenerator;
//         _userRepository = userRepository;
//     }
//
//     public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
//     {
//         
//         // 1. Validate the user doesn't exist
//         if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
//         {
//             return Errors.User.DuplicateEmail;
//         }
//         
//         // 2. Create user (generate unique ID) & Persist to DB
//         IUser user = command.Role switch
//         {
//             UserRole.CUSTOMER => Customer.Create(
//                 firstName: command.FirstName,
//                 lastName: command.LastName,
//                 passwordHash: command.Password,
//                 email: command.Email,
//                 phoneNumber: command.PhoneNumber),
//             
//             UserRole.SELLER => Seller.Create(
//                 brandName: command.BrandName,
//                 firstName: command.FirstName,
//                 lastName: command.LastName,
//                 passwordHash: command.Password,
//                 email: command.Email),
//             _ => throw new ArgumentException("Unknown role")
//         };
//         await _userRepository.AddAsync(user);
//         
//         // 3. Create JWT token
//
//         var token = _jwtTokenGenerator.GenerateToken(user);
//
//         return new AuthenticationResult(
//             user,
//             token);
//     }
// }