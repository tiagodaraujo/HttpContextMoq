using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests;

public class FormCollectionFakeTests
{
    [Fact]
    public void Add_WhenFileWithName_ExpectGetFileName()
    {
        // Assert
        var target = new FormCollectionFake();

        const string fileName = "attachment";
        var file = new Mock<IFormFile>();
        file.Setup(x => x.Name).Returns(fileName);

        // Act
        target.FilesFake.Add(file.Object);

        // Assert
        target.Files.GetFile(fileName).Should().NotBeNull();
    }

    [Fact]
    public void Add_WhenFilesWithSameName_ExpectHaveTwoFiles()
    {
        // Assert
        var target = new FormCollectionFake();

        const string fileName = "attachment";
        var file1 = new Mock<IFormFile>();
        file1.Setup(x => x.Name).Returns(fileName);
        var file2 = new Mock<IFormFile>();
        file2.Setup(x => x.Name).Returns(fileName);

        // Actv
        target.FilesFake.Add(file1.Object);
        target.FilesFake.Add(file2.Object);

        // Assert
        target.Files.GetFiles(fileName)
            .Should().NotBeNull()
            .And.HaveCount(2);
    }
}
