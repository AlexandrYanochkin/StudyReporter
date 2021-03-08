using System;
using System.Configuration;
using System.Threading.Tasks;
using StudyReporter.Models;
using StudyReporter.Models.Dto;
using StudyReporter.Models.Interfaces;
using StudyReporter.DependencyInjection;
using Autofac;
using MediatR;

namespace StudyReporter
{
    public static class Program
    {
        private static readonly string _filePath =
            ConfigurationManager.ConnectionStrings["dataFileConnection"].ConnectionString;

        private static string _outputFolderPath = @"..\..\Data\OutputData";
 
        public static async Task Main(string[] args)
        {
            var container = ContainerFactory.GetContainer();

            var mediator = container.Resolve<IMediator>();

            var studentValidator = container.Resolve<IValidator<StudentDto>>();

            var facade = new ConsoleFacade(mediator, studentValidator, _filePath, _outputFolderPath);

            await facade.Main();

            Console.ReadKey();
        }
    }
}
