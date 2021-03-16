using System;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using StudyReporter.Models.Dto;
using Newtonsoft.Json;
using MediatR;

namespace StudyReporter.Features.Notifications
{
    public class CreateStudyReports
    {
        public class Notification : INotification
        {
            public Notification(string folderPath, IEnumerable<StudentReportDto> students)
            {
                FolderPath = folderPath;
                Students = students;
            }

            public IEnumerable<StudentReportDto> Students { get; set; }

            public string FolderPath { get; set; }
        }

        public class JsonReport : INotificationHandler<Notification>
        {
            private const string _reportName = "JsonStudyReport.json"; 

            public Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                var outputFile = Path.Combine(notification.FolderPath, _reportName);

                using (var streamWriter = new StreamWriter(outputFile))
                {
                    var jsonSerializer = JsonSerializer.CreateDefault();

                    jsonSerializer.Serialize(streamWriter, notification.Students);
                }

                return Task.CompletedTask;
            }
        }

        public class XmlReport : INotificationHandler<Notification>
        {
            private const string _reportName = "XmlStudyReport.xml";

            public Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                var studentReportCollection = notification.Students;

                var xDoc = new XDocument();

                var rootElement = new XElement(nameof(studentReportCollection));

                foreach (var studentReport in studentReportCollection)
                {
                    rootElement.Add(CreateStudentReportNode(studentReport));
                }

                xDoc.Add(rootElement);

                var outputFile = Path.Combine(notification.FolderPath, _reportName);

                xDoc.Save(outputFile);

                return Task.CompletedTask;
            }

            private XElement CreateStudentReportNode(StudentReportDto studentReport)
            {
                var studyReportNode = new XElement(nameof(studentReport));

                studyReportNode.Add(new XElement(nameof(StudentReportDto.FirstName), studentReport.FirstName));
                studyReportNode.Add(new XElement(nameof(StudentReportDto.LastName), studentReport.LastName));
                studyReportNode.Add(new XElement(nameof(StudentReportDto.Course), studentReport.Course));

                var subjectCollectionNode = new XElement(nameof(studentReport.Subjects));

                foreach (var subjectReport in studentReport.Subjects)
                {              
                    subjectCollectionNode.Add(CreateSubjectReportNode(subjectReport));
                }

                studyReportNode.Add(subjectCollectionNode);

                return studyReportNode;
            }

            private XElement CreateSubjectReportNode(SubjectReportDto subjectReport)
            {
                var subjectReportNode = new XElement(nameof(subjectReport));

                subjectReportNode.Add(new XElement(nameof(SubjectReportDto.SubjectName), subjectReport.SubjectName));
                subjectReportNode.Add(new XElement(nameof(SubjectReportDto.AverageMark), subjectReport.AverageMark));
                subjectReportNode.Add(new XElement(nameof(SubjectReportDto.MaxMark), subjectReport.MaxMark));
                subjectReportNode.Add(new XElement(nameof(SubjectReportDto.MinMark), subjectReport.MinMark));

                return subjectReportNode;
            }
        }

        public class ConsoleReport : INotificationHandler<Notification>
        {
            public Task Handle(Notification notification, CancellationToken cancellationToken)
            {
                var studentCollection = notification.Students;

                foreach (var student in studentCollection)
                {
                    Console.WriteLine($"{nameof(StudentReportDto.FirstName)}:\t{student.FirstName}");
                    Console.WriteLine($"{nameof(StudentReportDto.LastName)}:\t{student.LastName}");
                    Console.WriteLine($"{nameof(StudentReportDto.Course)}:\t{student.Course}");
                    Console.WriteLine($"CountOfSubjects:\t{student.Subjects.Count()}");
                }

                return Task.CompletedTask;
            }
        }
    }
}
