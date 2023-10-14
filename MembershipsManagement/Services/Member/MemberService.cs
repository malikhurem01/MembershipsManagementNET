using AutoMapper;
using AutoMapper.Execution;
using MembershipsManagement.Data;
using MembershipsManagement.DTOs.Member;
using MembershipsManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace MembershipsManagement.Services.Member
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

        public async Task<ServiceResponse<GetMemberDto>> AddMember(AddMemberDto member)
        {
            var response = new ServiceResponse<GetMemberDto>();
            var memberToAdd = _mapper.Map<Clan>(member);
            _memberContext.Clanovi.Add(memberToAdd);
            await _memberContext.SaveChangesAsync();
            response.Data = _mapper.Map<GetMemberDto>(memberToAdd);
            response.Message = "Member has been successfully added";
            return response;
        }

        public async Task<ServiceResponse<GetMemberDto>> ModifyMember(ModifyMemberDto memberModified)
        {
            var response = new ServiceResponse<GetMemberDto>();

            var memberFetched = await _memberContext.Clanovi
                .Where(member => member.Id == memberModified.Id)
                .Include(member => member.Dzemat)
                .FirstOrDefaultAsync();

            if (memberFetched == null)
            {
                throw new EntityNotFoundException("Member not found in database.");
            }

            memberFetched.FirstName = memberModified.FirstName.Length > 0 ? memberModified.FirstName : memberFetched.FirstName;
            memberFetched.FathersName = memberModified.FathersName.Length > 0 ? memberModified.FathersName : memberFetched.FathersName;
            memberFetched.LastName = memberModified.LastName.Length > 0 ? memberModified.LastName : memberFetched.LastName;
            memberFetched.EvBroj = memberModified.EvBroj > 0 ? memberModified.EvBroj : memberFetched.EvBroj;
            memberFetched.Status = memberModified.Status > 0 ? memberModified.Status : memberFetched.Status;
            memberFetched.DzematId = memberModified.DzematId > 0 ? memberModified.DzematId : memberFetched.DzematId;

            await _memberContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetMemberDto>(memberFetched);
            response.Message = "Member has been successfully modified for dzemat " + memberFetched.Dzemat.Ime + ".";
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
            Int32.TryParse(dzematId, out int dzematIdInt);
            var membersFetched = await _memberContext.Clanovi
                .Where(member => member.DzematId == dzematIdInt)
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
            Int32.TryParse(dzematId, out int dzematIdInt);
            var memberFetched = await _memberContext.Clanovi
                .Where(member => member.Id == memberId)
                .Where(member => member.DzematId == dzematIdInt)
                .FirstOrDefaultAsync();
            response.Data = _mapper.Map<GetMemberDto>(memberFetched);
            response.Message = memberFetched != null? "Member has been fetched." : "No member has been found.";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteMember(DeleteMemberDto member)
        {
            var response = new ServiceResponse<string>();
            var memberToDelete = await _memberContext.Clanovi
                .Where(c => c.Id == member.Id)
                .FirstOrDefaultAsync();
            if(memberToDelete == null)
            {
                throw new EntityNotFoundException("Member with ID: " + member.Id + " was not found in database.");
            }
            _memberContext.Clanovi.Remove(memberToDelete);
            await _memberContext.SaveChangesAsync();
            response.Data = "No response data.";
            response.Message = "Member with ID: " + member.Id + " was successfully deleted from database.";
            return response;
        }

        public async Task<ServiceResponse<GetMemberDto>> AddMemberFamily(string token, AddMemberFamilyListDto familyMembers)
        {
            var response = new ServiceResponse<GetMemberDto>();
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var dzematId = decodedToken.Claims
                .Where(claim => claim.Type
                .Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata"))
                .Select(claim => claim.Value)
                .SingleOrDefault();
            Int32.TryParse(dzematId, out int dzematIdInt);
            var memberFetched = await _memberContext.Clanovi
                .Where(member => member.Id == familyMembers.ClanId)
                .Where(member => member.DzematId == dzematIdInt)
                .FirstOrDefaultAsync();

            if (memberFetched is null) 
                throw new EntityNotFoundException("Member ID: " + familyMembers.ClanId + " in džemat ID: " + dzematIdInt + " was not found.");

            List<ClanPorodice> familyList = familyMembers.FamilyList.Select(f => _mapper.Map<ClanPorodice>(f)).ToList();
            memberFetched.FamilyMembers.AddRange(familyList);

            await _memberContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetMemberDto>(memberFetched);
            response.Message = "Family members have been added for member ID: " + memberFetched.Id + ".";

            return response;

        }

        public Task<ServiceResponse<GetMemberDto>> AddMemberFamily(AddMemberFamilyListDto familyMembers)
        {
            throw new NotImplementedException();
        }
    }
}
