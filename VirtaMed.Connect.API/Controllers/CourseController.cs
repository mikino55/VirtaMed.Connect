using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtaMed.Data.Entity;
using VirtaMed.Data.Shared.DTO.Course;
using VirtaMed.Persistence.Repository;
using VirtaMed.Services.Mapping;

namespace VirtaMed.Connect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseRepository courseRepository;
        private readonly IDataMapper mapper;

        public CourseController(CourseRepository courseRepository, IDataMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> CreateCourse(CourseDto courseDto)
        {
            var entity = this.mapper.Map<CourseEntity>(courseDto);

            await this.courseRepository.InsertOneAsync(entity);
            return Ok();
        }
    }
}