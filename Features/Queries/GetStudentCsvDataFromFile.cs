using System;
using System.IO;
using System.Collections.Generic;
using StudyReporter.Models.Dto;
using StudyReporter.Models.Interfaces;
using StudyReporter.Models.Constants;
using MediatR;

namespace StudyReporter.Features.Queries
{
    public class GetStudentCsvDataFromFile 
    {
        public class Query : IRequest<IEnumerable<StudentDto>>
        {
            public Query(string filePath)
            {
                FilePath = filePath;
            }

            public string FilePath { get; set; }      
        }

        public class QueryHandler : RequestHandler<Query, IEnumerable<StudentDto>>
        {
            private readonly IParser<StudentDto> _studentParser;

            public QueryHandler(IParser<StudentDto> studentParser)
            {
                _studentParser = studentParser;
            }

            protected override IEnumerable<StudentDto> Handle(Query request)
            {
                if (!File.Exists(request.FilePath))
                {
                    throw new ArgumentException(ErrorMessages.IncorrectFilePath);
                }

                var studentCollection = new List<StudentDto>();

                using (var streamReader = new StreamReader(request.FilePath))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var fileData = streamReader.ReadLine();
                        var student = _studentParser.Parse(fileData);

                        studentCollection.Add(student);
                    }
                }

                return studentCollection;
            }
        }

    }
}
