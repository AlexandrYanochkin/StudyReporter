using System;
using System.Linq;
using System.Threading.Tasks;
using StudyReporter.Models.Dto;
using StudyReporter.Models.Interfaces;
using StudyReporter.Features.Notifications;
using StudyReporter.Features.Queries;
using StudyReporter.Models.Factories;
using MediatR;

namespace StudyReporter.Models
{
    public class ConsoleFacade
    {
        private readonly IMediator _mediator;

        private readonly IValidator<StudentDto> _studentValidator;

        private readonly string _filePath;

        private readonly string _outputDirectory;

        public ConsoleFacade(IMediator mediator, IValidator<StudentDto> validator, string filePath, string outputDirectory)
        {
            _mediator = mediator;
            _studentValidator = validator;
            _filePath = filePath;
            _outputDirectory = outputDirectory;
        }

        public async Task Main()
        {
            try
            {
                var query = new GetStudentCsvDataFromFile.Query(_filePath);

                var studentCollection = await _mediator.Send(query);

                var validationResults = studentCollection.Select(t => _studentValidator.Validate(t));

                if (validationResults.Any(t => !t.IsValid))
                {
                    var validationResult = validationResults.First(t => !t.IsValid);

                    Console.WriteLine(validationResult.Error);

                    return;
                }

                var studentReportCollection = ReportFactory.CreateStudentReport(studentCollection);

                var notification = new CreateStudyReports.Notification(_outputDirectory, studentReportCollection);

                await _mediator.Publish(notification);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }        
        }

    }
}
