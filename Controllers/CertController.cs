using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFBackend.Data;
using TFBackend.Entities.Dto.Certs;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CertController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

         
        [HttpGet]
        public IActionResult GetCerts()
        {
            var Certs = _context.Certs.Include( c => c.CertCategory).ToList();
            return CustomResult("Success", Certs);    
        }
        [HttpGet("{id}")]
        public IActionResult GetOneCert(int id)
        {
            if (!CheckCertExist(id))
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            var cert = _context.Certs.Where(c => c.Id == id).FirstOrDefault();

            return CustomResult("Success", cert);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCert(int id)
        {
            if (!CheckCertExist(id))
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            var removeCert = _context.Certs.Where(c => c.Id == id).FirstOrDefault();
            _context.Certs.Remove(removeCert);
            if (Save())
            {
                return CustomResult("Success");
            }
            else
            {
                return CustomResult("Badquest", System.Net.HttpStatusCode.BadRequest);

            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CertDtoPost certDto)
        {
            if (!CheckCertExist(id))
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);

            var certRe = _context.Certs.Where(_ => _.Id == id).FirstOrDefault();  
            
            certRe.Name = certDto.Name ?? certRe.Name;
            certRe.CertCategoryId = certDto.CateId;
            certRe.Description = certDto.Description ?? certRe.Description;

            if (Save())
            {
                return CustomResult("Success", certRe);
            }
            else
            {
                return CustomResult("Badquest", System.Net.HttpStatusCode.BadRequest);

            }
        }

        [HttpPost]
        public IActionResult PostCert(CertDtoPost CertDto )
        {
            if (!CheckCatetExist(CertDto.CateId))
                return CustomResult("Category not exist !", System.Net.HttpStatusCode.BadRequest);

            if (CheckCertName(CertDto.Name))
                return CustomResult("Certificate name already exist !", System.Net.HttpStatusCode.BadRequest);
            var NewCert = new Cert()
            {
                Name = CertDto.Name,
                Description = CertDto.Description,
                CertCategoryId = CertDto.CateId
            };
             _context.Certs.Add(NewCert);

            if (Save())
            {

            return CustomResult("Success", NewCert);
            }
            else
            {

            return CustomResult("Bad request", System.Net.HttpStatusCode.BadGateway);

            }
        }
        
 
       

        private bool CheckCertName(string Name)
        {
            return _context.Certs.Any(c => c.Name == Name);
        }

        private bool CheckCertExist (int  Id)
        {
            return _context.Certs.Any(c => c.Id == Id);
        }

        private bool CheckCatetExist(int Id)
        {
            return _context.CertCategories.Any(c => c.Id == Id);
        }

        private bool CheckStafftExist(int Id)
        {
            return _context.Staff.Any(c => c.Id == Id);
        }

        private bool CheckCategoryName(string Name)
        {
            return _context.CertCategories.Any(c => c.Name == Name);
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
