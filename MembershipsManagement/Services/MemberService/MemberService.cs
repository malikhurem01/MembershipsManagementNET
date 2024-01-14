using AutoMapper;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.MemberInfoDTOs;
using MembershipsManagement.DTOs.PaymentDTOs;
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
                .Where(s => s.DzematId == member.DzematId)
                .FirstOrDefaultAsync();

            if (supervisor is null)
            {
                throw new EntityNotFoundException("FATAL ERROR. No supervisor ID: " + userId + " for dzemat with ID: " + member.DzematId + " was found.");
            }

            var memberWithSameEvNumber = await _memberContext.Members.Where(m => m.DzematId == member.DzematId).Where(m => m.EvNumber == member.EvNumber).FirstOrDefaultAsync();

            if (memberWithSameEvNumber is not null)
            {
                throw new BadRequestException("Član sa evidencionim brojem " + member.EvNumber + " već postoji");

            }

            var memberToAdd = _mapper.Map<Member>(member);

                var activeDzematSpreadsheet = await _memberContext.Spreadsheets
                    .Where(s => s.DzematId == member.DzematId)
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

        public async Task<ServiceResponse<GetMemberDto>> ModifyMember(ModifyMemberDto memberModified)
        {
            var response = new ServiceResponse<GetMemberDto>();

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
            memberFetched.DateOfBirth = memberModified.DateOfBirth;
            memberFetched.Address = memberModified.Address;
            memberFetched.Email = memberModified.Email;
            memberFetched.PhoneNumber = memberModified.PhoneNumber;
            memberFetched.EvNumber = memberModified.EvNumber;
            memberFetched.Note = memberModified.Note;
            memberFetched.Status = memberModified.Status;
            memberFetched.Active = memberModified.Active;

            await _memberContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetMemberDto>(memberFetched);
            response.Message = "Informacije uspješno uređene";
            return response;
        }

        public async Task<ServiceResponse<List<GetMemberDto>>> GetMemberList(string token)
        {
            var response = new ServiceResponse<List<GetMemberDto>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _memberContext.Supervisors.Where(s => s.Id.Equals(userId)).Include(s => s.Dzemat).FirstOrDefaultAsync();

            var membersFetched = await _memberContext.Members
                .Where(member => member.DzematId == supervisor.DzematId)
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
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _memberContext.Supervisors.Where(s => s.Id.Equals(userId)).Include(s => s.Dzemat).FirstOrDefaultAsync();
            var memberFetched = await _memberContext.Members
                .Where(member => member.Id == memberId)
                .Where(member => member.DzematId == supervisor.DzematId)
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
            var userId = decodedToken.Claims
                 .Where(claim => claim.Type
                 .Equals("nameid"))
                 .Select(claim => claim.Value)
                 .SingleOrDefault();

            var supervisor = await _memberContext.Supervisors.Where(s => s.Id.Equals(userId)).Include(s => s.Dzemat).FirstOrDefaultAsync();

            var memberFetched = await _memberContext.Members
                .Where(member => member.Id == familyMember.MemberId)
                .Where(member => member.DzematId.Equals(supervisor.DzematId))
                .FirstOrDefaultAsync();

            if (memberFetched is null) 
                throw new EntityNotFoundException("Member ID: " + familyMember.MemberId + " in džemat ID: " + supervisor.DzematId + " was not found.");

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
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _memberContext.Supervisors.Where(s => s.Id.Equals(userId)).Include(s => s.Dzemat).FirstOrDefaultAsync();

            var memberFetched = await _memberContext.Members
                .Where(member => member.Id == familyMember.MemberId)
                .Where(member => member.DzematId == supervisor.DzematId)
                .Include(member => member.FamilyMembers)
                .FirstOrDefaultAsync();

            if (memberFetched is null)
                throw new EntityNotFoundException("Member ID: " + familyMember.MemberId + " in džemat ID: " + supervisor.DzematId + " was not found.");

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

        public async Task<ServiceResponse<PagedList<GetSpreadsheetMemberInfo>>> GetFilteredMembersForSpredsheet(
            string token,
            int spreadsheetId, 
            string? evNumber,
            string? firstName,
            string? lastName,
            string? fathersName,
            bool? hasPayed,
            int pageNumber,
            int pageSize)
        {
            var response = new ServiceResponse<PagedList<GetSpreadsheetMemberInfo>>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("nameid"))
                .Select(claim => claim.Value)
                .SingleOrDefault();

            var supervisor = await _memberContext.Supervisors.Where(s => s.Id.Equals(userId)).FirstOrDefaultAsync();

            IQueryable<MemberInfo> membersInfoQuery;

            if (supervisor.Position.Equals(Roles.GlavniImam))
            {
                membersInfoQuery = _memberContext.MembersInfo.Where(mi => mi.Spreadsheet.Dzemat.MedzlisId == supervisor.MedzlisId);
            }
            else
            {
                membersInfoQuery = _memberContext.MembersInfo.Where(mi => mi.Spreadsheet.Dzemat.Id == supervisor.DzematId);
            }

            membersInfoQuery = membersInfoQuery.Where(mi => mi.SpreadsheetId == spreadsheetId);

            if(int.TryParse(evNumber, out int parsedEvNumber))
            {
                membersInfoQuery = membersInfoQuery.Where(
                mi => mi.Member.EvNumber == parsedEvNumber);
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                membersInfoQuery = membersInfoQuery.Where(mi =>
                mi.Member.FirstName.Contains(firstName));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                membersInfoQuery = membersInfoQuery.Where(mi =>
                mi.Member.LastName.Contains(lastName));
            }
            if (!string.IsNullOrEmpty(fathersName))
            {
                membersInfoQuery = membersInfoQuery.Where(mi =>
                mi.Member.FathersName.Contains(fathersName));
            }
            if (hasPayed == true)
            {
                membersInfoQuery = membersInfoQuery.Where(mi => mi.Member.Debt == 0);
            }

            var spreadsheetMemberInfoQuery = membersInfoQuery
                .Include(mi => mi.Spreadsheet)
                .Include(mi => mi.Member)
                .ThenInclude(m => m.FamilyMembers)
                .Include(mi => mi.Payments)
                .ThenInclude(p => p.Supervisor)
                .Select(mi => new GetSpreadsheetMemberInfo
                {
                    Year = mi.Spreadsheet.Year,
                    TotalAmountPayed = mi.TotalAmountPayed,
                    Member = _mapper.Map<GetMemberDto>(mi.Member),
                    MembershipFee = mi.MembershipFee,
                    Payments = mi.Payments.Where(p => p.SpreadsheetId == spreadsheetId).OrderByDescending(p => p.DateOfPayment).Select(y => new GetSpreadsheetPaymentDto
                    {
                        Id = y.Id,
                        Amount = y.Amount,
                        DateOfPayment = y.DateOfPayment,
                        SupervisorFirstName = y.Supervisor.FirstName,
                        SupervisorLastName = y.Supervisor.LastName
                    }).ToList()
                }).AsNoTracking();

            var spreadsheetMemberInfoResponse = await PagedList<GetSpreadsheetMemberInfo>
                .CreateAsync(spreadsheetMemberInfoQuery, pageNumber, pageSize);

            _memberContext.ChangeTracker.LazyLoadingEnabled = false;

            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            response.Data = spreadsheetMemberInfoResponse;
            return response;

        }

        public async Task<object> Populate(int dzematId)
        {
            List<Member> members = new List<Member>();
            for (int i = 0; i < 500; i++)
            {
                Member newMember = new Member()
                {
                    EvNumber = i + 1,
                    FirstName = "@@ČlanIme" + i + 1,
                    FathersName = "@@ČlanOtac" + i + 1,
                    LastName = "@@ČlanPrezime" + i + 1,
                    DateOfBirth = DateTime.Now,
                    PhoneNumber = "@@123456789101112",
                    Address = "@@Malešići bb " + i,
                    Email = "@@ČlanEmail@gmail.com",
                    Debt = 0,
                    Note = "@@Napomena",
                    Status = Status.Brak,
                    DzematId = dzematId,
                    Active = true
                };
                members.Add(newMember);
            }
            _memberContext.Members.AddRange(members);
            await _memberContext.SaveChangesAsync();

            return "Dodano 500 članova";
        }
    }
}
