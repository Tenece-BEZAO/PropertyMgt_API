/*using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;


namespace Property_Management.BLL.Implementations
{
    public class MaintenaceRequestServices : IMaintenanceRequestServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Tenant> _tenantRepo;
        private readonly IRepository<MaintenanceRequest> _maintenaceRequestRepo;

        public MaintenaceRequestServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _maintenaceRequestRepo = _unitOfWork.GetRepository<MaintenanceRequest>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
        }

        public async Task<Response> AddOrUpdateAsync(AddOrUpdateMaintenanceVM model)
        {

            // _taskRepo.GetSingleByAsync(t => t.UserId == model.UserId && t.Id == model.TaskId);

            Tenant tenant = await _tenantRepo.GetSingleByAsync(u => u.TenantId == model.TenantId, include: u => u.Include(x => x.MaintenanceRequests), tracking: true);

            if (tenant == null)
            {
                return new Response
                {
                    StatusCode = 400,
                    Message = $"tenant with id:{model.TenantId} wasn't found",
                    Action = $"Maintainace request {false}"
                };
            }

            MaintenanceRequest maintenance = tenant.MaintenanceRequests.SingleOrDefault(t => t.MaintenanceRequestId == model.MaintenanceRequestId);


            if (maintenance != null)
            {

                _mapper.Map(model, maintenance);

                //
                // task.Title = model.Title;
                // task.Description = model.Description;
                // task.Priority = (model.Priority ?? Priority.Normal);
                // task.DueDate = model.DueDate;

                await _unitOfWork.SaveChangesAsync();
                return new Response
                {
                    StatusCode = 400,
                    Message = $"tenant with id:{model.TenantId} has been updated.",
                    Action = $"Maintainace request {true}"
                };
            }

            // var newTask = _mapper.Map<AddOrUpdateTaskVM,Todo>(model);
            var newMaintenaceRequest = _mapper.Map<MaintenanceRequest>(model);

            // var newTask = new Todo
            // {
            //  
            //     Title = model.Title,
            //     Description = model.Description,
            //     Priority = model.Priority ?? Priority.Normal,
            //     DueDate = model.DueDate,
            //
            // };
            tenant.MaintenanceRequests.Add(newMaintenaceRequest);

            var rowChanges = await _unitOfWork.SaveChangesAsync();

            return rowChanges > 0 ? new Response
            {
                StatusCode = 400,
                Message = $"tenant with id:{model.TenantId} wasn't found",
                Action = $"Maintainace request {false}"
            } : new Response
            {
                StatusCode = 400,
                Message = $"tenant with id:{model.TenantId} wasn't found",
                Action = $"Maintainace request {false}"
            };

        }


    }
}*/

using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.Interfaces;
using Property_Management.BLL.Models;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.DAL.Context;

using System.Text.Json.Serialization;
using System.Text.Json;
/*using NotImplementedException = LMS.BLL.Exceptions.NotImplementedException;*/
using AutoMapper;

namespace LMS.BLL.Implementation
{
    public class MaintainanceServices : IMaintenanceRequestServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MaintainanceServices> _maintenaceRepo;
        private readonly IRepository<Tenant> _tenantRepo;
        /*private readonly IRepository<Student> _studentRepo;
        private readonly IRepository<EnrolledStudentsCourses> _enrolledRepo;
        private readonly IRepository<CompletedStudentsCourses> _completedRepo;*/
        private readonly PMSDbContext _dbContext;
        private readonly IMapper _mapper;

        public MaintainanceServices(IUnitOfWork unitOfWork, PMSDbContext dbContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
            _maintenaceRepo = _unitOfWork.GetRepository<MaintainanceServices>();
            _tenantRepo = _unitOfWork.GetRepository<Tenant>();
           /* _studentRepo = _unitOfWork.GetRepository<Student>();
            _enrolledRepo = _unitOfWork.GetRepository<EnrolledStudentsCourses>();
            _completedRepo = _unitOfWork.GetRepository<CompletedStudentsCourses>();*/

        }
        public async Task<TenantDTO> CreateCourse(CreateCourseDto course)
        {
            var Instructor = await _instructorRepo.GetByIdAsync(course.InstructorId);
            if (Instructor == null)
                throw new NotFoundException("Invalid instructor Id");

            Course newCourse = new Course()
            {
                Title = course.Title,
                Detail = course.Detail,
                HeaderImageUrl = course.HeaderImageUrl,
                Price = course.Price,
                VideoResourceUrl = course.VideoResourceUrl,
                TextResourceUrl = course.TextResourceUrl,
                AdditionalResourcesUrl = course.AdditionalResourcesUrl,
                CourseType = course.CourseType,
                InstructorId = course.InstructorId,
                IsActive = false,

            };
            var createdCourse = await _courseRepo.AddAsync(newCourse);
            if (createdCourse == null)
                throw new NotImplementedException("Course was not able to be created");

            return new CourseDto()
            {

                Title = createdCourse.Title,
                Detail = createdCourse.Detail,
                HeaderImageUrl = createdCourse.HeaderImageUrl,
                Price = createdCourse.Price,
                VideoResourceUrl = createdCourse.VideoResourceUrl,
                TextResourceUrl = createdCourse.TextResourceUrl,
                AdditionalResourcesUrl = createdCourse.AdditionalResourcesUrl,
                CourseType = createdCourse.CourseType,
                InstructorId = createdCourse.InstructorId,
                IsActive = false,
            };
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException("Course not found");
            await _courseRepo.DeleteAsync(course);
            return true;
        }

        public async Task<CourseDto> EditCourse(EditCourseDto editCourse)
        {
            var Instructor = await _instructorRepo.GetByIdAsync(editCourse.InstructorId);
            if (Instructor == null)
                throw new NotFoundException("Invalid instructor Id");

            var foundCourse = await _courseRepo.GetByIdAsync(editCourse.Id);
            if (foundCourse == null)
                throw new NotFoundException("Course not found");

            foundCourse.Title = editCourse.Title;
            foundCourse.Detail = editCourse.Detail;
            foundCourse.HeaderImageUrl = editCourse.HeaderImageUrl;
            foundCourse.Price = editCourse.Price;
            foundCourse.VideoResourceUrl = editCourse.VideoResourceUrl;
            foundCourse.TextResourceUrl = editCourse.TextResourceUrl;
            foundCourse.AdditionalResourcesUrl = editCourse.AdditionalResourcesUrl;
            foundCourse.CourseType = editCourse.CourseType;
            foundCourse.IsActive = editCourse.IsActive;



            Course updatedCourse = await _courseRepo.UpdateAsync(foundCourse);
            if (updatedCourse == null)
                throw new NotImplementedException("Unable to update course");

            return new CourseDto()
            {

                Title = updatedCourse.Title,
                Detail = updatedCourse.Detail,
                HeaderImageUrl = updatedCourse.HeaderImageUrl,
                Price = updatedCourse.Price,
                VideoResourceUrl = updatedCourse.VideoResourceUrl,
                TextResourceUrl = updatedCourse.TextResourceUrl,
                AdditionalResourcesUrl = updatedCourse.AdditionalResourcesUrl,
                CourseType = updatedCourse.CourseType,
                InstructorId = updatedCourse.InstructorId,
                IsActive = updatedCourse.IsActive
            };

        }

        public async Task<EnrolledStudentsCourses> EnrollForACourse(CourseEnrollDto courseEnrollDto)
        {
            var student = await _studentRepo.GetByIdAsync(courseEnrollDto.StudentId);
            if (student == null)
                throw new NotFoundException("Invalid user id ");
            var course = await _courseRepo.GetByIdAsync(courseEnrollDto.CourseId);
            if (course == null)
                throw new NotFoundException("Invalid course id");

            var alreadyEnrolled = await _enrolledRepo.GetByAsync(c => c.CourseId == courseEnrollDto.CourseId && c.StudentId == courseEnrollDto.StudentId);
            if (alreadyEnrolled.Count() > 0)
                throw new BadRequestException("You have already enrolled for this course");

            EnrolledStudentsCourses newEnrollCourse = new EnrolledStudentsCourses()
            {
                CourseId = courseEnrollDto.CourseId,
                StudentId = courseEnrollDto.StudentId,
                CreatedBy = student.FullName
            };


            var result = await _enrolledRepo.AddAsync(newEnrollCourse);
            if (result != null)
            {
                return result;
            }

            throw new NotImplementedException("Was not able to enroll for this course");
        }

        public async Task<IEnumerable<Course>> GetAllCompletedCourses()
        {
            var isCompleted = await _completedRepo.GetAllAsync();

            IEnumerable<Course> courses;
            foreach (var enrolledCourse in isCompleted)
            {
                courses = await _courseRepo.GetByAsync(c => c.Id == enrolledCourse.CourseId);

                if (courses.Count() > 0)
                {
                    return courses;
                }
            }
            throw new NotFoundException("No course was found");
        }

        public async Task<IEnumerable<Course>> GetAllCourse()
        {
            var courses = await _courseRepo.GetAllAsync();
            if (courses == null)
                throw new NotImplementedException("No courses");

            return courses;
        }

        public async Task<CourseDto> GetCourseById(int courseId)
        {
            var course = await _courseRepo.GetByIdAsync(courseId);
            if (course == null)
                throw new NotFoundException("No course was found");

            return new CourseDto()
            {
                Title = course.Title,
                Detail = course.Detail,
                HeaderImageUrl = course.HeaderImageUrl,
                Price = course.Price,
                VideoResourceUrl = course.VideoResourceUrl,
                TextResourceUrl = course.TextResourceUrl,
                AdditionalResourcesUrl = course.AdditionalResourcesUrl,
                CourseType = course.CourseType,
                InstructorId = course.InstructorId,
                IsActive = course.IsActive
            };
        }

        public Task<IEnumerable<Course>> GetUserCompletedCourses(int userId)
        {
            throw new System.NotImplementedException();
        }


        public async Task<IEnumerable<CourseDto>> GetUserEnrolledCourses(int studentId)
        {



            var result = await _studentRepo.GetSingleByAsync(s => s.Id == studentId, include: x => x.Include(x => x.EnrolledCourses).ThenInclude(x => x.Course));

            var response = result.EnrolledCourses.Select(x => x.Course).ToList();

            var mappedValue = _mapper.Map<IEnumerable<CourseDto>>(response);
            return mappedValue;
            /* IEnumerable<Course> courses;
             foreach (var enrolledCourse in enrolledCourses)
             {
                 courses = await _courseRepo.GetByAsync(c => c.Id == enrolledCourse.CourseId);
                 if (courses.Count() > 0)
                 {
                     return courses;
                 }
             }
             throw new NotFoundException("No course was found");*/
            //hrow new System.NotImplementedException();
        }

        public async Task<bool> MarkAsComplete(CourseEnrollDto markCourseAsCompleted)
        {
            var enrolledCourse = await _enrolledRepo.GetByAsync(c => c.CourseId == markCourseAsCompleted.CourseId && c.StudentId == markCourseAsCompleted.StudentId);
            if (enrolledCourse.Count() == 0)
                throw new NotFoundException("Course was not found");
            var student = await _studentRepo.GetByIdAsync(markCourseAsCompleted.StudentId);


            var already = await _completedRepo.GetByAsync(c => c.CourseId == markCourseAsCompleted.CourseId && c.StudentId == markCourseAsCompleted.StudentId);
            if (already.Count() != 0)
                throw new NotImplementedException("This course have already been marked as complete");

            var completedCourse = new CompletedStudentsCourses()
            {
                CourseId = markCourseAsCompleted.CourseId,
                StudentId = markCourseAsCompleted.StudentId,
                CreatedBy = student.FullName
            };

            var created = await _completedRepo.AddAsync(completedCourse);
            if (created == null)
                throw new NotImplementedException("Was ubale to complete course");


            return true;
        }
    }
}


