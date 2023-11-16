using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MembershipsManagement.Services.MemberServiceImpl
{
    public class MemberService : IMemberService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _memberContext;
        public MemberService(IMapper mapper, DataContext memberContext)
        {
            _mapper = mapper;
            _memberContext = memberContext;
        }

        public async Task<ServiceResponse<GetMemberDto>> AddMember(string token, AddMemberDto member)
        {
            var response = new ServiceResponse<GetMemberDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _memberContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .Where(s => s.DzematId.Equals(member.DzematId))
                .FirstOrDefaultAsync();

            if (supervisor is null)
            {
                throw new EntityNotFoundException("FATAL ERROR. No supervisor ID: " + userId + " for dzemat with ID: " + member.DzematId + " was found.");
            }

            var memberToAdd = _mapper.Map<Member>(member);

                var activeDzematSpreadsheet = await _memberContext.Spreadsheets
                    .Where(s => s.DzematId.Equals(member.DzematId))
                    .Where(s => s.Archived == false)
                    .FirstOrDefaultAsync();
                if(activeDzematSpreadsheet is not null)
                {
                    memberToAdd.Spreadsheets.Add(activeDzematSpreadsheet);

                switch (memberToAdd.Status)
                {
                    case Status.Brak:
                        memberToAdd.Debt = activeDzematSpreadsheet.MarriageFee;
                        break;
                    case Status.DobnaGranica:
                        memberToAdd.Debt = activeDzematSpreadsheet.AgeLimitFee;
                        break;
                    case Status.Udovac:
                        memberToAdd.Debt = activeDzematSpreadsheet.WidowerFee;
                        break;
                    default:
                        memberToAdd.Debt = 0;
                        break;
                }


                memberToAdd.Debt += member.Debt;
                _memberContext.Members.Add(memberToAdd);
                    await _memberContext.SaveChangesAsync();
                }
            
            response.Data = _mapper.Map<GetMemberDto>(memberToAdd);
            response.Message = "Član je uspješno dodan";
            return response;
        }

        public async Task<ServiceResponse<object>> GetMemberByNameAndLastName(string token, string name, string lastName, int spreadsheetId)
        {
            var response = new ServiceResponse<object>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _memberContext.Supervisors
                .Where(s => s.Id.Equals(userId))
                .FirstOrDefaultAsync();

            _memberContext.ChangeTracker.LazyLoadingEnabled = false;
            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            if (name.Length > 0 && lastName.Length > 0)
            {
                var membersList = await _memberContext.MembersInfo
                 .Where(mi => mi.SpreadsheetId == spreadsheetId)
                 .Where(mi => mi.Member.FirstName.Contains(name.Trim()))
                 .Where(mi => mi.Member.LastName.Contains(lastName.Trim()))
                 .Include(mi => mi.Spreadsheet)
                 .Include(mi => mi.Member)
                 .Include(mi => mi.Payments)
                 .ThenInclude(p => p.Supervisor)
                 .Select(mi => new
                 {
                     totalAmountPayed = mi.TotalAmountPayed,
                     member = _mapper.Map<GetMemberDto>(mi.Member),
                     note = mi.Note,
                     membershipFee = mi.MembershipFee,
                     latestPayment = mi.Payments.OrderByDescending(p => p.DateOfPayment).FirstOrDefault(),
                     payments = mi.Payments.Select(y => new
                     {
                         id = y.Id,
                         amount = y.Amount,
                         supervisor = _mapper.Map<GetUserDto>(y.Supervisor),
                         dateOfPayment = y.DateOfPayment
                     }).ToArray()
                 }).ToListAsync();

                response.Data = JsonSerializer.Serialize(new { membersList }, options);
                response.Message = "Članovi uspješno učitani";
            }
            else if (name.Length > 0)
            {
                var membersList = await _memberContext.MembersInfo
                  .Where(mi => mi.SpreadsheetId == spreadsheetId)
                  .Where(mi => mi.Member.FirstName.Contains(name.Trim()))
                  .Include(mi => mi.Spreadsheet)
                  .Include(mi => mi.Member)
                  .Include(mi => mi.Payments)
                  .ThenInclude(p => p.Supervisor)
                  .Select(mi => new
                  {
                      totalAmountPayed = mi.TotalAmountPayed,
                      member = _mapper.Map<GetMemberDto>(mi.Member),
                      note = mi.Note,
                      membershipFee = mi.MembershipFee,
                      latestPayment = mi.Payments.OrderByDescending(p => p.DateOfPayment).FirstOrDefault(),
                      payments = mi.Payments.Select(y => new
                      {
                          id = y.Id,
                          amount = y.Amount,
                          supervisor = _mapper.Map<GetUserDto>(y.Supervisor),
                          dateOfPayment = y.DateOfPayment
                      }).ToArray()
                  }).ToListAsync();

                response.Data = JsonSerializer.Serialize(new { membersList }, options);
                response.Message = "Članovi uspješno učitani";
            }
            else if (lastName.Length > 0)
            {
                var membersList = await _memberContext.MembersInfo
                  .Where(mi => mi.SpreadsheetId == spreadsheetId)
                  .Where(mi => mi.Member.LastName.Contains(lastName.Trim()))
                  .Include(mi => mi.Spreadsheet)
                  .Include(mi => mi.Member)
                  .Include(mi => mi.Payments)
                  .ThenInclude(p => p.Supervisor)
                  .Select(mi => new
                  {
                      totalAmountPayed = mi.TotalAmountPayed,
                      member = _mapper.Map<GetMemberDto>(mi.Member),
                      note = mi.Note,
                      membershipFee = mi.MembershipFee,
                      latestPayment = mi.Payments.OrderByDescending(p => p.DateOfPayment).FirstOrDefault(),
                      payments = mi.Payments.Select(y => new
                      {
                          id = y.Id,
                          amount = y.Amount,
                          supervisor = _mapper.Map<GetUserDto>(y.Supervisor),
                          dateOfPayment = y.DateOfPayment
                      }).ToArray()
                  }).ToListAsync();

                response.Data = JsonSerializer.Serialize(new { membersList }, options);
                response.Message = "Članovi uspješno učitani";
            }

            return response;

        }

        public async Task<ServiceResponse<object>> ModifyMember(ModifyMemberDto memberModified)
        {
            var response = new ServiceResponse<object>();

            var memberFetched = await _memberContext.Members
                .Where(member => member.Id == memberModified.Id)
                .Include(member => member.Dzemat)
                .Include(member => member.Payments)
                .Include(member => member.Spreadsheets)
                .Include(member => member.FamilyMembers)
                .FirstOrDefaultAsync();

            if (memberFetched == null)
            {
                throw new EntityNotFoundException("Član nije pronađen u bazi podataka. Molimo kontaktirajte podršku.");
            }

            memberFetched.FirstName = memberModified.FirstName;
            memberFetched.FathersName = memberModified.FathersName;
            memberFetched.LastName = memberModified.LastName;
            memberFetched.Address = memberModified.Address;
            memberFetched.Email = memberModified.Email;
            memberFetched.PhoneNumber = memberModified.PhoneNumber;
            memberFetched.EvNumber = memberModified.EvNumber;
            memberFetched.Status = memberModified.Status;

            await _memberContext.SaveChangesAsync();

            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            response.Data = JsonSerializer.Serialize(new {memberFetched}, options);
            response.Message = "Informacije uspješno uređene";
            return response;
        }

        public async Task<ServiceResponse<List<GetMemberDto>>> GetMemberList(string token)
        {
            var response = new ServiceResponse<List<GetMemberDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var dzematId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata"))
                .Select(claim => claim.Value)
                .SingleOrDefault();
            var membersFetched = await _memberContext.Members
                .Where(member => member.DzematId.Equals(dzematId))
                .ToListAsync();

            response.Data = membersFetched
                .Select(member => _mapper.Map<GetMemberDto>(member))
                .ToList();
            response.Message = "All members have been fetched.";
            return response;
        }

        public async Task<ServiceResponse<GetMemberDto>> GetSingleMember(string token, int memberId)
        {
            var response = new ServiceResponse<GetMemberDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var dzematId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata"))
                .Select(claim => claim.Value)
                .SingleOrDefault();
            var memberFetched = await _memberContext.Members
                .Where(member => member.Id == memberId)
                .Where(member => member.DzematId.Equals(dzematId))
                .FirstOrDefaultAsync();
            response.Data = _mapper.Map<GetMemberDto>(memberFetched);
            response.Message = memberFetched != null? "Member has been fetched." : "No member has been found.";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteMember(DeleteMemberDto member)
        {
            var response = new ServiceResponse<string>();
            var memberToDelete = await _memberContext.Members
                .Where(c => c.Id == member.Id)
                .FirstOrDefaultAsync();
            if(memberToDelete == null)
            {
                throw new EntityNotFoundException("Member with ID: " + member.Id + " was not found in database.");
            }
            _memberContext.Members.Remove(memberToDelete);
            await _memberContext.SaveChangesAsync();
            response.Data = "No response data.";
            response.Message = "Member with ID: " + member.Id + " was successfully deleted from database.";
            return response;
        }

        public async Task<ServiceResponse<GetMemberDto>> AddMemberFamily(string token, AddMemberFamilyDto familyMember)
        {
            var response = new ServiceResponse<GetMemberDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var dzematId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var memberFetched = await _memberContext.Members
                .Where(member => member.Id == familyMember.MemberId)
                .Where(member => member.DzematId.Equals(dzematId))
                .FirstOrDefaultAsync();

            if (memberFetched is null) 
                throw new EntityNotFoundException("Member ID: " + familyMember.MemberId + " in džemat ID: " + dzematId + " was not found.");

            memberFetched.FamilyMembers.Add(_mapper.Map<FamilyMember>(familyMember));

            await _memberContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetMemberDto>(memberFetched);
            response.Message = "Uspješno ste dodali člana porodice";

            return response;

        }

        public async Task<ServiceResponse<GetMemberDto>> DeleteMemberFamily(string token, DeleteFamilyMemberDto familyMember)
        {
            var response = new ServiceResponse<GetMemberDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var dzematId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var memberFetched = await _memberContext.Members
                .Where(member => member.Id == familyMember.MemberId)
                .Where(member => member.DzematId.Equals(dzematId))
                .Include(member => member.FamilyMembers)
                .FirstOrDefaultAsync();

            if (memberFetched is null)
                throw new EntityNotFoundException("Member ID: " + familyMember.MemberId + " in džemat ID: " + dzematId + " was not found.");

            var familyMemberToRemove = memberFetched.FamilyMembers.First(fm => fm.Id == familyMember.Id);

            memberFetched.FamilyMembers.Remove(familyMemberToRemove);

            await _memberContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetMemberDto>(memberFetched);
            response.Message = "Uspješno ste uklonili člana porodice";

            return response;

        }

        public async Task<ServiceResponse<List<GetFamilyMemberDto>>> GetFamily(string token, int memberId)
        {
            var response = new ServiceResponse<List<GetFamilyMemberDto>>();
            var memberFamily = await _memberContext.FamilyMembers
                .Where(fm => fm.MemberId == memberId)
                .ToListAsync();
            response.Data = memberFamily.Select(fm => _mapper.Map<GetFamilyMemberDto>(fm)).ToList();
            response.Message = "Fetched family members";
            return response;
        }

        public Task<ServiceResponse<GetMemberDto>> AddMemberFamily(AddMemberFamilyListDto familyMembers)
        {
            throw new NotImplementedException();
        }
    }
}
