using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using TFBackend.Data;
using TFBackend.Entities.Dto.Certs;
using TFBackend.Models;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public IActionResult GetOneCate(int id)
        {
            if (!CheckCertExist(id))
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            var cert = _context.CertCategories.Where(c => c.Id == id).FirstOrDefault();

            return CustomResult("Success", cert);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCert(int id)
        {
            if (!CheckCertExist(id))
                return CustomResult("Bad request", System.Net.HttpStatusCode.BadRequest);
            var removeCert = _context.CertCategories.Where(c => c.Id == id).FirstOrDefault();
            _context.CertCategories.Remove(removeCert);
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

            var certRe = _context.CertCategories.Where(_ => _.Id == id).FirstOrDefault();

            certRe.Name = certDto.Name ?? certRe.Name;

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
        public IActionResult PostCertCategory(CategoryCertDtoPost CateDto)
        {
            if (CheckCategoryName(CateDto.Name))
                return CustomResult("Category name already exist !", System.Net.HttpStatusCode.BadRequest);
            var NewCate = new CertCategory()
            {
                Name = CateDto.Name,

            };
            _context.CertCategories.Add(NewCate);

            if (Save())
                return CustomResult("Success", NewCate);

            return CustomResult("Bad request", System.Net.HttpStatusCode.BadGateway);
        }

        [HttpGet]
        public IActionResult GetCategorys()
        {
            var Categories = _context.CertCategories.ToList();
            return CustomResult("Success", Categories);
        }


        private bool CheckCertName(string Name)
        {
            return _context.Certs.Any(c => c.Name == Name);
        }

        private bool CheckCertExist(int Id)
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
